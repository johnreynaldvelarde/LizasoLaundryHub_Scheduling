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
using static Lizaso_Laundry_Hub.Dashboard_Widget.Pending_Widget_Form;

namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    public partial class Pending_Widget_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private int bookingID, unitID, customerID, getTransactionID;
        private string customerName, serviceType, weight;

        public Pending_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
        }

        private void grid_pending_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_pending = grid_pending_view.Columns[e.ColumnIndex].Name;

            if (column_pending == "Paynow")
            {
                Payments_Form paymentsInstance = new Payments_Form();
                Pending_Widget_Form widgetInstance = new Pending_Widget_Form();

                Payment_Details_Form frm = new Payment_Details_Form(paymentsInstance, this);
                frm.CustomerID = customerID;
                frm.UnitID = unitID;
                frm.BookingID = bookingID;
                frm.txt_CustomerName.Text = customerName;
                frm.txt_ServiceType.Text = serviceType;
                frm.setWeight = weight;
                frm.ShowDialog();
                
            }
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
        
        public void DisplayCustomerPending() 
        {
            getData.Get_BookingPending(grid_pending_view);
        }
        
        private void Pending_Widget_Form_Load(object sender, EventArgs e)
        {
            DisplayCustomerPending();
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
    }
}
