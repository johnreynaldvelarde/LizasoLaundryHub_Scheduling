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

namespace Lizaso_Laundry_Hub.Settings_Module
{
    public partial class LaundryUnit_Configuration_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private int unitID, unitStatus, deletedUnitID;
        private string unitName;

        public LaundryUnit_Configuration_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
        }

        public void DisplayUnit()
        {
            if (tab_Units.SelectedTab == tabPage1)
            {
                getData.Get_UnitTable(grid_unit_view);
                getData.Get_CountAllUnitStatus(lblAvailable, lblOccupied, lblNotAvaialble);
            }
            else if (tab_Units.SelectedTab == tabPage2)
            {
                getData.Get_UnitDeleted(grid_unit_archive);
                getData.Get_CountAllUnitStatus(lblAvailable, lblOccupied, lblNotAvaialble);
            }
        }

        private void LaundryUnit_Configuration_Form_Load(object sender, EventArgs e)
        {
            DisplayUnit();
        }

        private int ConvertUnitStatusToInt(string status)
        {
            switch (status)
            {
                case "Available":
                    return 0;
                case "Occupied":
                    return 1;
                case "Not Available":
                    return 3;
                default:
                    // Handle unknown status or return a default value
                    return -1;
            }
        }

        private void tab_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUnit();
        }

        
        private void grid_unit_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_unit = grid_unit_view.Columns[e.ColumnIndex].Name;

            if (column_unit == "Edit")
            {
                Add_Unit_Form frm = new Add_Unit_Form(this);
                frm.lblUnitTitle.Text = "Update this unit information";
                frm.btnSave.Text = "Update";
                frm.txt_UnitName.Enabled = true;
                frm.getunitID = unitID;
                frm.txt_UnitName.Text = unitName;
                frm.ShowDialog();
            }
            else if (column_unit == "Delete")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this unit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_Delete_Unit(unitID);
                    DisplayUnit();
                }
            }

        }

        private void grid_unit_view_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_unit_view.CurrentRow != null)
            {
                int i = grid_unit_view.CurrentRow.Index;

                if (int.TryParse(grid_unit_view[1, i].Value.ToString(), out int selectUnitID))
                {
                    unitID = selectUnitID;
                    unitName = grid_unit_view[2, i].Value.ToString();
                    unitStatus = ConvertUnitStatusToInt(grid_unit_view[3, i].Value.ToString());
                }
            }
        }

        // to recycle the unit that deleted
        private void grid_unit_archive_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_delete = grid_unit_archive.Columns[e.ColumnIndex].Name;

            if (column_delete == "Recycle")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to recycle this unit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_Recycle_Unit(deletedUnitID);
                    DisplayUnit();
                }
            }
        }

        private void grid_unit_archive_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_unit_archive.CurrentRow != null)
            {
                int i = grid_unit_archive.CurrentRow.Index;

                if (int.TryParse(grid_unit_archive[1, i].Value.ToString(), out int d_UnitID))
                {
                    deletedUnitID = d_UnitID;
                }
            }
        }

        private void btn_NewUnit_Click(object sender, EventArgs e)
        {
            Services_Form servicesFormInstance = new Services_Form();

            // Now create an instance of Add_Unit_Form
            Add_Unit_Form frm = new Add_Unit_Form(this);
            frm.ShowDialog();
        }
    }
}
