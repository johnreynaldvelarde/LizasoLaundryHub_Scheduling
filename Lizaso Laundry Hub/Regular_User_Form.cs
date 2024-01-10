using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub
{
    public partial class Regular_User_Form : Form
    {
        public Account_Class AuthenticatedUser { get; set; }
        private Get_Data_Class getData;
        private Form activeForm = null;
        
        public int User_ID;

        public Regular_User_Form(Account_Class authenticatedUser)
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            AuthenticatedUser = authenticatedUser;

            if (AuthenticatedUser != null)
            {
                lblUserName.Text = "Welcome, " + AuthenticatedUser.User_Name;
                lblUserName.Width = CalculateLabelWidth(lblUserName.Text);
                User_ID = AuthenticatedUser.User_ID;
            }
            InitializeButtons();
           
        }

        private int CalculateLabelWidth(string text)
        {
            using (Graphics g = lblUserName.CreateGraphics())
            {
                SizeF size = g.MeasureString(text, lblUserName.Font);
                return (int)size.Width + 0; // Add some padding to the width
            }
        }

        private void Regular_User_Form_Load(object sender, EventArgs e)
        {
            CheckEnableButton();
        }
        public void CheckEnableButton()
        {
            if (btn_Dashboard.Enabled)
            {
                openChildPanel(new Dashboard_Form());
            }
            else if (btn_Services.Enabled)
            {
                openChildPanel(new Services_Form());
            }
            else if (btn_Schedule.Enabled)
            {
                openChildPanel(new Schedule_Form());
            }
            else if (btn_Customer.Enabled)
            {
                openChildPanel(new Customer_Form());
            }
            else if (btn_Payments.Enabled)
            {
                openChildPanel(new Payments_Form());
            }
            else if (btn_UserManage.Enabled)
            {
                openChildPanel(new User_Form());
            }
            else if (btn_Inventory.Enabled)
            {
                openChildPanel(new Inventory_Form());
            }
            else if (btn_Settings.Enabled)
            {
                openChildPanel(new Settings_Form());
            }
            else
            {
                btnNotification.Enabled = false;
                MessageBox.Show("No available permissions found for this user account. The user may not have been assigned any permissions or is marked as a super user.", "No Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // main panel dock
        private void openChildPanel(Form childPanel)
        {
            if (activeForm != null)

            activeForm.Close();
            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            regular_panel_dock.Controls.Add(childPanel);
            regular_panel_dock.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }

        private void InitializeButtons()
        {
            // Fetch user permissions from the database
            User_Permissions_Class permissions = getData.GetUserPermissions(AuthenticatedUser.User_ID);

            // Enable or disable buttons based on user permissions
            btn_Dashboard.Enabled = permissions.Dashboard;
            btn_Services.Enabled = permissions.Available_Services;
            btn_Schedule.Enabled = permissions.Schedule;
            btn_Customer.Enabled = permissions.Customer_Manage;
            btn_Payments.Enabled = permissions.Payments;
            btn_UserManage.Enabled = permissions.User_Manage;
            btn_Inventory.Enabled = permissions.Inventory;
            btn_Settings.Enabled = permissions.Settings;
        }
        private void btn_Dashboard_Click(object sender, EventArgs e)
        {
            openChildPanel(new Dashboard_Form());
        }

        private void btn_Services_Click(object sender, EventArgs e)
        {
            openChildPanel(new Services_Form());
        }

        private void btn_Payments_Click(object sender, EventArgs e)
        {
            openChildPanel(new Payments_Form());
        }

        private void btn_Schedule_Click(object sender, EventArgs e)
        {
            openChildPanel(new Schedule_Form());
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            openChildPanel(new Customer_Form());
        }

        private void btn_UserManage_Click(object sender, EventArgs e)
        {
            openChildPanel(new User_Form());
        }

        private void btn_Inventory_Click(object sender, EventArgs e)
        {
            openChildPanel(new Inventory_Form());
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            openChildPanel(new Settings_Form());
        }
     
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                this.Dispose();
                Login_Form frm = new Login_Form();
                frm.Show();
            }
            else
            {
                this.Show();
            }
        }
        
       

       
    }
}
