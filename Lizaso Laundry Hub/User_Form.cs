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
    public partial class User_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private int u_userID;
        private string u_username;
        private byte u_services, u_schedule, u_customer, u_payments, u_user, u_inventory, u_settings;

        public User_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        private void btn_CreateNewUser_Click(object sender, EventArgs e)
        {
            Add_User_Form frm = new Add_User_Form(this);
            frm.ShowDialog();
        }

        public void  DisplayUserView()
        {
            if (tab_User.SelectedTab == tabPage2)
            {
                getData.Get_RegularUserAndPermissions(grid_regular_user);
            }
            else if (tab_User.SelectedTab == tabPage1)
            {
                //getData.Get_GuestsCustomer(grid_guest_customer);
            }
            else if (tab_User.SelectedTab == tabPage3)
            {
                getData.Get_AllActivityLog(grid_view_activity);
            }
            else
            {

            }
        }

        private void User_Form_Load(object sender, EventArgs e)
        {
            DisplayUserView();
        }

        private void tab_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUserView();
        }

        private void grid_regular_user_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_user = grid_regular_user.Columns[e.ColumnIndex].Name;

            if (column_user == "Edit2")
            {
                Add_User_Form frm = new Add_User_Form(this);
                frm.btnSave.Text = "Update";
                frm.lblUserTitle.Text = "Update User and Permissions";
                frm.u_userID = u_userID;
                frm.txt_UserName.Text = u_username;
                frm.rdRegularUser.Checked = true;
                frm.ckAvailableServices.Checked = (u_services == 1);
                frm.ckSchedule.Checked = (u_schedule == 1);
                frm.ckCustomerManage.Checked = (u_customer == 1);
                frm.ckPayments.Checked = (u_payments == 1);
                frm.ckUserManage.Checked = (u_user == 1);
                frm.ckInventory.Checked = (u_inventory == 1);
                frm.ckSettings.Checked = (u_settings == 1);
                frm.ShowDialog();
            }
            else if (column_user == "Delete2")
            {
                // Handle delete action if needed
            }
        }

        private void grid_regular_user_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_regular_user.CurrentRow != null)
            {
                int i = grid_regular_user.CurrentRow.Index;

                if (int.TryParse(grid_regular_user[1, i].Value.ToString(), out int userId))
                {
                    u_userID = userId;

                    // Assuming you have other columns in your DataGridView for username, services, schedule, etc.
                    u_username = grid_regular_user[2, i].Value.ToString();
                    u_services = Convert.ToByte(grid_regular_user[3, i].Value.ToString() == "Yes");
                    u_schedule = Convert.ToByte(grid_regular_user[4, i].Value.ToString() == "Yes");
                    u_customer = Convert.ToByte(grid_regular_user[5, i].Value.ToString() == "Yes");
                    u_payments = Convert.ToByte(grid_regular_user[6, i].Value.ToString() == "Yes");
                    u_user = Convert.ToByte(grid_regular_user[7, i].Value.ToString() == "Yes");
                    u_inventory = Convert.ToByte(grid_regular_user[8, i].Value.ToString() == "Yes");
                    u_settings = Convert.ToByte(grid_regular_user[9, i].Value.ToString() == "Yes");
                }
            }
        }



    }
}
