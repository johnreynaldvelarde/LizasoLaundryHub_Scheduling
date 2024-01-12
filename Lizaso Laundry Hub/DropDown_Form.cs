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
using ComponentFactory.Krypton.Toolkit;
using Lizaso_Laundry_Hub.Settings_Module;

namespace Lizaso_Laundry_Hub
{
    public partial class DropDown_Form : KryptonForm
    {
        private Panel panel_upper;
        private Backup_Data_Class backupData;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;
        private Account_Class account;

        public event EventHandler BtnSettingsClick;

        public DropDown_Form(Panel panelUpper)
        {
            InitializeComponent();
            backupData = new Backup_Data_Class();
            updateData = new Update_Data_Class();
            activityLogger = new Activity_Log_Class();
            account = new Account_Class();
            this.panel_upper = panelUpper;

            if (this.panel_upper != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(1160, 50); 
            }
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(this.panel_upper.Right, this.panel_upper.Bottom);
            }

            this.Show();
        }

    
        private async void btn_Logout_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayUIBackup();
                await Task.Delay(1500);
                UserActivityLog(account.User_Name);
                this.Dispose();
                updateData.Update_UserLastActiveAndStatus(account.User_ID);
                Application.OpenForms["Main_Form"].Dispose();
                
                Login_Form frm = new Login_Form();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisplayUIBackup()
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
            }
        }

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

        private void DropDown_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            BtnSettingsClick?.Invoke(this, EventArgs.Empty);
        }

        private void btn_BackupRestore_Click(object sender, EventArgs e)
        {
            Backup_Restore_Form backup = new Backup_Restore_Form();
            backup.ShowDialog();
        }
    }
}
