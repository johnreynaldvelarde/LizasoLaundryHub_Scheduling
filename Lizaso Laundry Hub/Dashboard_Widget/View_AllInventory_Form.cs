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

namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    public partial class View_AllInventory_Form : KryptonForm
    {
        private Get_Data_Class getData;

        public View_AllInventory_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void View_AllInventory_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void View_AllInventory_Form_Load(object sender, EventArgs e)
        {
            getData.Get_AllItem(grid_item_view);
            CountQyt();
        }

        private void grid_item_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_item_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public void CountQyt()
        {
            decimal totalQyt = 0;

            foreach (DataGridViewRow row in grid_item_view.Rows)
            {
                if (row.Cells[2].Value != null && int.TryParse(row.Cells[2].Value.ToString(), out int qyt))
                {
                    totalQyt += qyt;
                }
            }

            LabelTotal.Text = $"{totalQyt}";
        }
    }
}
