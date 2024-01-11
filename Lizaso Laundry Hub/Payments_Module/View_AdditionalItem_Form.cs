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

namespace Lizaso_Laundry_Hub.Payments_Module
{
    public partial class View_AdditionalItem_Form : KryptonForm
    {
        private Get_Data_Class getData;
        public int setTransctionID;

        public View_AdditionalItem_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void View_AdditionalItem_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void CheckIfAdditionalPayment(int transactionID, DataGridView gridAdditionalItem)
        {
            bool hasAdditionalItems = getData.Get_AdditionalItems(transactionID, gridAdditionalItem);

            if (hasAdditionalItems)
            {
               
            }
            else
            {
                MessageBox.Show("This transaction does not have additional payments.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
        }

        private void View_AdditionalItem_Form_Load(object sender, EventArgs e)
        {
            CheckIfAdditionalPayment(setTransctionID, grid_additional_view);
            CountAmount();
        }

        private void grid_additional_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_additional_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public void CountAmount()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in grid_additional_view.Rows)
            {
                if (row.Cells[3].Value != null && decimal.TryParse(row.Cells[3].Value.ToString(), out decimal amount))
                {
                    totalAmount += amount;
                }
            }

            LabelAmount.Text = $"PHP {totalAmount}";
        }
    }
}
