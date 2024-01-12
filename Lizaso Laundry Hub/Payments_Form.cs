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
using Lizaso_Laundry_Hub.Dashboard_Widget;
using Lizaso_Laundry_Hub.Payments_Module;
using static Lizaso_Laundry_Hub.Payments_Form;

namespace Lizaso_Laundry_Hub
{
    public partial class Payments_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;
        private int bookingID, unitID, customerID, getTransactionID;
        private string customerName, serviceType, weight;

        public Payments_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
            activityLogger = new Activity_Log_Class();
        }

        public void DisplayInPendingList()
        {
            if (grid_transaction_view.SelectedTab == tabPage1)
            {
                 getData.Get_BookingPending(grid_pending_view);

            }
            else if (grid_transaction_view.SelectedTab == tabPage2)
            {
                getData.Get_TransactionHistory(grid_transaction_history_view);
            }
            else
            {
              
            }
        }

        private void Payments_Form_Load(object sender, EventArgs e)
        {
            DisplayInPendingList();
        }

        private void grid_transaction_history_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_transaction_history_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void grid_transaction_history_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_history = grid_transaction_history_view.Columns[e.ColumnIndex].Name;

            if (column_history == "View")
            {
                View_AdditionalItem_Form item = new View_AdditionalItem_Form();
                item.setTransctionID = getTransactionID;
                item.Show();
            }
        }

        private void grid_transaction_history_view_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_transaction_history_view.CurrentRow != null)
            {
                int rowIndex = grid_transaction_history_view.CurrentRow.Index;

                if (int.TryParse(grid_transaction_history_view[1, rowIndex].Value.ToString(), out int selectedTransactionID))
                {
                    getTransactionID = selectedTransactionID;
                }
            }
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
                Pending_Widget_Form widgetInstance = new Pending_Widget_Form();

                Payment_Details_Form frm = new Payment_Details_Form(this, widgetInstance);
                frm.CustomerID = customerID;
                frm.UnitID = unitID;
                frm.BookingID = bookingID;
                frm.txt_CustomerName.Text = customerName;
                frm.txt_ServiceType.Text = serviceType;
                frm.setWeight = weight;
                frm.ShowDialog();
            }
            else if (column_pending == "Cancel")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this pending payments?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_CancelPendingPayments(bookingID);
                    UserActivityLog(customerName);
                    DisplayInPendingList();
                }
            }
        }

        public bool UserActivityLog(string customerName)
        {
            string activityType = "Canceled";
            string CancelDescription = $"{customerName}'s pending laundry booking has been canceled as of {DateTime.Now}. Any associated payments have been voided.";
            activityLogger.LogActivity(activityType, CancelDescription);

            return true;
        }

        private void grid_pending_view_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_pending_view.CurrentRow != null)
            {
                int rowIndex = grid_pending_view.CurrentRow.Index;

                if (int.TryParse(grid_pending_view[1, rowIndex].Value.ToString(), out int selectedBookingID))
                {
                    int selectedUnitID = Convert.ToInt32(grid_pending_view[2, rowIndex].Value);
                    int selectedCustomerID = Convert.ToInt32(grid_pending_view[3, rowIndex].Value);
                    string selectedCustomerName = grid_pending_view[4, rowIndex].Value.ToString();
                    string selectedServiceType = grid_pending_view[6, rowIndex].Value.ToString();
                    string selectedWeight = grid_pending_view[7, rowIndex].Value.ToString();


                    bookingID = selectedBookingID;
                    unitID = selectedUnitID;
                    customerID = selectedCustomerID;
                    customerName = selectedCustomerName;
                    serviceType = selectedServiceType;
                    weight = selectedWeight;
                }
            }
        }
    }
}
