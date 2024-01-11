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
        private Account_Class account;

        public event EventHandler BtnSettingsClick;

        public DropDown_Form(Panel panelUpper)
        {
            InitializeComponent();
            backupData = new Backup_Data_Class();
            updateData = new Update_Data_Class();
            account = new Account_Class();
            this.panel_upper = panelUpper;

            if (this.panel_upper != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(1160, 50); // Coordinates of the button
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
                await Task.Delay(2000);
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

            return false; // Default to false if any errors occur
        }



        private void DropDown_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
            /*
          try
          {
              Account_Class authenticatedUser = new Account_Class(); // Replace this with your actual instance

              // Pass authenticatedUser as a parameter to Main_Form constructor
              Main_Form frm2 = new Main_Form(authenticatedUser);
              frm2.ShowImageDatabase();
              //DisplayUIBackup();

              // Introduce a 5-second delay
              await Task.Delay(2000);
              this.Dispose();
              Application.OpenForms["Main_Form"].Dispose();

              Login_Form frm = new Login_Form();
              frm.Show();
          }
          catch (Exception ex)
          {
              MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
             DialogResult res;
         res = MessageBox.Show("Do you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

         if (res == DialogResult.Yes)
         {
             /
             this.Dispose();
             Application.OpenForms["Main_Form"].Dispose();
             Login_Form frm = new Login_Form();
             frm.Show();
         }
         else
         {
             backupData.BackupDatabaseEveryLogout();
             this.Dispose();
             Application.OpenForms["Main_Form"].Dispose();
             Login_Form frm = new Login_Form();
             frm.Show();

         }

        
         */
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
