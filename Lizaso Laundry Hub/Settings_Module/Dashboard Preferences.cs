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

        public void Get_DashboardPreferences()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Dashboard Preferences.txt.txt");

            // Check if the file exists before proceeding
            if (!File.Exists(filePath))
            {

            }
            else
            { 
            
            
            }

        }


        private void btn_SaveConfigRestore_Click(object sender, EventArgs e)
        {
            // Section 1

            // Section 2

            // Section 3
            bool selectCalendarView = rdCalendarView.Checked;
            // Section 4
            bool selectDeliveryList = rdDeliverySummary.Checked;
            bool selectPendingList = rdDeliverySummary.Checked;

            // Define the path for the notepad file
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Dashboard Preferences.txt");

            try
            {
                // Write the details to the notepad file
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine($"Calendar View: {selectCalendarView}");
                    sw.WriteLine($"Delivery Summary: {selectDeliveryList}");
                    sw.WriteLine($"Payment Pending List: {selectPendingList}");
                }

                MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {

        }
    }
}
