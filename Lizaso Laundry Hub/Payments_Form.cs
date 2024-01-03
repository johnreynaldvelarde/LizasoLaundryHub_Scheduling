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

namespace Lizaso_Laundry_Hub
{
    public partial class Payments_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private int bookingID, unitID, customerID;
        private string customerName, serviceType;

        public Payments_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        public void DisplayInPendingList()
        {
            if (tab_Payments.SelectedTab == tabPage1)
            {
                 getData.Get_BookingPending(grid_pending_view);

            }
            else if (tab_Payments.SelectedTab == tabPage2)
            {
                //getData.Get_BookingReserved(grid_pending_view);

            }
            else
            {
                MessageBox.Show("Wala wag kang excited");
            }
           
        }

        private void Payments_Form_Load(object sender, EventArgs e)
        {
            DisplayInPendingList();
        }

        private void tab_Payments_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayInPendingList();
        }

        private void grid_pending_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_pending_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void grid_pending_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_pending = grid_pending_view.Columns[e.ColumnIndex].Name;

            if (column_pending == "Pay")
            {
                Payment_Details_Form frm = new Payment_Details_Form(this);
                frm.CustomerID = customerID;
                frm.UnitID = unitID;
                frm.BookingID = bookingID;
                frm.txt_CustomerName.Text = customerName;
                frm.txt_ServiceType.Text = serviceType;
                frm.ShowDialog();
            }
            else if (column_pending == "Cancel")
            {
                // Handle delete action if needed
            }
        }

        private void grid_pending_view_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_pending_view.CurrentRow != null)
            {
                int rowIndex = grid_pending_view.CurrentRow.Index;

                if (int.TryParse(grid_pending_view[1, rowIndex].Value.ToString(), out int selectedBookingID))
                {
                    // Assuming the other columns contain the relevant data
                    int selectedUnitID = Convert.ToInt32(grid_pending_view[2, rowIndex].Value);
                    int selectedCustomerID = Convert.ToInt32(grid_pending_view[3, rowIndex].Value);
                    string selectedCustomerName = grid_pending_view[4, rowIndex].Value.ToString();
                    string selectedServiceType = grid_pending_view[6, rowIndex].Value.ToString();

                    // Assign the values to the class properties
                    bookingID = selectedBookingID;
                    unitID = selectedUnitID;
                    customerID = selectedCustomerID;
                    customerName = selectedCustomerName;
                    serviceType = selectedServiceType;

                    // Now you can use bookingID, unitID, customerID, customerName, and serviceType as needed
                }
            }
        }
    }
}
