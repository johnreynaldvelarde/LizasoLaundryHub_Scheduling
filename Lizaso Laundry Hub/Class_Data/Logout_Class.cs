﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub.Class_Data
{
    public class Logout_Class
    {
        private Backup_Data_Class backupData;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;
        private Account_Class account;

        public Logout_Class()
        {
            backupData = new Backup_Data_Class();
            updateData = new Update_Data_Class();
            activityLogger = new Activity_Log_Class();
            account = new Account_Class();
        }

        public async void MethodToLogoutUser()
        {
            try
            {
                Main_Form mainForm = Application.OpenForms.OfType<Main_Form>().FirstOrDefault();

                if (CheckLogoutAutoBackupSetting())
                {
                    if (mainForm != null)
                    {
                        mainForm.ShowImageDatabase();
                        mainForm.Get_AutoSave_Label();
                    }

                    backupData.BackupDatabaseEveryLogout();
                    await Task.Delay(2000);
                    UserActivityLog(account.User_Name);
                    updateData.Update_UserLastActiveAndStatus(account.User_ID);
                    Application.OpenForms["Main_Form"].Dispose();

                    Login_Form frm = new Login_Form();
                    frm.Show();
                }
                else
                {
                    UserActivityLog(account.User_Name);
                    updateData.Update_UserLastActiveAndStatus(account.User_ID);
                    Application.OpenForms["Main_Form"].Dispose();

                    Login_Form frm1 = new Login_Form();
                    frm1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // method to check if the auto backup is true in textfile
        private bool CheckLogoutAutoBackupSetting()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Auto Backup Configuration.txt");

            // Check if the file exists before proceeding
            if (!File.Exists(filePath))
            {
                // Handle the case when the file doesn't exist
                return false;
            }

            try
            {
                // Read the details from the configuration file
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read each line and look for "Logout Auto Backup"
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string settingName = parts[0].Trim();
                            bool settingValue = Convert.ToBoolean(parts[1].Trim());

                            // Check if it is "Logout Auto Backup"
                            if (settingName == "Logout Auto Backup")
                            {
                                return settingValue;
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

        // method to put a logout action by the user
        public bool UserActivityLog(string userName)
        {
            try
            {
                string activityType = "Logout";
                string logoutDescription = $"{userName} logged out of the system at {DateTime.Now}.";

                activityLogger.LogActivity(activityType, logoutDescription);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging user activity: {ex.Message}");
                return false; 
            }
        }
    }
}
