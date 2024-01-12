using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub
{
    public class RealTime_Data_Class
    {
        private DB_Connection database = new DB_Connection();
        private Activity_Log_Class activityLogger;
        private readonly object updateBookingLockPending = new object();
        private readonly object checkBookingLockReserved = new object();
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private Services_Form servicesForm;

        public RealTime_Data_Class()
        {
            activityLogger = new Activity_Log_Class();
        }

        public RealTime_Data_Class(Services_Form form)
        {
            activityLogger = new Activity_Log_Class();
            servicesForm = form;
        }

        // method to update the ui of in progress
        private async Task<List<In_Progress_Class>> RetrieveProgressListAsync()
        {
            List<In_Progress_Class> progress = new List<In_Progress_Class>();

            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    await connect.OpenAsync();

                    string query = @"SELECT
                                LB.Booking_ID,
                                LB.Customer_ID,
                                LB.Unit_ID,
                                C.Customer_Name,
                                LU.Unit_Name,
                                LB.Services_Type,
                                DATEDIFF(MINUTE, GETDATE(), LB.End_Time) AS TimeLeft,
                                LB.Bookings_Status AS Status
                            FROM
                                Laundry_Bookings LB
                                INNER JOIN Customers C ON LB.Customer_ID = C.Customer_ID
                                INNER JOIN Laundry_Unit LU ON LB.Unit_ID = LU.Unit_ID
                            WHERE
                                LB.Bookings_Status = 'In-Progress';";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                In_Progress_Class pen = new In_Progress_Class()
                                {
                                    BookingID = (int)reader["Booking_ID"],
                                    CustomerID = (int)reader["Customer_ID"],
                                    UnitID = (int)reader["Unit_ID"],
                                    Customer_Name = reader["Customer_Name"].ToString(),
                                    Unit_Name = reader["Unit_Name"].ToString(),
                                    ServiceType = reader["Services_Type"].ToString(),
                                    TimeLeft = Math.Max((int)reader["TimeLeft"], 0).ToString(),
                                    Status = reader["Status"].ToString()
                                };
                                progress.Add(pen);
                            }
                        }
                    }
                }

                return progress;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; 
            }
        }

        // method to load the data in Laundry_Bookings table its show it to the flow out panel with help of ucProgressList_control
        public async Task LoadProgressAsync(FlowLayoutPanel flow_progress)
        {
            try
            {
                flow_progress.Controls.Clear();

                List<In_Progress_Class> progress = await RetrieveProgressListAsync();

                foreach (var prog in progress)
                {
                    ucProgressList_Control progressControl = new ucProgressList_Control(prog);
                    flow_progress.Controls.Add(progressControl);
                }

                // Update the UI periodically
                while (true)
                {
                    await Task.Delay(1000); 

                    progress = await RetrieveProgressListAsync();

                    foreach (Control control in flow_progress.Controls)
                    {
                        if (control is ucProgressList_Control progressListControl)
                        {
                            progressListControl.UpdateTimeLeft();

                            if (progressListControl.TimeLeft == "0 seconds")
                            {
                                if (progressListControl.Status == "In-Progress")
                                {
                                    await UpdateBookingStatusToPendingAsync(progressListControl.BookingID, progressListControl.UnitID);

                                    await CheckBookingStatusReservedAsync(progressListControl.UnitID);

                                    servicesForm.Load_Unit();
                                    await servicesForm.DisplayInProgress();
                                }
                                else if (progressListControl.Status == "Pending")
                                {
                                    servicesForm.Load_Unit();
                                    await servicesForm.DisplayInProgress();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // method to update the booking status to pending when time limit run out
        public async Task UpdateBookingStatusToPendingAsync(int bookingID, int unitID)
        {
            try
            {
                await UpdateBookingStatusAsync(bookingID, unitID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task UpdateBookingStatusAsync(int bookingID, int unitID)
        {
            lock (updateBookingLockPending)
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open(); 

                    string updateBookingQuery = @"UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID;";
                    string updateUnitQuery = @"UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = @UnitID;";

                    using (SqlCommand updateBookingCommand = new SqlCommand(updateBookingQuery, connect))
                    using (SqlCommand updateUnitCommand = new SqlCommand(updateUnitQuery, connect))
                    {
                        updateBookingCommand.Parameters.AddWithValue("@BookingID", bookingID);
                        updateUnitCommand.Parameters.AddWithValue("@UnitID", unitID);

                        SqlTransaction transaction = connect.BeginTransaction();

                        try
                        {
                            updateBookingCommand.Transaction = transaction;
                            updateUnitCommand.Transaction = transaction;

                            updateBookingCommand.ExecuteNonQuery();
                            updateUnitCommand.ExecuteNonQuery();

                            transaction.Commit();

                            Get_CustomerNameUnitNameInProgress(bookingID, unitID);
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw; 
                        }
                    }
                }
            }
        }

        // notification log when laundry bookings is finished the progress
        public async Task Get_CustomerNameUnitNameInProgress(int BookingID, int UnitID)
        {
            string activityType = "Notifications";
            string Description;

            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT C.Customer_Name, U.Unit_Name
                             FROM Laundry_Bookings B
                             INNER JOIN Customers C ON B.Customer_ID = C.Customer_ID
                             INNER JOIN Laundry_Unit U ON B.Unit_ID = U.Unit_ID
                             WHERE B.Booking_ID = @BookingID AND U.Unit_ID = @UnitID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", BookingID);
                        cmd.Parameters.AddWithValue("@UnitID", UnitID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string customerName = reader["Customer_Name"].ToString();
                                string unitName = reader["Unit_Name"].ToString();

                                Description = $"{customerName}'s laundry is done! {unitName} is ready for the next booking.";

                                activityLogger.LogActivity(activityType, Description);
                            }
                            else
                            {
                                Description = "No matching records found for the provided Booking_ID and Unit_ID.";
                                activityLogger.LogActivity(activityType, Description);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // reservation
        public async Task CheckBookingStatusReservedAsync(int reservedunitID)
        {
            try
            {
                await CheckReservedAsync(reservedunitID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task CheckReservedAsync(int reservedunitID)
        {
            await semaphore.WaitAsync();

            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    SqlTransaction transaction = connect.BeginTransaction();
                    try
                    {
                        List<int> reservedBookingIDs = new List<int>();

                        string findReservedBookingsCmdText = "SELECT Booking_ID FROM Laundry_Bookings WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                        using (SqlCommand findReservedBookingsCmd = new SqlCommand(findReservedBookingsCmdText, connect, transaction))
                        {
                            findReservedBookingsCmd.Parameters.AddWithValue("@UnitID", reservedunitID);

                            using (SqlDataReader reader = findReservedBookingsCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    reservedBookingIDs.Add(reader.GetInt32(0));
                                }
                            }
                        }

                        foreach (int bookingID in reservedBookingIDs)
                        {
                            using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Booking_ID = @BookingID", connect, transaction))
                            {
                                updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                updateBookingCmd.ExecuteNonQuery();
                            }
                        }

                        int unitStatus = (reservedBookingIDs.Count > 0) ? 1 : 0;

                        using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID", connect, transaction))
                        {
                            updateUnitCmd.Parameters.AddWithValue("@UnitID", reservedunitID);
                            updateUnitCmd.Parameters.AddWithValue("@UnitStatus", unitStatus);
                            updateUnitCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        foreach (int bookingID in reservedBookingIDs)
                        {
                            Get_CustomerNameUnitNameInReserved(bookingID, reservedunitID);
                        }

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                semaphore.Release();
            }
        }

        // notification log when laundry bookings reserved is now in progress
        public async Task Get_CustomerNameUnitNameInReserved(int BookingID, int UnitID)
        {
            string activityType = "Notifications";
            string Description;

            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT C.Customer_Name, U.Unit_Name
                             FROM Laundry_Bookings B
                             INNER JOIN Customers C ON B.Customer_ID = C.Customer_ID
                             INNER JOIN Laundry_Unit U ON B.Unit_ID = U.Unit_ID
                             WHERE B.Booking_ID = @BookingID AND U.Unit_ID = @UnitID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", BookingID);
                        cmd.Parameters.AddWithValue("@UnitID", UnitID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string customerName = reader["Customer_Name"].ToString();
                                string unitName = reader["Unit_Name"].ToString();

                                Description = $"{customerName}'s unit reserved is now in progress! {unitName} is currently occupied";

                                activityLogger.LogActivity(activityType, Description);
                            }
                            else
                            {
                                Description = "No matching records found for the provided Booking_ID and Unit_ID.";
                                activityLogger.LogActivity(activityType, Description);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // for reserved
        private async Task UpdateBookingStatusReservedAsync(int UnitID)
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                await connect.OpenAsync();
                SqlTransaction transaction = connect.BeginTransaction();

                try
                {
                    List<int> reservedBookingIDs = new List<int>();

                    string findReservedBookingsCmdText = "SELECT Booking_ID FROM Laundry_Bookings WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                    using (SqlCommand findReservedBookingsCmd = new SqlCommand(findReservedBookingsCmdText, connect, transaction))
                    {
                        findReservedBookingsCmd.Parameters.AddWithValue("@UnitID", UnitID);

                        using (SqlDataReader reader = findReservedBookingsCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservedBookingIDs.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    foreach (int bookingID in reservedBookingIDs)
                    {
                        using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Booking_ID = @BookingID", connect, transaction))
                        {
                            updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                            updateBookingCmd.ExecuteNonQuery();
                        }
                    }

                    int unitStatus = (reservedBookingIDs.Count > 0) ? 1 : 0;

                    using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID", connect, transaction))
                    {
                        updateUnitCmd.Parameters.AddWithValue("@UnitID", UnitID);
                        updateUnitCmd.Parameters.AddWithValue("@UnitStatus", unitStatus);
                        updateUnitCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
