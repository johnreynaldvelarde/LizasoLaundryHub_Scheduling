using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Lizaso_Laundry_Hub
{
    public class Backup_Data_Class
    {
        private DB_Connection database = new DB_Connection();

        // method to backup database every logout of the user in the system
        /*
        public void BackupDatabaseEveryLogout()
        {
            try
            {
                string dbName = "DB_Laundry";
                string backupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DB_Backup");

                // Ensure the backup directory exists
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }

                // Set the connection string for your database
                //string connectionString = "Data Source=YourServer;Initial Catalog=" + dbName + ";Integrated Security=True";

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Create a backup command
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connect;
                        command.CommandText = "BACKUP DATABASE " + dbName + " TO DISK='" + Path.Combine(backupPath, "DBBackup.bak") + "'";

                        // Execute the backup command
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Database backup successful.", "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during database backup: " + ex.Message, "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */

        public void BackupDatabaseEveryLogout()
        {
            try
            {
                string dbName = "DB_Laundry";
                string backupFileName = "DBBackup.bak";
                string backupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DB_Backup");

                // Ensure the backup directory exists or create it
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }

                string connectionString = database.MyConnection(); // Adjust this according to your connection method

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a backup command
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        // Use the 'WITH INIT' option to overwrite the existing backup file
                        command.CommandText = $"BACKUP DATABASE [{dbName}] TO DISK='{Path.Combine(backupPath, backupFileName)}' WITH INIT";

                        // Execute the backup command
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Database backup successful.", "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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








        public bool Now5()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {




                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
