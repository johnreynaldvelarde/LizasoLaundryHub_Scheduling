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
        public Backup_Restore_Form()
        {
            InitializeComponent();
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
        }

        private void btn_BackupDatabase_Click(object sender, EventArgs e)
        {
            /*
            try
            {

            }
            catch()
            {

            }
            */
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
    }
}
