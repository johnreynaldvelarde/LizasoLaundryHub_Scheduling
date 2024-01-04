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
    public partial class Main_Form : Form
    {
        private Form activeForm = null;
        public Account_Class AuthenticatedUser { get; set; }
        private DropDown_Form dropDownForm;
        private Notify_Module.DropDown_Notification_Form dropNoti;
        private Get_Data_Class getData;
        public int User_ID;
        public string User_Name;

        private Services_Form servicesForm;

        public Main_Form(Account_Class authenticatedUser)
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            AuthenticatedUser = authenticatedUser;

            if (AuthenticatedUser != null)
            {
                lblUserName.Text = "Welcome, " + AuthenticatedUser.User_Name;
                lblUserName.Width = CalculateLabelWidth(lblUserName.Text);
                User_ID = AuthenticatedUser.User_ID;
                User_Name = AuthenticatedUser.User_Name;
            }

            Count_Pending_Timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            Count_Pending_Timer.Tick += Count_Pending_Timer_Tick;
            Count_Pending_Timer.Start();
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
                return (int)size.Width + 0; // Add some padding to the width
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

        private void btn_Services_Click(object sender, EventArgs e)
        {
            Services_Form servicesForm = new Services_Form();
            openChildPanel(servicesForm);
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            openChildPanel(new Customer_Form());
        }

        private void btn_StaffMembers_Click(object sender, EventArgs e)
        {
            openChildPanel(new User_Form());
        }

        private void btn_Inventory_Click(object sender, EventArgs e)
        {
            openChildPanel(new Inventory_Form());
        }

        private void btn_Schedule2_Click(object sender, EventArgs e)
        {
            openChildPanel(new Schedule_Form());
        }

        private void btn_Payments_Click(object sender, EventArgs e)
        {
            openChildPanel(new Payments_Form());
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            Services_Form servicesForm = new Services_Form();
            openChildPanel(servicesForm);
            lblUpperTime.Text = DateTime.Now.ToLongTimeString();
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
                // Enable the timer after the asynchronous operation is complete
                Count_Pending_Timer.Enabled = true;
            }
        }

        public async Task DisplayNotification()
        {
            try 
            { 
            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Enable the timer after the asynchronous operation is complete
                //Count_Pending_Timer.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblUpperTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            if (dropDownForm == null || dropDownForm.IsDisposed)
            {
                // Create a new instance if it doesn't exist or is disposed
                dropDownForm = new DropDown_Form(panel_upper);
            }
            else
            {
                // Toggle the visibility if the form is already instantiated
                if (dropDownForm.Visible)
                    dropDownForm.Close();
                else
                    dropDownForm.Show();
            }
        }

        private void btnNotification_Click(object sender, EventArgs e)
        {

            if (dropNoti == null || dropNoti.IsDisposed)
            {
                // Create a new instance if it doesn't exist or is disposed
                dropNoti = new Notify_Module.DropDown_Notification_Form(panel_upper);
            }
            else
            {
                // Toggle the visibility if the form is already instantiated
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

       







        // main panel dock
        /*
        private void openChildPanel(Form childPanel)
        {
            if (activeForm != null)

            activeForm.Close();
            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            main_panelDock.Controls.Add(childPanel);
            main_panelDock.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }
        */

    }
}
