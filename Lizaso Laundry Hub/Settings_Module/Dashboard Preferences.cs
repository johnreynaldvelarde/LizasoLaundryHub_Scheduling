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

namespace Lizaso_Laundry_Hub.Settings_Module
{
    public partial class Dashboard_Preferences : KryptonForm
    {
        public Dashboard_Preferences()
        {
            InitializeComponent();
        }

        private void btn_SaveConfigRestore_Click(object sender, EventArgs e)
        {
            // Section 1
            bool selectAllLogUsers = rdAllUsersLog.Checked;
            bool selectListCustomer = rdCustomerList.Checked;

            // Section 2
            bool selectTotalEarnings = rdTotalEarnings.Checked;
            bool selectInventoryMonitor = rdInventoryMonitor.Checked;

            // Section 3
            bool selectCalendarView = rdCalendarView.Checked;
            bool selectStatsView = rdStatsView.Checked;

            // Section 4
            bool selectDeliveryList = rdDeliverySummary.Checked;
            bool selectPendingList = rdPendingList.Checked;

            // Define the path for the notepad file
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Dashboard Preferences.txt");

            try
            {
                // Write the details to the notepad file
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // Section 1
                    sw.WriteLine($"All Users Activity Log: {selectAllLogUsers}");
                    sw.WriteLine($"Customer List View: {selectListCustomer}");
                    // Section 2
                    sw.WriteLine($"Total Earnings: {selectTotalEarnings}");
                    sw.WriteLine($"Inventory Monitor: {selectInventoryMonitor}");

                    // Section 3
                    sw.WriteLine($"Calendar View: {selectCalendarView}");
                    sw.WriteLine($"Statistic View: {selectStatsView}");

                    // Section 4
                    sw.WriteLine($"Delivery Summary: {selectDeliveryList}");
                    sw.WriteLine($"Payment Pending List: {selectPendingList}");
                }

                MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Get_DashboardPreferences();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Get_DashboardPreferences()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Dashboard Preferences.txt");

            // Check if the file exists before proceeding
            if (!File.Exists(filePath))
            {

            }
            else
            {
                try
                {
                    // Read the details from the configuration file
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        // Read each line and update the corresponding controls
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(':');
                            if (parts.Length == 2)
                            {
                                string DashboardName = parts[0].Trim();
                                bool DashboardValue = Convert.ToBoolean(parts[1].Trim());

                                UpdateDashboardSettings(DashboardName, DashboardValue);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateDashboardSettings(string dashboardName, bool dashboardValue)
        {
            switch (dashboardName)
            {
                case "All Users Activity Log":
                    rdAllUsersLog.Checked = dashboardValue;
                    break;
                case "Customer List View":
                    rdCustomerList.Checked = dashboardValue;
                    break;
                case "Total Earnings":
                    rdTotalEarnings.Checked = dashboardValue;
                    break;
                case "Inventory Monitor":
                    rdInventoryMonitor.Checked = dashboardValue;
                    break;
                case "Calendar View":
                    rdCalendarView.Checked = dashboardValue;
                    break;
                case "Statistic View":
                    rdStatsView.Checked = dashboardValue;
                    break;
                case "Delivery Summary":
                    rdDeliverySummary.Checked = dashboardValue;
                    break;
                case "Payment Pending List":
                    rdPendingList.Checked = dashboardValue;
                    break;

                // Add more cases for additional settings if needed
                default:
                    break;
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            rdAllUsersLog.Checked = true;
            rdTotalEarnings.Checked = true;
            rdCalendarView.Checked = true;
            rdDeliverySummary.Checked = true;
        }

        private void Dashboard_Preferences_Load(object sender, EventArgs e)
        {
            Get_DashboardPreferences();
        }
    }
}
