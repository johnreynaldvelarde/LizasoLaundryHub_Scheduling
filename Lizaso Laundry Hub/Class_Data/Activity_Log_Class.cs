using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub
{
    public class Activity_Log_Class
    {
        private DB_Connection database = new DB_Connection();
        private Account_Class account;

        public Activity_Log_Class()
        {
            account = new Account_Class();
        }

        public bool LogActivity(string activityType, string description)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "INSERT INTO Activity_Log (User_ID, Log_Date, User_Name, Activity_Type, Description, Status) " +
                                   "VALUES (@UserID, @LogDate, @UserName, @ActivityType, @Description, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@UserID", account.User_ID);
                        cmd.Parameters.AddWithValue("@LogDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UserName", account.User_Name);
                        cmd.Parameters.AddWithValue("@ActivityType", activityType);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Status", 1);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
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
