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
        private int deliveryCount;
        private bool updatingCheckboxes = false;

        public Delivery_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
        }
        /*
        private void ResetCounting()
        {
            deliveryCount = 0;
            UpdateRowNumbers();
        }
        private void UpdateRowNumbers()
        {
            foreach (DataGridViewRow row in grid_delivery_view.Rows)
            {
                deliveryCount++;
                row.Cells[0].Value = deliveryCount;
            }
        }
        private void UpdateRowNumbersFromIndex(int startIndex)
        {
            for (int i = startIndex; i < grid_delivery_view.Rows.Count; i++)
            {
                deliveryCount++;
                grid_delivery_view.Rows[i].Cells[0].Value = deliveryCount;
            }
        }
        */

        public void DisplayDeliveryStatus()
        {
            //getData.Get_DashboardDeliveryList(grid_delivery_view, ckInTransit.Checked, ckCompleted.Checked, ckCancel.Checked);
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

        /*
        private void ckInTransit_CheckedChanged(object sender, EventArgs e)
        {
            if (ckInTransit.Checked)
            {
                ResetCounting();
                DisplayDeliveryStatus();
            }
            else
            {
                DisplayDeliveryStatus();
            }
            /*
            if (ckInTransit.Checked)
            {
                ResetCounting();
                DisplayDeliveryStatus();
            }
            //DisplayDeliveryStatus();
            
        }

        private void ckCompleted_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingCheckboxes && ckCompleted.Checked)
            {
                updatingCheckboxes = true;
                ResetCounting();
                DisplayDeliveryStatus();
                updatingCheckboxes = false;
            }
            else
            {
                DisplayDeliveryStatus();
            }
            /*
            if (ckCompleted.Checked)
            {
                ResetCounting();
                DisplayDeliveryStatus();
            }
            //DisplayDeliveryStatus();
            I
        }

        private void ckCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingCheckboxes && ckCancel.Checked)
            {
                updatingCheckboxes = true;
                ResetCounting();
                DisplayDeliveryStatus();
                updatingCheckboxes = false;
            }
            else
            {
                DisplayDeliveryStatus();
            }
            /*
            if (ckCancel.Checked)
            {
                ResetCounting();
                DisplayDeliveryStatus();
            }

        }
            */

        private void grid_delivery_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_delivery_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
            /*
            if (!updatingCheckboxes)
            {
                deliveryCount = e.RowIndex + 1;
                grid_delivery_view.Rows[e.RowIndex].Cells[0].Value = deliveryCount;
            }
            
            if (!ckInTransit.Checked && !ckCompleted.Checked && !ckCancel.Checked)
            {
                deliveryCount = e.RowIndex + 1;
                grid_delivery_view.Rows[e.RowIndex].Cells[0].Value = deliveryCount;
            }
            /*
            int count = 0;
            foreach (DataGridViewRow row in grid_delivery_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
            */
        }

        private void btn_ClickDeliveryList_Click(object sender, EventArgs e)
        {

        }
    }
}
