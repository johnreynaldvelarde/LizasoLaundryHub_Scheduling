﻿using Lizaso_Laundry_Hub.Class_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lizaso_Laundry_Hub
{
    public partial class Main_Form : Form
    {
        private Timer timer;
        private Timer notificationTimer;

        private Form activeForm = null;
        private Backup_Data_Class backupData;
        private Update_Data_Class updateData;
        private Get_Data_Class getData;
        private Activity_Log_Class activityLogger;
        private Logout_Class logout;

        public Account_Class AuthenticatedUser { get; set; }
        private Account_Class account;
        private Notify_Module.DropDown_Notification_Form dropNoti;
        public int User_ID;
        public string User_Name;

        private Services_Form servicesForm;

        public Main_Form(Account_Class authenticatedUser)
        {
            InitializeComponent();
            logout = new Logout_Class();
            account = new Account_Class();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
            backupData = new Backup_Data_Class();
            activityLogger = new Activity_Log_Class();
         

            AuthenticatedUser = authenticatedUser;

            if (AuthenticatedUser != null)
            {
                lblUserName.Text = "Welcome, " + AuthenticatedUser.User_Name;
                lblUserName.Width = CalculateLabelWidth(lblUserName.Text);
                User_ID = AuthenticatedUser.User_ID;
                User_Name = AuthenticatedUser.User_Name;

                updateData.Update_UserToOnline(User_ID);
                UserActivityLog(User_Name);
            }

            Count_Pending_Timer.Interval = 1000; 
            Count_Pending_Timer.Tick += Count_Pending_Timer_Tick;
            Count_Pending_Timer.Start();

            // for notification
            notificationTimer = new Timer();
            notificationTimer.Interval = 1000; 
            notificationTimer.Tick += async (sender, e) => await NotificationTimer_TickAsync();

            notificationTimer.Start();

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

        private void DropDownForm_BtnSettingsClick(object sender, EventArgs e)
        {
            openChildPanel(new Settings_Form());
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

        private int CalculateLabelWidth(string text)
        {
            using (Graphics g = lblUserName.CreateGraphics())
            {
                SizeF size = g.MeasureString(text, lblUserName.Font);
                return (int)size.Width + 0; 
            }
        }

        private void openChildPanel(Form childPanel)
        {
            if (activeForm != null)
                activeForm.Close();

            if (childPanel is Services_Form)
            {
                if (servicesForm == null || servicesForm.IsDisposed)
                {
                    servicesForm = (Services_Form)childPanel;
                    servicesForm.FormClosed += (s, args) => servicesForm = null; 
                }
                else
                {
                    servicesForm.BringToFront();
                    return; 
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

        private async void Main_Form_Load(object sender, EventArgs e)
        {
            openChildPanel(new Dashboard_Form());

            await StartPeriodicTask();
            lblUpperTime.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");

            //DisplayCountPending();
        }

        private async void Count_Pending_Timer_Tick(object sender, EventArgs e)
        {
            //await DisplayCountPending();
        }

        public async Task DisplayCountPending()
        {
            try
            {
                //Disable the timer while the asynchronous operation is in progress
                Count_Pending_Timer.Enabled = false;

                // Call the asynchronous method
                //int pendingCount = await getData.Get_UpdateCountPendingLabelAsync(lblCountPending);

                // Update the visibility based on pendingCount
               // lblCountPending.Visible = pendingCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Count_Pending_Timer.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblUpperTime.Text = DateTime.Now.ToString("MMMM dd, yyyy   hh:mm:ss tt");
            timer1.Start();
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

        private void btn_Dashboard_Click(object sender, EventArgs e)
        {
            openChildPanel(new Dashboard_Form());
        }
        private void btn_AvailableServices_Click(object sender, EventArgs e)
        {
            Services_Form servicesForm = new Services_Form();
            openChildPanel(servicesForm);
        }

        private void btn_PendingPayments_Click(object sender, EventArgs e)
        {
            openChildPanel(new Payments_Form());
        }

        private void btn_Schedule_Click(object sender, EventArgs e)
        {
            openChildPanel(new Schedule_Form());
        }

        private void btn_CustomerManage_Click(object sender, EventArgs e)
        {
            openChildPanel(new Customer_Form());
        }

        private void btn_UserMange_Click(object sender, EventArgs e)
        {
            openChildPanel(new User_Form());
        }

        private void btn_Inventory_Click_1(object sender, EventArgs e)
        {
            openChildPanel(new Inventory_Form());
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            openChildPanel(new Settings_Form());
        }


        private async Task StartPeriodicTask()
        {
            try
            {
                while (true)
                {
                    await Task.Delay(1000);

                    await Task.Run(() => AutoCheckBaseonTime());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AutoCheckBaseonTime()
        {
            // Check if it's near 12:00 AM for Daily Auto Backup
            if (IsNearMidnight())
            {
                if (CheckAutoBackupSettings("Daily Auto Backup"))
                {
                    backupData.DailyBackup();
                }
            }

            // Check if it's near the end of Sunday for Weekly Auto Backup
            if (IsNearEndOfWeek(DayOfWeek.Sunday))
            {
                if (CheckAutoBackupSettings("Weekly Auto Backup"))
                {
                    backupData.WeeklyBackup();
                }
            }

            // Check if it's near the end of the month for Monthly Auto Backup
            if (IsNearEndOfMonth())
            {
                if (CheckAutoBackupSettings("Monthly Auto Backup"))
                {
                    backupData.MonthlyBackup();
                }
            }

            // Check if it's near the end of the year for Yearly Auto Backup
            if (IsNearEndOfYear())
            {
                if (CheckAutoBackupSettings("Yearly Auto Backup"))
                {
                    backupData.YearlyBackup();
                }
            }
        }

        private bool IsNearMidnight()
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            return currentTime >= TimeSpan.FromHours(23) && currentTime <= TimeSpan.FromHours(24);
        }

        private bool IsNearEndOfWeek(DayOfWeek day)
        {
            DateTime now = DateTime.Now;
            return now.DayOfWeek == day && now.TimeOfDay >= TimeSpan.FromHours(23);
        }

        private bool IsNearEndOfMonth()
        {
            DateTime now = DateTime.Now;
            DateTime endOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);
            return (endOfMonth - now).TotalDays < 2;
        }

        private bool IsNearEndOfYear()
        {
            DateTime now = DateTime.Now;
            DateTime endOfYear = new DateTime(now.Year, 12, 31, 23, 59, 59);
            return (endOfYear - now).TotalDays < 7; 
        }

        private bool CheckAutoBackupSettings(string settingKey)
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Auto Backup Configuration.txt");

            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();

                        if (key.Equals(settingKey, StringComparison.OrdinalIgnoreCase))
                        {
                            string value = parts[1].Trim();
                            if (bool.TryParse(value, out bool autoBackupSetting))
                            {
                                return autoBackupSetting;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false; 
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                logout.MethodToLogoutUser();
            }
        }
    }
}
