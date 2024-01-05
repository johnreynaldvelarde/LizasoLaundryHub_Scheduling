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
    public partial class Backup_Restore_Form : KryptonForm
    {
        private Backup_Data_Class backupData;
        public Backup_Restore_Form()
        {
            InitializeComponent();
            backupData = new Backup_Data_Class();
        }
        // get the string data modified in C:\:Lizaso Laundry Hub \ Database Backup check the DB_Backup.bak and get the date modified on it   public void DisplayDateModified()
        public void DisplayDateModified()
        {
            string backupFolderPath = @"C:\Lizaso Laundry Hub\Database Backup";
            string backupFileName = "DB_Backup.bak";
            string backupFilePath = Path.Combine(backupFolderPath, backupFileName);

            try
            {
                // Check if the directory exists
                if (Directory.Exists(backupFolderPath))
                {
                    // Check if the file exists
                    if (File.Exists(backupFilePath))
                    {
                        // Get the date modified of the file
                        DateTime dateModified = File.GetLastWriteTime(backupFilePath);

                        // Display the date modified in the label
                        Label_LastBackUpInfo.Text = dateModified.ToString();
                    }
                    else
                    {
                        Label_LastBackUpInfo.Text = "DB_Backup.bak file not found.";
                    }
                }
                else
                {
                    Label_LastBackUpInfo.Text = "Database Backup directory not found.";
                }
            }
            catch (Exception ex)
            {
                Label_LastBackUpInfo.Text = $"An error occurred: {ex.Message}";
            }
        }

        private void Backup_Restore_Form_Load(object sender, EventArgs e)
        {
            DisplayDateModified();
            Get_ManuallyBackupConfig();
        }

        private void btn_BackupDatabase_Click_1(object sender, EventArgs e)
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

       
    }
}
