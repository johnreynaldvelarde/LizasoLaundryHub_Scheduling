using Lizaso_Laundry_Hub.Class_Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lizaso_Laundry_Hub
{
    public partial class Regular_User_Form : Form
    {
        public Account_Class AuthenticatedUser { get; set; }

        private Notify_Module.DropDown_Notification_Form dropNoti;

        private Logout_Class logout;
        private Account_Class account;
        private Get_Data_Class getData;
        private Activity_Log_Class activityLogger;

        private Timer notificationTimer;
        private Services_Form servicesForm;
        private Form activeForm = null;
        
        public int User_ID;
        public string User_Name;

        public Regular_User_Form(Account_Class authenticatedUser)
        {
            InitializeComponent();
            logout = new Logout_Class();
            account = new Account_Class();
            getData = new Get_Data_Class();
            activityLogger = new Activity_Log_Class();

            AuthenticatedUser = authenticatedUser;

            if (AuthenticatedUser != null)
            {
                lblUserName.Text = "Welcome, " + AuthenticatedUser.User_Name;
                lblUserName.Width = CalculateLabelWidth(lblUserName.Text);
                User_ID = AuthenticatedUser.User_ID;
                User_Name = AuthenticatedUser.User_Name;

                UserActivityLog(User_Name);
            }
            InitializeButtons();

            // for notification
            notificationTimer = new Timer();
            notificationTimer.Interval = 1000; 
            notificationTimer.Tick += async (sender, e) => await NotificationTimer_TickAsync();

            // Start the timer
            notificationTimer.Start();
        }

        public void ShowImageDatabase()
        {
            image_database_save.Visible = true;
        }

        public Label Get_AutoSave_Label()
        {
            lbl_ShowAutoBackup.Visible = true;
            return lbl_ShowAutoBackup;
        }

        public bool UserActivityLog(string userName)
        {
            try
            {
                string activityType = "Login";
                string loginDescription = $"{userName} logged into the system at {DateTime.Now}.";
                activityLogger.LogActivity(activityType, loginDescription);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging user activity: {ex.Message}");
                return false; 
            }
        }

        private int CalculateLabelWidth(string text)
        {
            using (Graphics g = lblUserName.CreateGraphics())
            {
                SizeF size = g.MeasureString(text, lblUserName.Font);
                return (int)size.Width + 0; 
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

            if (childPanel is Services_Form)
            {
                if (servicesForm == null || servicesForm.IsDisposed)
                {
                    servicesForm = (Services_Form)childPanel;
                    servicesForm.FormClosed += (s, args) => servicesForm = null; // Reset the reference when the form is closed
                }
                else
                {
                    servicesForm.BringToFront();
                    return; // Don't proceed with the rest of the method
                }
            }

            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            main_panelDock.Controls.Add(childPanel);
            main_panelDock.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }
        /*
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
        */

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
            Services_Form servicesForm = new Services_Form();
            openChildPanel(servicesForm);
            //openChildPanel(new Services_Form());
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
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                logout.MethodToLogoutUser();
            }
            /*
            DialogResult res;
            res = MessageBox.Show("Do you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                UserActivityLogout(account.User_Name);
                this.Dispose();
                Login_Form frm = new Login_Form();
                frm.Show();
            }
            else
            {
                this.Show();
            }
            */
        }

        private void btnNotification_Click(object sender, EventArgs e)
        {
            if (dropNoti == null || dropNoti.IsDisposed)
            {
                dropNoti = new Notify_Module.DropDown_Notification_Form(panel_upper);
            }
            else
            {
                if (dropNoti.Visible)
                    dropNoti.Close();
                else
                    dropNoti.Show();
            }
        }


        private async Task NotificationTimer_TickAsync()
        {
            await Task.Run(() => RunNotification());
        }

        public void RunNotification()
        {
            bool hasTrueNotifications = getData.GetActivityLogCount(account.User_ID);

            if (hasTrueNotifications)
            {
                UpdateNotificationImage(Properties.Resources.BellWithRed);
            }
            else
            {
                UpdateNotificationImage(Properties.Resources.BellWhite);
            }
        }

        public void UpdateNotificationImage(Image image)
        {
            if (btnNotification.InvokeRequired)
            {
                btnNotification.Invoke(new Action(() => btnNotification.Image = image));
            }
            else
            {
                btnNotification.Image = image;
            }
        }
    }
}
