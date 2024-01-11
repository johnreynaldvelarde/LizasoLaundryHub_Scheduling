using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Toolkit;

namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    public partial class Delivery_Widget_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private int getDeliveryID;

        public Delivery_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
        }

        public void DisplayDeliveryStatus()
        {
            getData.Get_DashboardDeliveryList(grid_delivery_view);
        }

        private void Delivery_Widget_Form_Load(object sender, EventArgs e)
        {
            DisplayDeliveryStatus();
        }

        private void grid_delivery_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_delivery = grid_delivery_view.Columns[e.ColumnIndex].Name;

            if (column_delivery == "Cancel")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this delivery?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_DeliveryToCancel(getDeliveryID);
                    DisplayDeliveryStatus();
                }
            }
            else if (column_delivery == "Complete")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to complete this delivery?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_DeliveryToCompleted(getDeliveryID);
                    DisplayDeliveryStatus();
                }
            }
        }

        private void grid_delivery_view_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_delivery_view.CurrentRow != null)
            {
                int i = grid_delivery_view.CurrentRow.Index;

                if (int.TryParse(grid_delivery_view[1, i].Value.ToString(), out int getdeliveryID))
                {
                    getDeliveryID = getdeliveryID;
                }
            }
        }

        private void grid_delivery_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_delivery_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void btn_ClickDeliveryList_Click(object sender, EventArgs e)
        {
            View_DeliveryList_Form delivery = new View_DeliveryList_Form();
            delivery.Show();
        }
    }
}
