using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;

namespace Lizaso_Laundry_Hub
{
    public partial class Services_Form : KryptonForm
    {
        private DB_Connection database = new DB_Connection();
        private RealTime_Data_Class realTime;
       
        private Get_Data_Class getData;
        
        private Select_Unit_Form selectUnitForm;

        public Services_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            realTime = new RealTime_Data_Class(this);
            Load_Unit();
            
        }
        public void DisplayReserved()
        {
            if (tab_side.SelectedTab == tabPage2)
            {
                Load_Reserved();
            }
        }
        public async Task DisplayInProgress()
        {
            await realTime.LoadProgressAsync(flow_panel_progress);
        }

        private async void Services_Form_Load(object sender, EventArgs e)
        {
            try
            {
                await DisplayInProgress();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error na kailangan ayusin");
            }
        }

        private void UcUnitControl_SelectButtonClicked(object sender, EventArgs e)
        {
            // Disable the current form
            this.Enabled = false;

            // Create and show SelectUnit_Form
            selectUnitForm = new Select_Unit_Form(this);
            selectUnitForm.FormClosed += SelectUnitForm_FormClosed;
            selectUnitForm.Show();
        }

        private void SelectUnitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Re-enable the current form when SelectUnit_Form is closed
            this.Enabled = true;

            // Dispose of the form to release resources
            selectUnitForm.Dispose();
        }
       
        public void Load_Unit()
        {
            try
            {
                flow_panel_unit.Controls.Clear();

                List<Unit_Class> units = getData.Get_RetrieveUnits();

                foreach (var unit in units)
                {
                    ucUnit_Control unitControl = new ucUnit_Control(unit);
                    flow_panel_unit.Controls.Add(unitControl);
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception
                Console.WriteLine($"An error occurred in Load_Unit: {ex.Message}");
            }
        }

        public void Load_Reserved()
        {
            try
            {
                flow_panel_reserved.Controls.Clear();

                List<In_Reserved_Class> reserved = getData.Get_RetrieveLaundryBookingsReserved();

                foreach (var reve in reserved)
                {
                    ucReservedList_Control reservedControl = new ucReservedList_Control(reve);
                    flow_panel_reserved.Controls.Add(reservedControl);
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception
                Console.WriteLine($"An error occurred in Load_Unit: {ex.Message}");
            }
        }


        /*
        public async Task Load_ProgressAsync()
        {
            flow_panel_progress.Controls.Clear();

            List<In_Progress_Class> progress = RetrieveProgressList();

            foreach (var prog in progress)
            {
                ucProgressList_Control progressControl = new ucProgressList_Control(prog);
                flow_panel_progress.Controls.Add(progressControl);
            }

            // Update the UI periodically
            while (true)
            {
                await Task.Delay(1000); // Update every second

                // Iterate through each control and update the time left
                foreach (Control control in flow_panel_progress.Controls)
                {
                    if (control is ucProgressList_Control progressListControl)
                    {
                        progressListControl.UpdateTimeLeft(); // Update the time left in each control
                    }
                }
            }
        }



        /*
        public void Load_Progress()
        {
            flow_panel_progress.Controls.Clear();

            List<In_Progress_Class> progress = RetrieveProgressList();

            foreach (var prog in progress)
            {
                ucProgressList_Control progressControl = new ucProgressList_Control(prog);
                flow_panel_progress.Controls.Add(progressControl);
            }
        }
        

        private List<In_Progress_Class> RetrieveProgressList()
        {
            List<In_Progress_Class> progress = new List<In_Progress_Class>();

            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            In_Progress_Class pen = new In_Progress_Class()
                            {
                                BookingID = (int)reader["Booking_ID"],
                                CustomerID = (int)reader["Customer_ID"],
                                UnitID = (int)reader["Unit_ID"],
                                Customer_Name = reader["Customer_Name"].ToString(),
                                Unit_Name = reader["Unit_Name"].ToString(),
                                ServiceType = reader["Services_Type"].ToString(),
                                //TimeLeft = reader["TimeLeft"].ToString(),
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
        */

        /*
        public async Task LoadProgressAsync()
        {
            flow_panel_progress.Controls.Clear();

            List<In_Progress_Class> progress = await RetrieveProgressListAsync();

            foreach (var prog in progress)
            {
                ucProgressList_Control progressControl = new ucProgressList_Control(prog);
                flow_panel_progress.Controls.Add(progressControl);
            }

            // Update the UI periodically
            while (true)
            {
                await Task.Delay(1000); // Update every second

                // Reload the progress list and update the UI
                progress = await RetrieveProgressListAsync();

                foreach (Control control in flow_panel_progress.Controls)
                {
                    if (control is ucProgressList_Control progressListControl)
                    {
                        progressListControl.UpdateTimeLeft();
                    }
                }
            }
        }
        */

        /*
        public async Task LoadProgressAsync()
        {
            flow_panel_progress.Controls.Clear();

            List<In_Progress_Class> progress = await RetrieveProgressListAsync();

            foreach (var prog in progress)
            {
                ucProgressList_Control progressControl = new ucProgressList_Control(prog);
                flow_panel_progress.Controls.Add(progressControl);
            }

            // Update the UI periodically
            while (true)
            {
                await Task.Delay(5000); // Update every second

                // Reload the progress list and update the UI
                progress = await RetrieveProgressListAsync();

                foreach (Control control in flow_panel_progress.Controls)
                {
                    if (control is ucProgressList_Control progressListControl)
                    {
                        progressListControl.UpdateTimeLeft();

                        // Check if the time left is 0, and update the status to 'Pending'
                        if (progressListControl.TimeLeft == "0 minutes" && progressListControl.Status == "In-Progress")
                        {
                            // Update the database
                            await realTime.UpdateBookingStatusToPendingAsync(progressListControl.BookingID);
                           
                        }
                    }
                }
            }
        }
        //await UpdateBookingStatusToPendingAsync(progressListControl.BookingID);
        //await UpdateBookingStatusReservedAsync(progressListControl.UnitID);

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
        */

        /*
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

        // method when the timeleft become 0 minutes with status In-Progress its change it to Pending
        private async Task UpdateBookingStatusToPendingAsync(int bookingID)
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
                        Load_Unit();
                       // await LoadProgressAsync();
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

        /*
        // method when the timeleft become 0 minutes with status In-Progress its change it to Pending
        private async Task UpdateBookingStatusToPendingAsync(int bookingID)
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                await connect.OpenAsync();

                string query = @"UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID;";

                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@BookingID", bookingID);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        */



        /*
        private List<In_Reserved_Class> RetrieveReservedList()
        {
            List<In_Reserved_Class> reserved = new List<In_Reserved_Class>();

            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            In_Reserved_Class rev = new In_Reserved_Class()
                            {
                                BookingID = (int)reader["Booking_ID"],
                                CustomerID = (int)reader["Customer_ID"],
                                UnitID = (int)reader["Unit_ID"],
                                Customer_Name = reader["Customer_Name"].ToString(),
                                Unit_Name = reader["Unit_Name"].ToString(),
                                ServiceType = reader["Services_Type"].ToString(),
                                //TimeLeft = Math.Max((int)reader["TimeLeft"], 0).ToString(),
                                Status = reader["Status"].ToString()
                            };
                            reserved.Add(rev);
                        }
                    }
                }

            }
            return reserved;
        }
        */

        private void btnReserve_Click(object sender, EventArgs e)
        {
            Add_Reserved_Form frm = new Add_Reserved_Form(this);
            frm.ShowDialog();
        }

        private void tab_side_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayReserved();
        }

        private bool isPanel1Moved = false;

        private void btn_PanelMove_Click(object sender, EventArgs e)
        {
            if (isPanel1Moved)
            {
                splitContainer1.SplitterDistance = 960;
            }
            else
            {
                splitContainer1.SplitterDistance = splitContainer1.Width;
            }

            isPanel1Moved = !isPanel1Moved;
        }
    }
}
