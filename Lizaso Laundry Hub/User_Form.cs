using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Lizaso_Laundry_Hub
{
    public partial class User_Form : KryptonForm
    {
        private Account_Class account;
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;
        private User_Module.View_Online_Form offline;
        private int u_userID, s_userID, getUserIDArchive;
        private string u_username, s_userName;
        private byte u_dashboard, u_services, u_schedule, u_customer, u_payments, u_user, u_inventory, u_settings;
        private byte s_services, s_schedule, s_customer, s_payments, s_user, s_inventory, s_settings;

        private string getPassword;

        public User_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
            account = new Account_Class();
            activityLogger = new Activity_Log_Class();
        }

        private void btn_OnlineView_Click(object sender, EventArgs e)
        {
            offline = new User_Module.View_Online_Form();
            offline.Show();
        }

        private void btn_CreateNewUser_Click(object sender, EventArgs e)
        {
            Add_User_Form frm = new Add_User_Form(this);
            frm.ShowDialog();
        }

        public void  DisplayUserView()
        {
            if (tab_User.SelectedTab == tabPage1)
            {
                getData.Get_RegularUserAndPermissions(grid_regular_user);
            }
            else if (tab_User.SelectedTab == tabPage2)
            {
                getData.Get_SuperUserAndPermissions(grid_super_user);
            }
            else if (tab_User.SelectedTab == tabPage3)
            {
                getData.Get_DeletedUser(grid_user_archive);
            }
            else if (tab_User.SelectedTab == tabPage4)
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

        // Datagridview for Regular User
        private void grid_regular_user_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_regularuser = grid_regular_user.Columns[e.ColumnIndex].Name;

            if (column_regularuser == "Edit2")
            {
                Get_UserProfileRegularUser();
                
                Add_User_Form frm = new Add_User_Form(this);
                frm.btnSave.Text = "Update";
                frm.lblUserTitle.Text = "Update User and Permissions";
                frm.u_userID = u_userID;
                frm.txt_UserName.Text = u_username;
                frm.txt_Password.Text = getPassword;
                frm.rdRegularUser.Checked = true;
                frm.ckDashboard.Checked = (u_dashboard == 1);
                frm.ckAvailableServices.Checked = (u_services == 1);
                frm.ckSchedule.Checked = (u_schedule == 1);
                frm.ckCustomerManage.Checked = (u_customer == 1);
                frm.ckPayments.Checked = (u_payments == 1);
                frm.ckUserManage.Checked = (u_user == 1);
                frm.ckInventory.Checked = (u_inventory == 1);
                frm.ckSettings.Checked = (u_settings == 1);
                frm.ShowDialog();
            }
            else if (column_regularuser == "Delete2")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this user account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_UserToDeleted(u_userID, account.User_ID);
                    UserActivityLog(u_username);
                    DisplayUserView();
                }
            }
        }

        public void UserActivityLog(string userName)
        {
            string activityType = "Archive";
            string archiveDescription = $"{userName}'s account has been deleted as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, archiveDescription);
        }

        // Datagridview for Regular User
        private void grid_regular_user_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_regular_user.CurrentRow != null)
            {
                int i = grid_regular_user.CurrentRow.Index;

                if (int.TryParse(grid_regular_user[1, i].Value.ToString(), out int userId))
                {
                    u_userID = userId;

                    u_username = grid_regular_user[2, i].Value.ToString();
                    u_dashboard = Convert.ToByte(grid_regular_user[3, i].Value.ToString() == "Yes");
                    u_services = Convert.ToByte(grid_regular_user[4, i].Value.ToString() == "Yes");
                    u_schedule = Convert.ToByte(grid_regular_user[5, i].Value.ToString() == "Yes");
                    u_customer = Convert.ToByte(grid_regular_user[6, i].Value.ToString() == "Yes");
                    u_payments = Convert.ToByte(grid_regular_user[7, i].Value.ToString() == "Yes");
                    u_user = Convert.ToByte(grid_regular_user[8, i].Value.ToString() == "Yes");
                    u_inventory = Convert.ToByte(grid_regular_user[9, i].Value.ToString() == "Yes");
                    u_settings = Convert.ToByte(grid_regular_user[10, i].Value.ToString() == "Yes");
                }
            }
        }

        // Datagrid method for Super User
        private void grid_super_user_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_superuser = grid_super_user.Columns[e.ColumnIndex].Name;

            if (column_superuser == "Edit3")
            {
                Get_UserProfileSuperUser();

                Add_User_Form frm = new Add_User_Form(this);
                frm.btnSave.Text = "Update";
                frm.lblUserTitle.Text = "Update User and Permissions";
                frm.u_userID = s_userID;
                frm.txt_UserName.Text = s_userName;
                frm.txt_Password.Text = getPassword;
                frm.rdSuperUser.Checked = true;
                frm.ShowDialog();

            }
            else if (column_superuser == "Delete3")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this user account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_UserToDeleted(s_userID, account.User_ID);
                    UserActivityLog(s_userName);
                    DisplayUserView();
                }
            }
        }

        private void grid_super_user_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_super_user.CurrentRow != null)
            {
                int i = grid_super_user.CurrentRow.Index;

                if (int.TryParse(grid_super_user[1, i].Value.ToString(), out int SuperuserId))
                {
                    s_userID = SuperuserId;

                    s_userName = grid_super_user[2, i].Value.ToString();
                    s_services = Convert.ToByte(grid_super_user[3, i].Value.ToString() == "Yes");
                    s_schedule = Convert.ToByte(grid_super_user[4, i].Value.ToString() == "Yes");
                    s_customer = Convert.ToByte(grid_super_user[5, i].Value.ToString() == "Yes");
                    s_payments = Convert.ToByte(grid_super_user[6, i].Value.ToString() == "Yes");
                    s_user = Convert.ToByte(grid_super_user[7, i].Value.ToString() == "Yes");
                    s_inventory = Convert.ToByte(grid_super_user[8, i].Value.ToString() == "Yes");
                    s_settings = Convert.ToByte(grid_super_user[9, i].Value.ToString() == "Yes");
                }
            }
        }


        private void grid_user_archive_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_archive = grid_user_archive.Columns[e.ColumnIndex].Name;

            if (column_archive == "Recycle")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to recycle this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_UserRecycleArchive(getUserIDArchive);
                    DisplayUserView();
                }
            }

        }

        public void UserActivityLogRecycle(string userName)
        {
            string activityType = "Recycle";
            string recycleDescription = $"{userName}'s account has been restored from the archive as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, recycleDescription);
        }

        private void grid_user_archive_SelectionChanged(object sender, EventArgs e)
        {

            if (grid_user_archive.CurrentRow != null)
            {
                int archive = grid_user_archive.CurrentRow.Index;

                if (int.TryParse(grid_user_archive[1, archive].Value.ToString(), out int selectArchiveID))
                {
                    getUserIDArchive = selectArchiveID;

                }
            }
        }

        // for super user
        public void Get_UserProfileSuperUser()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\User Profile", $"{s_userName}.txt");

            if (!File.Exists(filePath))
            {

            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        getPassword = sr.ReadLine()?.Replace("Password: ", "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // for regular user
        public void Get_UserProfileRegularUser()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\User Profile", $"{u_username}.txt");

            if (!File.Exists(filePath))
            {

            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        getPassword = sr.ReadLine()?.Replace("Password: ", "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
