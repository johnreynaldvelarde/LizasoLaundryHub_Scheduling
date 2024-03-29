﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Runtime.InteropServices.ComTypes;

namespace Lizaso_Laundry_Hub
{
    public partial class Schedule_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;
        private int counts = 0;

        private int selectedBookingID;
        private int selectedUnitID;
        private string getCustomerName;

        public Schedule_Form()
        {
            InitializeComponent();
            
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
            activityLogger = new Activity_Log_Class();
        }

        public void DisplayInProgressandReserved()
        {
            if(tab_Schedule.SelectedTab == tabPage1)
            {
                getData.Get_BookingProgress(grid_progress_view);

            }
            else if (tab_Schedule.SelectedTab == tabPage2)
            {
                getData.Get_BookingReserved(grid_reserved_view);

            }
        }

        private void Schedule_Form_Load(object sender, EventArgs e)
        {
            DisplayInProgressandReserved();
        }

        public void  Grid_ProgressCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_progress_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }


        public void Grid_ReservedCount()
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_reserved_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }


        private void grid_progress_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                int timeRangeColumnIndex = 8;

                if (e.ColumnIndex == timeRangeColumnIndex)
                {
                    string cellValue = e.Value?.ToString();

                    if (cellValue != null)
                    {
                        if (cellValue.StartsWith("Start Time:") || cellValue.StartsWith("End Time:"))
                        {
                            // Set red color for "Start Time:" and "End Time:"
                            e.CellStyle.ForeColor = Color.Red;
                        }
                    }
                }
                else if (e.ColumnIndex == 0)  
                {
                    // e.CellStyle.ForeColor = Color.Green;  

                    // e.Value = counts + 1;
                }
                else if (e.ColumnIndex == 11)  
                {
                    
                }

                Grid_ProgressCount();
            }
        }

        private void tab_Schedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayInProgressandReserved();
        }

        private void grid_reserved_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int timeRangeColumnIndex = 8;

                if (e.ColumnIndex == timeRangeColumnIndex)
                {
                    string cellValue = e.Value?.ToString();

                    if (cellValue != null)
                    {
                        if (cellValue.StartsWith("Reservation start time:") || cellValue.StartsWith("Reservation end time:"))
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                    }
                }
                else if (e.ColumnIndex == 0)  
                {
                    // e.CellStyle.ForeColor = Color.Green;  // Change the color as needed

                    //e.Value = counts + 1;
                }
                Grid_ReservedCount();
            }
        }

        private void grid_progress_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_progress = grid_progress_view.Columns[e.ColumnIndex].Name;

            if (column_progress == "Edit")
            {
                
            }
            else if (column_progress == "Cancel")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this booking?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_BookingStatusToCanceled(selectedBookingID, selectedUnitID);
                    UserActivityLog(getCustomerName);
                    DisplayInProgressandReserved();
                }
            }
        }

        public void UserActivityLog(string customerName)
        {
            string activityType = "Canceled";
            string CancelDescription = $"{customerName}'s in-progress laundry booking has been canceled as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, CancelDescription);
        }

        private void grid_progress_view_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_progress_view.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = grid_progress_view.SelectedRows[0];

                if (selectedRow.Cells.Count > 1 &&
                    int.TryParse(selectedRow.Cells[1].Value.ToString(), out selectedBookingID) &&
                    int.TryParse(selectedRow.Cells[2].Value.ToString(), out selectedUnitID))
                    
                {
                    getCustomerName = selectedRow.Cells[4].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Unable to retrieve Booking ID and Unit ID from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
