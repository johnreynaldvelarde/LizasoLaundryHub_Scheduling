using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Lizaso_Laundry_Hub.Settings_Module
{
    public partial class Backup_Restore_Form : KryptonForm
    {
        private Backup_Data_Class backupData;
        public Backup_Restore_Form()
        {
            InitializeComponent();
            backupData = new Backup_Data_Class();
        }

        private void Backup_Restore_Form_Load(object sender, EventArgs e)
        {
            Display_BackupRestoreSettings();
        }

        public void Display_BackupRestoreSettings()
        {
            if (tab_BackupRestore.SelectedTab == tabPage1)
            {
                //DisplayDateModified();
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
        }

        private void Label_ClickPath_Click(object sender, EventArgs e)
        {
            // Open a FolderBrowserDialog to let the user choose a directory
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    // Update the Label_ClickPath with the selected directory path
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
                // Get the values to be saved
                string saveDirectory = Label_ClickPath.Text;
                string filesToKeep = txt_NoFiles.Text;
                string databaseName = txt_DatabaseName.Text;

                // Define the path for the notepad file
                string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Manually Backup Configuration.txt");

                try
                {
                    // Write the details to the notepad file
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine($"Save Directory Path: {saveDirectory}");
                        sw.WriteLine($"No of Files to Keep: {filesToKeep}");
                        sw.WriteLine($"Database Name: {databaseName}");
                    }

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
            // Define the path for the notepad file
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Manually Backup Configuration.txt");

            // Check if the file exists before proceeding
            if (!File.Exists(filePath))
            {
              
            }
            else
            {
                try
                {
                    // Read the details from the notepad file
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        // Read each line and update the corresponding controls
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
            // Define the path for the notepad file
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Restore Configuration.txt");

            // Check if the file exists before proceeding
            if (!File.Exists(filePath))
            {

            }
            else
            {
                try
                {
                    // Read the details from the notepad file
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string locationPathLine = sr.ReadLine();
                        string serverNameLine = sr.ReadLine();
                        string databaseNameLine = sr.ReadLine();

                        // Check if the lines are not null before extracting values
                        if (locationPathLine != null)
                        {
                            Label_ClickLocateBackup.Text = locationPathLine.Replace("Location Path: ", "");
                        }

                        // Ignore the Server Name and proceed to read the Database Name
                        sr.ReadLine();

                        if (databaseNameLine != null)
                        {
                            txt_RestoreDatabaseName.Text = databaseNameLine.Replace("Database Name: ", "");
                        }
                        /*
                        // Read each line and update the corresponding controls
                        Label_ClickLocateBackup.Text = sr.ReadLine()?.Replace("Location Path: ", "");
                        txt_ServerName.Text = sr.ReadLine()?.Replace("Server Name: ", "");
                        txt_RestoreDatabaseName.Text = sr.ReadLine()?.Replace("Database Name: ", "");
                        */
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
            // Create an OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set initial directory, title, and filter
            openFileDialog.InitialDirectory = @"C:\Lizaso Laundry Hub\Database Backup";
            openFileDialog.Title = "Select .bak File";
            openFileDialog.Filter = "BAK files (*.bak)|*.bak";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            // Display the OpenFileDialog
            DialogResult result = openFileDialog.ShowDialog();

            // Process the selected file
            if (result == DialogResult.OK)
            {
                // Update the Label_ClickLocateBackup with the selected file's full path
                Label_ClickLocateBackup.Text = openFileDialog.FileName;

                // Optionally, you can display the selected .bak file's full path
                Console.WriteLine("Selected .bak file: " + openFileDialog.FileName);
            }
        }

        public async void Get_ServerName(TextBox textBox)
        {
            try
            {
                // Set a temporary message while retrieving the server name
                textBox.Text = "Retrieving...";
               
                // Run the server name retrieval in a separate thread
                string sqlInstanceName = await Task.Run(() => GetSqlInstanceName());
                image_server_loading.Visible = false;
                // Set the text property of the provided TextBox with the SQL Server instance name
                textBox.Text = sqlInstanceName;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the process
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

                // Assuming that you want the first available SQL Server instance
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
                // Get the values to be saved
                string locationDirectory = Label_ClickLocateBackup.Text;
                string serverName = txt_ServerName.Text;
                string databasNameRestore = txt_RestoreDatabaseName.Text;

                // Define the path for the notepad file
                string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Restore Configuration.txt");

                try
                {
                    // Write the details to the notepad file
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine($"Location Path: {locationDirectory}");
                        sw.WriteLine($"Server Name: {serverName}");
                        sw.WriteLine($"Database Name: {databasNameRestore}");
                    }

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

            // Define the path for the notepad file
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Auto Backup Configuration.txt");

            try
            {
                // Write the details to the notepad file
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
            // Define the path for the notepad file
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Auto Backup Configuration.txt");

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
                                string settingName = parts[0].Trim();
                                bool settingValue = Convert.ToBoolean(parts[1].Trim());

                                // Update controls based on the setting name
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
                // Add more cases for additional settings if needed
                default:
                    break;
            }
        }

        private void btn_LocateFileForDrive_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set initial directory, title, and filter
            openFileDialog.InitialDirectory = @"C:\Lizaso Laundry Hub\Database Backup";
            openFileDialog.Title = "Select .bak File";
            openFileDialog.Filter = "BAK files (*.bak)|*.bak";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            // Display the OpenFileDialog
            DialogResult result = openFileDialog.ShowDialog();

            // Process the selected file
            if (result == DialogResult.OK)
            {
                // Update the Label_ClickLocateBackup with the selected file's full path
                btn_LocateFileForDrive.Text = openFileDialog.FileName;

                // Optionally, you can display the selected .bak file's full path
                Console.WriteLine("Selected .bak file: " + openFileDialog.FileName);
            }
        }

        private void btn_SaveGoogle_Click(object sender, EventArgs e)
        {

        }
    }
}
