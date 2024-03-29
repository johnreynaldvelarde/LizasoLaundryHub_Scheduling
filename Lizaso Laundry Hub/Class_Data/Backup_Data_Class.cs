﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Lizaso_Laundry_Hub
{
    public class Backup_Data_Class
    {
        private DB_Connection database = new DB_Connection();

        // automatically create a folder for lizaso laundry hub in documents
        public void CreateLizasoLaundryHubFolder()
        {
            try
            {
                string baseFolderPath = @"C:\Lizaso Laundry Hub";
                string backupFolderPath = Path.Combine(baseFolderPath, "Database Backup");
                string manuallyBackupFolderPath = Path.Combine(backupFolderPath, "Manually Backup");
                string autoBackupFolderPath = Path.Combine(backupFolderPath, "Auto Backup");
                string userProfileFolderPath = Path.Combine(baseFolderPath, "User Profile");
                string customerRecipientFolderPath = Path.Combine(baseFolderPath, "Customer Recipient");
                string systemSettingsFolderPath = Path.Combine(baseFolderPath, "System Settings");

                if (!Directory.Exists(baseFolderPath))
                {
                    // Create Lizaso Laundry Hub folder
                    Directory.CreateDirectory(baseFolderPath);

                    // Create Database Backup folder
                    Directory.CreateDirectory(backupFolderPath);

                    // Create Manually Backup folder within Database Backup
                    Directory.CreateDirectory(manuallyBackupFolderPath);

                    // Create Auto Backup folder within Database Backup
                    Directory.CreateDirectory(autoBackupFolderPath);

                    // Create subfolders inside Auto Backup
                    string logoutUserBackupFolderPath = Path.Combine(autoBackupFolderPath, "Logout User Backup");
                    string dailyBackupFolderPath = Path.Combine(autoBackupFolderPath, "Daily Backup");
                    string weeklyBackupFolderPath = Path.Combine(autoBackupFolderPath, "Weekly Backup");
                    string monthlyBackupFolderPath = Path.Combine(autoBackupFolderPath, "Monthly Backup");
                    string yearlyBackupFolderPath = Path.Combine(autoBackupFolderPath, "Yearly Backup");

                    Directory.CreateDirectory(logoutUserBackupFolderPath);
                    Directory.CreateDirectory(dailyBackupFolderPath);
                    Directory.CreateDirectory(weeklyBackupFolderPath);
                    Directory.CreateDirectory(monthlyBackupFolderPath);
                    Directory.CreateDirectory(yearlyBackupFolderPath);

                    // Create User Profile folder
                    Directory.CreateDirectory(userProfileFolderPath);

                    // Create Customer Recipient folder
                    Directory.CreateDirectory(customerRecipientFolderPath);

                    // Create System Settings folder
                    Directory.CreateDirectory(systemSettingsFolderPath);

                    AutomaticallyCreateAutoBackupConfig();
                    AutoCreateDefaultAccountUser();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during folder creation: {ex.Message}", "Folder Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AutoCreateDefaultAccountUser()
        {
            string defaultUserName = "Admin";
            string defaultPassWord = "secret12345";

            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\User Profile", $"{defaultUserName}.txt");

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.WriteLine($"Password: {defaultPassWord}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AutomaticallyCreateAutoBackupConfig()
        {
            bool logoutBackup = false;
            bool dailyBackup = false;
            bool weeklyBackup = false;
            bool monthlyBackup = false;
            bool yearlyBackup = false;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // For every logout
        public void BackupDatabaseEveryLogout()
        {
            try
            {
                DateTime dateRecord = DateTime.Now;
                string dbName = "DB_Laundry";
                string backupFileName = $"DB_Backup_{dateRecord:yyyyMMdd_HHmmss}.bak";

                string baseFolderPath = @"C:\Lizaso Laundry Hub";
                string databaseBackupPath = Path.Combine(baseFolderPath, "Database Backup");
                string autoBackupPath = Path.Combine(databaseBackupPath, "Auto Backup");
                string logoutUserBackupPath = Path.Combine(autoBackupPath, "Logout User Backup");

                if (!Directory.Exists(baseFolderPath))
                {
                    Directory.CreateDirectory(baseFolderPath);
                }

                if (!Directory.Exists(databaseBackupPath))
                {
                    Directory.CreateDirectory(databaseBackupPath);
                }

                if (!Directory.Exists(autoBackupPath))
                {
                    Directory.CreateDirectory(autoBackupPath);
                }

                if (!Directory.Exists(logoutUserBackupPath))
                {
                    Directory.CreateDirectory(logoutUserBackupPath);
                }

                string connectionString = database.MyConnection();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string useDatabaseCommand = $"USE {dbName};";
                    using (SqlCommand useDatabaseCmd = new SqlCommand(useDatabaseCommand, connection))
                    {
                        useDatabaseCmd.ExecuteNonQuery();
                    }

                    string backupCommand = "BACKUP DATABASE " + dbName +
                                          " TO DISK = '" + Path.Combine(logoutUserBackupPath, backupFileName) +
                                          "' WITH FORMAT ,MEDIANAME = 'Z_SQLServerBackups', NAME = ' Full Backup of " + dbName + "';";

                    using (SqlCommand command = new SqlCommand(backupCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Server Error during database backup: {sqlEx.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during database backup: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // << SETTINGS FORM / Backup Restore Form / Tab Manually Backup Configuration>>
        // method to manually backup 
        public bool BackupDatabase_Manually(string folderPath, int numberFiles, string databaseName)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    MessageBox.Show("Backup folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                CleanUpOldBackups(folderPath);

                string connectionString = database.MyConnection();

                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();

                    string useDatabaseCommand = $"USE {databaseName};";
                    using (SqlCommand useDatabaseCmd = new SqlCommand(useDatabaseCommand, connect))
                    {
                        useDatabaseCmd.ExecuteNonQuery();
                    }

                    for (int i = 0; i < numberFiles; i++)
                    {
                        string backupFileName = $"DB_Backup_{i + 1}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                        string backupFilePath = Path.Combine(folderPath, backupFileName);

                        string backupCommand = $"BACKUP DATABASE {databaseName} TO DISK = '{backupFilePath}' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = 'Full Backup of {databaseName}';";

                        using (SqlCommand command = new SqlCommand(backupCommand, connect))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Database backup successful.", "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Server Error during database backup: {sqlEx.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during database backup: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void CleanUpOldBackups(string folderPath)
        {
            try
            {
                string[] backupFiles = Directory.GetFiles(folderPath, "DB_Backup_*.bak");

                foreach (var file in backupFiles)
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cleaning up old backups: {ex.Message}", "Cleanup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool RestoreDatabase(string locationPath, string serverName, string databaseName)
        {
            try
            {
                string masterConnectionString = $"Data Source={serverName};Initial Catalog=master;Integrated Security=True";

                using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
                {
                    masterConnection.Open();

                    string useDatabaseCommand = "USE master;";
                    using (SqlCommand useDatabaseCmd = new SqlCommand(useDatabaseCommand, masterConnection))
                    {
                        useDatabaseCmd.ExecuteNonQuery();
                    }

                    string singleUserCommand = $"ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                    using (SqlCommand singleUserCmd = new SqlCommand(singleUserCommand, masterConnection))
                    {
                        singleUserCmd.ExecuteNonQuery();
                    }

                    string restoreCommand = $"RESTORE DATABASE {databaseName} FROM DISK = '{locationPath}' WITH REPLACE;";
                    using (SqlCommand restoreCmd = new SqlCommand(restoreCommand, masterConnection))
                    {
                        restoreCmd.ExecuteNonQuery();
                    }

                    string multiUserCommand = $"ALTER DATABASE {databaseName} SET MULTI_USER;";
                    using (SqlCommand multiUserCmd = new SqlCommand(multiUserCommand, masterConnection))
                    {
                        multiUserCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Database restore successful.", "Restore Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true; 
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Server Error during database restore: {sqlEx.Message}", "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during database restore: {ex.Message}", "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // For daily backup
        public void DailyBackup()
        {
            try
            {
                DateTime dateRecord = DateTime.Now;
                string dbName = "DB_Laundry";
                string backupFileName = $"DB_Backup_{dateRecord:yyyyMMdd_HHmmss}.bak";

                string baseFolderPath = @"C:\Lizaso Laundry Hub";
                string databaseBackupPath = Path.Combine(baseFolderPath, "Database Backup");
                string autoBackupPath = Path.Combine(databaseBackupPath, "Auto Backup");
                string logoutUserBackupPath = Path.Combine(autoBackupPath, "Daily Backup");

                if (!Directory.Exists(baseFolderPath))
                {
                    Directory.CreateDirectory(baseFolderPath);
                }

                if (!Directory.Exists(databaseBackupPath))
                {
                    Directory.CreateDirectory(databaseBackupPath);
                }

                if (!Directory.Exists(autoBackupPath))
                {
                    Directory.CreateDirectory(autoBackupPath);
                }

                if (!Directory.Exists(logoutUserBackupPath))
                {
                    Directory.CreateDirectory(logoutUserBackupPath);
                }

                string connectionString = database.MyConnection();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string useDatabaseCommand = $"USE {dbName};";
                    using (SqlCommand useDatabaseCmd = new SqlCommand(useDatabaseCommand, connection))
                    {
                        useDatabaseCmd.ExecuteNonQuery();
                    }

                    string backupCommand = "BACKUP DATABASE " + dbName +
                                          " TO DISK = '" + Path.Combine(logoutUserBackupPath, backupFileName) +
                                          "' WITH FORMAT ,MEDIANAME = 'Z_SQLServerBackups', NAME = ' Full Backup of " + dbName + "';";

                    using (SqlCommand command = new SqlCommand(backupCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Server Error during database backup: {sqlEx.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during database backup: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // For weekly backup
        public void WeeklyBackup()
        {
            try
            {
                DateTime dateRecord = DateTime.Now;
                string dbName = "DB_Laundry";
                string backupFileName = $"DB_Backup_{dateRecord:yyyyMMdd_HHmmss}.bak";

                string baseFolderPath = @"C:\Lizaso Laundry Hub";
                string databaseBackupPath = Path.Combine(baseFolderPath, "Database Backup");
                string autoBackupPath = Path.Combine(databaseBackupPath, "Auto Backup");
                string logoutUserBackupPath = Path.Combine(autoBackupPath, "Weekly Backup");

                if (!Directory.Exists(baseFolderPath))
                {
                    Directory.CreateDirectory(baseFolderPath);
                }

                if (!Directory.Exists(databaseBackupPath))
                {
                    Directory.CreateDirectory(databaseBackupPath);
                }

                if (!Directory.Exists(autoBackupPath))
                {
                    Directory.CreateDirectory(autoBackupPath);
                }

                if (!Directory.Exists(logoutUserBackupPath))
                {
                    Directory.CreateDirectory(logoutUserBackupPath);
                }

                string connectionString = database.MyConnection();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string useDatabaseCommand = $"USE {dbName};";
                    using (SqlCommand useDatabaseCmd = new SqlCommand(useDatabaseCommand, connection))
                    {
                        useDatabaseCmd.ExecuteNonQuery();
                    }

                    string backupCommand = "BACKUP DATABASE " + dbName +
                                          " TO DISK = '" + Path.Combine(logoutUserBackupPath, backupFileName) +
                                          "' WITH FORMAT ,MEDIANAME = 'Z_SQLServerBackups', NAME = ' Full Backup of " + dbName + "';";

                    using (SqlCommand command = new SqlCommand(backupCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Server Error during database backup: {sqlEx.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during database backup: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // For monthly backup
        public void MonthlyBackup()
        {
            try
            {
                DateTime dateRecord = DateTime.Now;
                string dbName = "DB_Laundry";
                string backupFileName = $"DB_Backup_{dateRecord:yyyyMMdd_HHmmss}.bak";

                string baseFolderPath = @"C:\Lizaso Laundry Hub";
                string databaseBackupPath = Path.Combine(baseFolderPath, "Database Backup");
                string autoBackupPath = Path.Combine(databaseBackupPath, "Auto Backup");
                string logoutUserBackupPath = Path.Combine(autoBackupPath, "Monthly Backup");

                if (!Directory.Exists(baseFolderPath))
                {
                    Directory.CreateDirectory(baseFolderPath);
                }

                if (!Directory.Exists(databaseBackupPath))
                {
                    Directory.CreateDirectory(databaseBackupPath);
                }

                if (!Directory.Exists(autoBackupPath))
                {
                    Directory.CreateDirectory(autoBackupPath);
                }

                if (!Directory.Exists(logoutUserBackupPath))
                {
                    Directory.CreateDirectory(logoutUserBackupPath);
                }

                string connectionString = database.MyConnection();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Set the database context
                    string useDatabaseCommand = $"USE {dbName};";
                    using (SqlCommand useDatabaseCmd = new SqlCommand(useDatabaseCommand, connection))
                    {
                        useDatabaseCmd.ExecuteNonQuery();
                    }

                    string backupCommand = "BACKUP DATABASE " + dbName +
                                          " TO DISK = '" + Path.Combine(logoutUserBackupPath, backupFileName) +
                                          "' WITH FORMAT ,MEDIANAME = 'Z_SQLServerBackups', NAME = ' Full Backup of " + dbName + "';";

                    using (SqlCommand command = new SqlCommand(backupCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Server Error during database backup: {sqlEx.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during database backup: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // For yearly backup
        public void YearlyBackup()
        {
            try
            {
                DateTime dateRecord = DateTime.Now;
                string dbName = "DB_Laundry";
                string backupFileName = $"DB_Backup_{dateRecord:yyyyMMdd_HHmmss}.bak";

                string baseFolderPath = @"C:\Lizaso Laundry Hub";
                string databaseBackupPath = Path.Combine(baseFolderPath, "Database Backup");
                string autoBackupPath = Path.Combine(databaseBackupPath, "Auto Backup");
                string logoutUserBackupPath = Path.Combine(autoBackupPath, "Yearly Backup");

                if (!Directory.Exists(baseFolderPath))
                {
                    Directory.CreateDirectory(baseFolderPath);
                }

                if (!Directory.Exists(databaseBackupPath))
                {
                    Directory.CreateDirectory(databaseBackupPath);
                }

                if (!Directory.Exists(autoBackupPath))
                {
                    Directory.CreateDirectory(autoBackupPath);
                }

                if (!Directory.Exists(logoutUserBackupPath))
                {
                    Directory.CreateDirectory(logoutUserBackupPath);
                }

                string connectionString = database.MyConnection();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string useDatabaseCommand = $"USE {dbName};";
                    using (SqlCommand useDatabaseCmd = new SqlCommand(useDatabaseCommand, connection))
                    {
                        useDatabaseCmd.ExecuteNonQuery();
                    }

                    string backupCommand = "BACKUP DATABASE " + dbName +
                                          " TO DISK = '" + Path.Combine(logoutUserBackupPath, backupFileName) +
                                          "' WITH FORMAT ,MEDIANAME = 'Z_SQLServerBackups', NAME = ' Full Backup of " + dbName + "';";

                    using (SqlCommand command = new SqlCommand(backupCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Server Error during database backup: {sqlEx.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during database backup: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
