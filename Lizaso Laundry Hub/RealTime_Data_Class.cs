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
        private readonly object updateBookingLockPending = new object();
        private readonly object checkBookingLockReserved = new object();
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private Services_Form servicesForm;

        public RealTime_Data_Class(Services_Form form)
        {
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
                    await Task.Delay(1000); // Update every second

                    // Reload the progress list and update the UI
                    progress = await RetrieveProgressListAsync();

                    foreach (Control control in flow_progress.Controls)
                    {
                        if (control is ucProgressList_Control progressListControl)
                        {
                            progressListControl.UpdateTimeLeft();

                            // Check if the time left is 0, and update the status to 'Pending'

                            if (progressListControl.TimeLeft == "0 seconds")
                            {
                                if (progressListControl.Status == "In-Progress")
                                {
                                    // Update the database
                                    await UpdateBookingStatusToPendingAsync(progressListControl.BookingID, progressListControl.UnitID);

                                    // Check the booking status again
                                    await CheckBookingStatusReservedAsync(progressListControl.UnitID);

                                    // Reload unit and display in-progress bookings
                                    servicesForm.Load_Unit();
                                    await servicesForm.DisplayInProgress();
                                }
                                else if (progressListControl.Status == "Pending")
                                {
                                    // Reload unit and display in-progress bookings
                                    servicesForm.Load_Unit();
                                    await servicesForm.DisplayInProgress();
                                }
                            }
                            /*
                            if (progressListControl.TimeLeft == "0 seconds" && progressListControl.Status == "In-Progress")
                            {
                                // Update the database
                                await UpdateBookingStatusToPendingAsync(progressListControl.BookingID, progressListControl.UnitID);
                                //await CheckBookingStatusReservedAsync(progressListControl.UnitID);

                                servicesForm.Load_Unit();
                                await servicesForm.DisplayInProgress();
                                
                            }
                            else if (progressListControl.TimeLeft == "0 seconds" && progressListControl.Status == "Pending")
                            {
                                servicesForm.Load_Unit();
                                await servicesForm.DisplayInProgress();
                            }
                            */
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display a message, etc.)
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
                // Handle the exception as needed
            }
        }

        private async Task UpdateBookingStatusAsync(int bookingID, int unitID)
        {
            // Use a lock to ensure thread safety
            lock (updateBookingLockPending)
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open(); // Use synchronous Open here

                    string updateBookingQuery = @"UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID;";
                    //string updateUnitQuery = @"UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = (SELECT Unit_ID FROM Laundry_Bookings WHERE Booking_ID = @BookingID);";
                    string updateUnitQuery = @"UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = @UnitID;";

                    using (SqlCommand updateBookingCommand = new SqlCommand(updateBookingQuery, connect))
                    using (SqlCommand updateUnitCommand = new SqlCommand(updateUnitQuery, connect))
                    {
                        updateBookingCommand.Parameters.AddWithValue("@BookingID", bookingID);
                        //updateUnitCommand.Parameters.AddWithValue("@BookingID", bookingID);
                        updateUnitCommand.Parameters.AddWithValue("@UnitID", unitID);


                        // Execute the update queries within a transaction to ensure consistency
                        SqlTransaction transaction = connect.BeginTransaction();

                        try
                        {
                            updateBookingCommand.Transaction = transaction;
                            updateUnitCommand.Transaction = transaction;

                            updateBookingCommand.ExecuteNonQuery();
                            updateUnitCommand.ExecuteNonQuery();

                            // Commit the transaction if both updates are successful
                            transaction.Commit();
                            //servicesForm.Load_Unit();
                        }
                        catch (Exception)
                        {
                            // Handle exceptions and roll back the transaction if an error occurs
                            transaction.Rollback();
                            throw; // Rethrow the exception
                        }
                    }
                }
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

                        // Update Booking_Status to 'In-Progress' for each record
                        foreach (int bookingID in reservedBookingIDs)
                        {
                            using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Booking_ID = @BookingID", connect, transaction))
                            {
                                updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                updateBookingCmd.ExecuteNonQuery();
                            }
                        }

                        // Determine the unit status based on the presence of reserved bookings
                        int unitStatus = (reservedBookingIDs.Count > 0) ? 1 : 0;

                        // Update Unit_Status for the specified UnitID
                        using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID", connect, transaction))
                        {
                            updateUnitCmd.Parameters.AddWithValue("@UnitID", reservedunitID);
                            updateUnitCmd.Parameters.AddWithValue("@UnitStatus", unitStatus);
                            updateUnitCmd.ExecuteNonQuery();
                        }

                        // Commit the transaction if everything is successful
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Roll back the transaction if an error occurs
                        transaction.Rollback();
                        throw; // Rethrow the exception
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Release the semaphore in a finally block
                semaphore.Release();
            }
        }

        /*
        private async Task CheckReservedAsync(int reservedunitID)
        {
            // Use a lock to ensure thread safety
            lock (checkBookingLockReserved)
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    //await connect.OpenAsync();
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

                        // Update Booking_Status to 'In-Progress' for each record
                        foreach (int bookingID in reservedBookingIDs)
                        {
                            using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Booking_ID = @BookingID", connect, transaction))
                            {
                                updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                updateBookingCmd.ExecuteNonQuery();
                            }
                        }

                        // Determine the unit status based on the presence of reserved bookings
                        int unitStatus = (reservedBookingIDs.Count > 0) ? 1 : 0;

                        // Update Unit_Status for the specified UnitID
                        using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID", connect, transaction))
                        {
                            updateUnitCmd.Parameters.AddWithValue("@UnitID", reservedunitID);
                            updateUnitCmd.Parameters.AddWithValue("@UnitStatus", unitStatus);
                            updateUnitCmd.ExecuteNonQuery();
                        }

                        // Commit the transaction if everything is successful
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Roll back the transaction if an error occurs
                        transaction.Rollback();
                        throw; // Rethrow the exception
                    }
                }
            }
        }
        */

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

                    // Update Booking_Status to 'In-Progress' for each record
                    foreach (int bookingID in reservedBookingIDs)
                    {
                        using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Booking_ID = @BookingID", connect, transaction))
                        {
                            updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                            updateBookingCmd.ExecuteNonQuery();
                        }
                    }

                    // Determine the unit status based on the presence of reserved bookings
                    int unitStatus = (reservedBookingIDs.Count > 0) ? 1 : 0;

                    // Update Unit_Status for the specified UnitID
                    using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID", connect, transaction))
                    {
                        updateUnitCmd.Parameters.AddWithValue("@UnitID", UnitID);
                        updateUnitCmd.Parameters.AddWithValue("@UnitStatus", unitStatus);
                        updateUnitCmd.ExecuteNonQuery();
                    }

                    // Commit the transaction if everything is successful
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Roll back the transaction if an error occurs
                    transaction.Rollback();
                    throw; // Rethrow the exception
                }
            }
        }


        /*
        public async Task UpdateBookingStatusToPendingAsync(int bookingID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    await connect.OpenAsync();

                    // Update the Booking Status to 'Pending'
                    string updateBookingQuery = @"UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID;";

                    // Update the Unit_Status to 0
                    string updateUnitQuery = @"UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = (SELECT Unit_ID FROM Laundry_Bookings WHERE Booking_ID = @BookingID);";

                    using (SqlCommand updateBookingCommand = new SqlCommand(updateBookingQuery, connect))
                    using (SqlCommand updateUnitCommand = new SqlCommand(updateUnitQuery, connect))
                    {
                        updateBookingCommand.Parameters.AddWithValue("@BookingID", bookingID);
                        updateUnitCommand.Parameters.AddWithValue("@BookingID", bookingID);

                        // Execute the update queries within a transaction to ensure consistency
                        SqlTransaction transaction = connect.BeginTransaction();

                        try
                        {
                            updateBookingCommand.Transaction = transaction;
                            updateUnitCommand.Transaction = transaction;

                            await updateBookingCommand.ExecuteNonQueryAsync();
                            await updateUnitCommand.ExecuteNonQueryAsync();

                            // Commit the transaction if both updates are successful
                            transaction.Commit();
                            //Load_Unit();
                            //await LoadProgressAsync();
                        }
                        catch (Exception)
                        {
                            // Handle exceptions and roll back the transaction if an error occurs
                            transaction.Rollback();
                            throw; // Rethrow the exception
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display a message, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                // You might want to throw the exception again here if you want to propagate it further
                // throw;
            }
        }


        // another code with no try and catch
        
        private async Task<List<In_Progress_Class>> RetrieveProgressListAsync()
        {
            List<In_Progress_Class> progress = new List<In_Progress_Class>();

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

         public async Task LoadProgressAsync(FlowLayoutPanel flow_progress)
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
                await Task.Delay(2000); // Update every second

                // Reload the progress list and update the UI
                progress = await RetrieveProgressListAsync();

                foreach (Control control in flow_progress.Controls)
                {
                    if (control is ucProgressList_Control progressListControl)
                    {
                        progressListControl.UpdateTimeLeft();
                        
                        // Check if the time left is 0, and update the status to 'Pending'
                        if (progressListControl.TimeLeft == "0 seconds" && progressListControl.Status == "Pending")
                        {
                            // Update the database
                            await UpdateBookingStatusToPendingAsync(progressListControl.BookingID);
                        }
                    }
                }
            }
        }
        public async Task UpdateBookingStatusToPendingAsync(int bookingID)
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                await connect.OpenAsync();

                // Update the Booking Status to 'Pending'
                string updateBookingQuery = @"UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID;";

                // Update the Unit_Status to 0
                string updateUnitQuery = @"UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = (SELECT Unit_ID FROM Laundry_Bookings WHERE Booking_ID = @BookingID);";

                using (SqlCommand updateBookingCommand = new SqlCommand(updateBookingQuery, connect))
                using (SqlCommand updateUnitCommand = new SqlCommand(updateUnitQuery, connect))
                {
                    updateBookingCommand.Parameters.AddWithValue("@BookingID", bookingID);
                    updateUnitCommand.Parameters.AddWithValue("@BookingID", bookingID);

                    // Execute the update queries within a transaction to ensure consistency
                    SqlTransaction transaction = connect.BeginTransaction();

                    try
                    {
                        updateBookingCommand.Transaction = transaction;
                        updateUnitCommand.Transaction = transaction;

                        await updateBookingCommand.ExecuteNonQueryAsync();
                        await updateUnitCommand.ExecuteNonQueryAsync();

                        // Commit the transaction if both updates are successful
                        transaction.Commit();
                        //Load_Unit();
                        //await LoadProgressAsync();
                    }
                    catch (Exception)
                    {
                        // Handle exceptions and roll back the transaction if an error occurs
                        transaction.Rollback();
                        throw; // Rethrow the exception
                    }
                }
            }
        }
        */







    }
}
