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
using Calendar.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Lizaso_Laundry_Hub
{
    public partial class Schedule_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private int counts = 0;

        private int selectedBookingID;
        private int selectedUnitID;

        public Schedule_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
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
            else if (tab_Schedule.SelectedTab == tabPage3)
            {
                //MessageBox.Show("ANG MGA TAE AY LUMILIPAD");
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
            /*

            if (e.RowIndex >= 0)
            {
                // Adjust the column index based on your actual column index for the "Start Time" and "End Time"
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
                else if (e.ColumnIndex == 0)  // Assuming count is in the first column
                {
                    // Set formatting for the count column
                    // e.CellStyle.ForeColor = Color.Green;  // Change the color as needed

                    // Set the count value based on the existing count
                    //e.Value = counts + 1;
                }
                Grid_ProgressCount();
            }
            */

            if (e.RowIndex >= 0)
            {
                // Adjust the column index based on your actual column index for the "Start Time" and "End Time"
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
                else if (e.ColumnIndex == 0)  // Assuming count is in the first column
                {
                    // Set formatting for the count column
                    // e.CellStyle.ForeColor = Color.Green;  // Change the color as needed

                    // Set the count value based on the existing count
                    // e.Value = counts + 1;
                }
                else if (e.ColumnIndex == 11)  // Assuming the button column is at index 9
                {
                    
                }

                // Assuming Grid_ProgressCount is a method to update some count in your grid
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
                // Adjust the column index based on your actual column index for the "Start Time" and "End Time"
                int timeRangeColumnIndex = 8;

                if (e.ColumnIndex == timeRangeColumnIndex)
                {
                    string cellValue = e.Value?.ToString();

                    if (cellValue != null)
                    {
                        if (cellValue.StartsWith("Reservation start time:") || cellValue.StartsWith("Reservation end time:"))
                        {
                            // Set red color for "Start Time:" and "End Time:"
                            e.CellStyle.ForeColor = Color.Red;
                        }
                    }
                }
                else if (e.ColumnIndex == 0)  // Assuming count is in the first column
                {
                    // Set formatting for the count column
                    // e.CellStyle.ForeColor = Color.Green;  // Change the color as needed

                    // Set the count value based on the existing count
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
                    DisplayInProgressandReserved();
                }
            }
        }

        private void grid_progress_view_SelectionChanged(object sender, EventArgs e)
        {
            // Check if there's a selected row
            if (grid_progress_view.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = grid_progress_view.SelectedRows[0];

                // Assuming the Booking_ID and Unit_ID columns are at index 0 and 1, respectively
                if (selectedRow.Cells.Count > 1 &&
                    int.TryParse(selectedRow.Cells[1].Value.ToString(), out selectedBookingID) &&
                    int.TryParse(selectedRow.Cells[2].Value.ToString(), out selectedUnitID))
                {
                    // Now selectedBookingID and selectedUnitID have the values you need
                }
                else
                {
                    // Handle the case where parsing fails
                    MessageBox.Show("Unable to retrieve Booking ID and Unit ID from the selected row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
