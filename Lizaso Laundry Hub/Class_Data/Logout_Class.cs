using System;
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
                Regular_User_Form regularForm = Application.OpenForms.OfType<Regular_User_Form>().FirstOrDefault();

                if (CheckLogoutAutoBackupSetting())
                {
                    if (mainForm != null)
                    {
                        mainForm.ShowImageDatabase();
                        mainForm.Get_AutoSave_Label();
                    }
                    else if(regularForm != null)
                    {
                        regularForm.ShowImageDatabase();
                        regularForm.Get_AutoSave_Label();
                    }
                    else
                    {

                    }

                    backupData.BackupDatabaseEveryLogout();
                    await Task.Delay(2000);
                    UserActivityLog(account.User_Name);
                    updateData.Update_UserLastActiveAndStatus(account.User_ID);
                    CheckWhoFormOpen();

                    Login_Form frm = new Login_Form();
                    frm.Show();
                }
                else
                {
                    UserActivityLog(account.User_Name);
                    updateData.Update_UserLastActiveAndStatus(account.User_ID);
                    CheckWhoFormOpen();

                    Login_Form frm1 = new Login_Form();
                    frm1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CheckWhoFormOpen()
        {
            // Check if Main_Form is open
            if (IsFormOpen("Main_Form"))
            {
                Application.OpenForms["Main_Form"].Dispose();
            }
            // Check if Regular_User_Form is open
            else if (IsFormOpen("Regular_User_Form"))
            {
                Application.OpenForms["Regular_User_Form"].Dispose();
            }
        }

        private bool IsFormOpen(string formName)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == formName)
                {
                    return true; 
                }
            }
            return false; 
        }

        // method to check if the auto backup is true in textfile
        private bool CheckLogoutAutoBackupSetting()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Auto Backup Configuration.txt");

            if (!File.Exists(filePath))
            {
                return false;
            }

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string settingName = parts[0].Trim();
                            bool settingValue = Convert.ToBoolean(parts[1].Trim());

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
