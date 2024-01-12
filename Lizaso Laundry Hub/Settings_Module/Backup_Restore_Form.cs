using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Lizaso_Laundry_Hub.Settings_Module
{
    public partial class Backup_Restore_Form : KryptonForm
    {
        private Backup_Data_Class backupData;
        private Activity_Log_Class activityLogger;
        private Account_Class account;
        public Backup_Restore_Form()
        {
            InitializeComponent();
            backupData = new Backup_Data_Class();
            activityLogger = new Activity_Log_Class();
            account = new Account_Class();
        }

        private void Backup_Restore_Form_Load(object sender, EventArgs e)
        {
            Display_BackupRestoreSettings();
        }

        public void Display_BackupRestoreSettings()
        {
            if (tab_BackupRestore.SelectedTab == tabPage1)
            {
                Get_ManuallyBackupConfig();
            }
            else if (tab_BackupRestore.SelectedTab == tabPage2)
            {
                Get_ServerName(txt_ServerName);
                Get_RestoreConfig();
                image_server_loading.Visible = true;
            }
            else if (tab_BackupRestore.SelectedTab == tabPage3)
            {
                Get_AutoBackupConfig();
            }
        }


        private void btn_BackupDatabase_Click(object sender, EventArgs e)
        {
            string saveDirectory = Label_ClickPath.Text;
            int filesToKeep = int.Parse(txt_NoFiles.Text);
            string databaseName = txt_DatabaseName.Text;

            backupData.BackupDatabase_Manually(saveDirectory, filesToKeep, databaseName);
            UserActivityLogBackupManully(account.User_Name);
        }

        public void UserActivityLogBackupManully(string userName)
        {
            string activityType = "Backup Database";
            string backupDescription = $"A database backup has been performed by {userName} as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, backupDescription);
        }

        public void UserActivityLogRestore(string userName)
        {
            string activityType = "Restore Database";
            string restoreDescription = $"A database restore has been performed by {userName} as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, restoreDescription);
        }

        public void UserActivityLogSaveConfig(string userName)
        {
            string activityType = "Save Configuration";
            string configurationDescription = $"{userName} has saved the system configuration as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, configurationDescription);
        }

        private void Label_ClickPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    Label_ClickPath.Text = folderDialog.SelectedPath;
                }
            }
        }


        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            if (Label_ClickPath.Text == "Click to Set Directory Path")
            {
                MessageBox.Show("Please set the directory path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(String.IsNullOrEmpty(txt_NoFiles.Text))
            {
                MessageBox.Show("Please enter the number of files to keep.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(txt_NoFiles.Text, out int numberOfFiles) || numberOfFiles <= 0)
            {
                MessageBox.Show("Please enter a valid number greater than 0 for the number of files to keep.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(String.IsNullOrEmpty(txt_DatabaseName.Text))
            {
                MessageBox.Show("Please enter the database name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string saveDirectory = Label_ClickPath.Text;
                string filesToKeep = txt_NoFiles.Text;
                string databaseName = txt_DatabaseName.Text;

                string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Manually Backup Configuration.txt");

                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine($"Save Directory Path: {saveDirectory}");
                        sw.WriteLine($"No of Files to Keep: {filesToKeep}");
                        sw.WriteLine($"Database Name: {databaseName}");
                    }

                    UserActivityLogSaveConfig(account.User_Name);
                    MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Get_ManuallyBackupConfig();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // manually configuration
        public void Get_ManuallyBackupConfig()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Manually Backup Configuration.txt");

            if (!File.Exists(filePath))
            {
              
            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        Label_ClickPath.Text = sr.ReadLine()?.Replace("Save Directory Path: ", "");
                        txt_NoFiles.Text = sr.ReadLine()?.Replace("No of Files to Keep: ", "");
                        txt_DatabaseName.Text = sr.ReadLine()?.Replace("Database Name: ", "");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void Get_RestoreConfig()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Restore Configuration.txt");

            if (!File.Exists(filePath))
            {

            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string locationPathLine = sr.ReadLine();
                        string serverNameLine = sr.ReadLine();
                        string databaseNameLine = sr.ReadLine();

                        if (locationPathLine != null)
                        {
                            Label_ClickLocateBackup.Text = locationPathLine.Replace("Location Path: ", "");
                        }

                        sr.ReadLine();

                        if (databaseNameLine != null)
                        {
                            txt_RestoreDatabaseName.Text = databaseNameLine.Replace("Database Name: ", "");
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // restore configuration
        private void Label_ClickLocateBackup_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Lizaso Laundry Hub\Database Backup";
            openFileDialog.Title = "Select .bak File";
            openFileDialog.Filter = "BAK files (*.bak)|*.bak";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                Label_ClickLocateBackup.Text = openFileDialog.FileName;

                Console.WriteLine("Selected .bak file: " + openFileDialog.FileName);
            }
        }

        public async void Get_ServerName(TextBox textBox)
        {
            try
            {
                textBox.Text = "Retrieving...";
               
                string sqlInstanceName = await Task.Run(() => GetSqlInstanceName());
                image_server_loading.Visible = false;
                textBox.Text = sqlInstanceName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting SQL Server instance name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSqlInstanceName()
        {
            SqlDataSourceEnumerator enumerator = SqlDataSourceEnumerator.Instance;
            System.Data.DataTable table = enumerator.GetDataSources();

            foreach (System.Data.DataRow row in table.Rows)
            {
                string instanceName = row["InstanceName"] as string;
                string server = row["ServerName"] as string;

                if (!string.IsNullOrEmpty(instanceName))
                {
                    return $"{server}\\{instanceName}";
                }
            }

            return null;
        }

        private void btn_SaveConfigRestore_Click(object sender, EventArgs e)
        {
            if (Label_ClickLocateBackup.Text == "Click to the Location of Backup Folder")
            {
                MessageBox.Show("Please set the location path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(txt_ServerName.Text))
            {
                MessageBox.Show("Please enter the server name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txt_ServerName.Text == "Retrieving...")
            {
                MessageBox.Show("Server name is still being retrieved. Please wait.", "Retrieving Server Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (String.IsNullOrEmpty(txt_RestoreDatabaseName.Text))
            {
                MessageBox.Show("Please enter the database name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string locationDirectory = Label_ClickLocateBackup.Text;
                string serverName = txt_ServerName.Text;
                string databasNameRestore = txt_RestoreDatabaseName.Text;

                string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Restore Configuration.txt");

                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine($"Location Path: {locationDirectory}");
                        sw.WriteLine($"Server Name: {serverName}");
                        sw.WriteLine($"Database Name: {databasNameRestore}");
                    }

                    UserActivityLogSaveConfig(account.User_Name);
                    MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Get_RestoreConfig();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void btn_RestoreDatabase_Click(object sender, EventArgs e)
        {
            if (Label_ClickLocateBackup.Text == "Click to the Location of Backup Folder")
            {
                MessageBox.Show("Please set the location path.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(txt_ServerName.Text))
            {
                MessageBox.Show("Please enter the server name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txt_ServerName.Text == "Retrieving...")
            {
                MessageBox.Show("Server name is still being retrieved. Please wait.", "Retrieving Server Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (String.IsNullOrEmpty(txt_RestoreDatabaseName.Text))
            {
                MessageBox.Show("Please enter the database name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string LocationPath = Label_ClickLocateBackup.Text;
                string serverName = txt_ServerName.Text;
                string databaseName = txt_RestoreDatabaseName.Text;

                backupData.RestoreDatabase(LocationPath, serverName, databaseName);
                UserActivityLogRestore(account.User_Name);
            }
        }

        private void tab_BackupRestore_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display_BackupRestoreSettings();
        }

        // Automatically Backup Config
        private void btn_SaveAutoBackupConfig_Click(object sender, EventArgs e)
        {
            bool logoutBackup = cbBackupLogout.Checked;
            bool dailyBackup = cbDailyBackup.Checked;
            bool weeklyBackup = cbWeeklyBackup.Checked;
            bool monthlyBackup = cbMonthlyBackup.Checked;
            bool yearlyBackup = cbYearlyBackup.Checked;

            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Auto Backup Configuration.txt");

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine($"Logout Auto Backup: {logoutBackup}");
                    sw.WriteLine($"Daily Auto Backup: {dailyBackup}");
                    sw.WriteLine($"Weekly Auto Backup: {weeklyBackup}");
                    sw.WriteLine($"Monthly Auto Backup: {monthlyBackup}");
                    sw.WriteLine($"Yearly Auto Backup: {yearlyBackup}");
                }

                MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Get_AutoBackupConfig()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Auto Backup Configuration.txt");

            if (!File.Exists(filePath))
            {

            }
            else
            {
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

                                UpdateControlBasedOnSetting(settingName, settingValue);
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

        private void UpdateControlBasedOnSetting(string settingName, bool settingValue)
        {
            switch (settingName)
            {
                case "Logout Auto Backup":
                    cbBackupLogout.Checked = settingValue;
                    break;
                case "Daily Auto Backup":
                    cbDailyBackup.Checked = settingValue;
                    break;
                case "Weekly Auto Backup":
                    cbWeeklyBackup.Checked = settingValue;
                    break;
                case "Monthly Auto Backup":
                    cbMonthlyBackup.Checked = settingValue;
                    break;
                case "Yearly Auto Backup":
                    cbYearlyBackup.Checked = settingValue;
                    break;

                default:
                    break;
            }
        }

        private string backupFilePath;

        private void btn_LocateFileForDrive_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\Lizaso Laundry Hub\Database Backup";
            openFileDialog.Title = "Select .bak File";
            openFileDialog.Filter = "BAK files (*.bak)|*.bak";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                btn_LocateFileForDrive.Text = openFileDialog.FileName;
                btn_LocateFileForDrive.Text = backupFilePath;

                Console.WriteLine("Selected .bak file: " + openFileDialog.FileName);
            }
        }

        private async void btn_SaveGoogle_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsInternetAvailable())
                {
                    // Internet connection is available, proceed with Google Drive upload
                    UserCredential credential = await GetGoogleDriveCredential();

                    if (credential != null)
                    {
                        var driveService = new DriveService(new BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = txt_ApplicationName.Text, // Use txt_ApplicationName text
                        });

                        // Upload the file
                        await UploadFileToDrive(driveService, backupFilePath);
                    }
                    else
                    {
                        // Handle credential retrieval failure
                        MessageBox.Show("Failed to retrieve Google Drive credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // No internet connection, display a message or take appropriate action
                    MessageBox.Show("No internet connection available. Please check your network settings.", "No Internet Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsInternetAvailable()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("www.google.com", 1000);
                return (reply != null && reply.Status == IPStatus.Success);
            }
            catch (PingException)
            {
                return false;
            }
        }

        private async Task<UserCredential> GetGoogleDriveCredential()
        {
            UserCredential credential;

            string credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "credentials.json");

            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.DriveFile },
                    txt_EmailAddress.Text, // Use txt_EmailAddress text
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
            }

            return credential;
        }

        private async Task UploadFileToDrive(DriveService service, string filePath)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id";
                await request.UploadAsync();
            }

            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);
        }
    }
}
