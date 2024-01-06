using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lizaso_Laundry_Hub
{
    public partial class Add_User_Form : KryptonForm
    {
        private Insert_Data_Class insertData;
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private Account_Class account;

        private User_Form frm;
        public int u_userID;

        public Add_User_Form(User_Form user)
        {
            InitializeComponent();
            insertData = new Insert_Data_Class();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
            account = new Account_Class();
            frm = user;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_UserName.Text))
            {
                MessageBox.Show("Please enter the User Name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_Password.Text))
            {
                MessageBox.Show("Please enter the Password.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!(rdSuperUser.Checked ^ rdRegularUser.Checked))
            {
                MessageBox.Show("Please choose a user type (Super User or Regular User).", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (btnSave.Text == "Update")
            {
                string _username = txt_UserName.Text;
                string _password = txt_Password.Text;


                if (rdSuperUser.Checked)
                {
                    updateData.Update_User(account.User_ID, u_userID, _username, _password, 1, 1, 1, 1, 1, 1, 1, 1);
                    //MessageBox.Show("Super user account updated successfully.");
                    this.Dispose();
                }
                else
                {
                    byte availableServices = (byte)(ckAvailableServices.Checked ? 1 : 0);
                    byte schedule = (byte)(ckSchedule.Checked ? 1 : 0);
                    byte customerManage = (byte)(ckCustomerManage.Checked ? 1 : 0);
                    byte payments = (byte)(ckPayments.Checked ? 1 : 0);
                    byte userManage = (byte)(ckUserManage.Checked ? 1 : 0);
                    byte inventory = (byte)(ckInventory.Checked ? 1 : 0);
                    byte settings = (byte)(ckSettings.Checked ? 1 : 0);

                    updateData.Update_User(account.User_ID, u_userID, _username, _password, 0, availableServices, schedule, customerManage, payments, userManage, inventory, settings);
                    //MessageBox.Show("Regular user account updated successfully.");
                    this.Dispose();
                    frm.DisplayUserView();
                }
            }
            else
            {
                string _username = txt_UserName.Text;
                string _password = txt_Password.Text;

                if (getData.Get_IsUserNameExists(_username))
                {
                    MessageBox.Show("User with the same name already exists. Please choose a different username.", "Duplicate User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    if (rdSuperUser.Checked)
                    {
                        insertData.Set_CreateUser(_username, _password, 1, 1, 1, 1, 1, 1, 1, 1);
                        MessageBox.Show("Super user account successfully created");
                        this.Dispose();
                    }
                    else
                    {
                        byte availableServices = (byte)(ckAvailableServices.Checked ? 1 : 0);
                        byte schedule = (byte)(ckSchedule.Checked ? 1 : 0);
                        byte customerManage = (byte)(ckCustomerManage.Checked ? 1 : 0);
                        byte payments = (byte)(ckPayments.Checked ? 1 : 0);
                        byte userManage = (byte)(ckUserManage.Checked ? 1 : 0);
                        byte inventory = (byte)(ckInventory.Checked ? 1 : 0);
                        byte settings = (byte)(ckSettings.Checked ? 1 : 0);

                        insertData.Set_CreateUser(_username, _password, 0, availableServices, schedule, customerManage, payments, userManage, inventory, settings);
                        MessageBox.Show("Regular user account successfully created");
                        this.Dispose();
                        frm.DisplayUserView();
                    }
                }
            }
        }

        private void Add_User_Form_Load(object sender, EventArgs e)
        {
            if (btnSave.Text == "Update")
            {
                Console.WriteLine("ETO ANG USERID " + account.User_ID);
                Console.WriteLine("ETO ANG USERID ULOL " + u_userID);
                EnableAllCheckboxes();
            }
            else
            {
                DisableAllCheckboxes();
            }
        }

        private void DisableAllCheckboxes()
        {
            ckAvailableServices.Enabled = false;
            ckSchedule.Enabled = false;
            ckCustomerManage.Enabled = false;
            ckPayments.Enabled = false;
            ckUserManage.Enabled = false;
            ckInventory.Enabled = false;
            ckSettings.Enabled = false;
        }

        public void EnableAllCheckboxes()
        {
            ckAvailableServices.Enabled = true;
            ckSchedule.Enabled = true;
            ckCustomerManage.Enabled = true;
            ckPayments.Enabled = true;
            ckUserManage.Enabled = true;
            ckInventory.Enabled = true;
            ckSettings.Enabled = true;
        }

        private void rdSuperUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSuperUser.Checked)
            {
                DisableAllCheckboxes();
                All_Unchecked();
            }
        }

        private void rdRegularUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rdRegularUser.Checked)
            {
                EnableAllCheckboxes();
            }
        }

        public void Clear()
        {
            txt_UserName.Clear();
            txt_Password.Clear();
            rdSuperUser.Checked = false;
            rdRegularUser.Checked = false;

            All_Unchecked();
            DisableAllCheckboxes();
           
        }
        private void All_Unchecked()
        {
            ckAvailableServices.Checked = false;
            ckSchedule.Checked = false;
            ckCustomerManage.Checked = false;
            ckPayments.Checked = false;
            ckUserManage.Checked = false;
            ckInventory.Checked = false;
            ckSettings.Checked = false;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
