using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using ComponentFactory.Krypton.Toolkit;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Diagnostics;
using Lizaso_Laundry_Hub.Notify_Module;

namespace Lizaso_Laundry_Hub
{
    public class Get_Data_Class
    {
        private DB_Connection database = new DB_Connection();

        // << DASHBOARD FROM / Delivery Widget Form >>
        // method to get delivery list in table Delivires in database
        public bool Get_DashboardDeliveryList(DataGridView view_delivery_list)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Define your SQL query to fetch data with a filter for 'In Transit' deliveries
                    string sql = "SELECT D.Delivery_ID, T.Transaction_ID, C.Customer_Name, D.Delivery_Address, T.Amount, D.Delivery_Status " +
                                 "FROM Deliveries D " +
                                 "JOIN Transactions T ON D.Transaction_ID = T.Transaction_ID " +
                                 "JOIN Laundry_Bookings LB ON T.Booking_ID = LB.Booking_ID " +
                                 "JOIN Customers C ON LB.Customer_ID = C.Customer_ID " +
                                 "WHERE D.Delivery_Status = 'In Transit'";  // Add this line to filter by 'In Transit' status

                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_delivery_list.Rows.Clear();

                   

                    while (reader.Read())
                    {
                        // Assuming you have the corresponding columns in your Deliveries, Transactions, Laundry_Bookings, and Customers tables
                        view_delivery_list.Rows.Add(0,
                            reader["Delivery_ID"],
                            reader["Transaction_ID"],
                            reader["Customer_Name"],
                            reader["Delivery_Address"],
                            reader["Amount"],
                            reader["Delivery_Status"]
                        );
                    }

                    reader.Close();
                    connect.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << DASHBOARD FORM / Earnings Widget Form >> 
        // method to get the total earnings of the shop and seperate the daily, weekly, monthly
        public bool Get_Transaction_Amount(Label totalEarnings, Label dailyEarnings, Label weeklyEarnings, Label monthlyEarnings)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Calculate Total Earnings
                    string totalQuery = "SELECT SUM(Amount) FROM Transactions";
                    using (SqlCommand totalCmd = new SqlCommand(totalQuery, connect))
                    {
                        object totalResult = totalCmd.ExecuteScalar();
                        if (totalResult != DBNull.Value)
                        {
                            totalEarnings.Text = $"PHP {Convert.ToDecimal(totalResult):N2}";
                        }
                        else
                        {
                            totalEarnings.Text = "PHP 0.00";
                        }
                    }

                    // Calculate Daily Earnings
                    string dailyQuery = "SELECT SUM(Amount) FROM Transactions WHERE Transaction_Date >= CAST(GETDATE() AS DATE)";
                    using (SqlCommand dailyCmd = new SqlCommand(dailyQuery, connect))
                    {
                        object dailyResult = dailyCmd.ExecuteScalar();
                        if (dailyResult != DBNull.Value)
                        {
                            dailyEarnings.Text = $"PHP {Convert.ToDecimal(dailyResult):N2}";
                        }
                        else
                        {
                            dailyEarnings.Text = "PHP 0.00";
                        }
                    }

                    // Calculate Weekly Earnings
                    string weeklyQuery = "SELECT SUM(Amount) FROM Transactions WHERE Transaction_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0)";
                    using (SqlCommand weeklyCmd = new SqlCommand(weeklyQuery, connect))
                    {
                        object weeklyResult = weeklyCmd.ExecuteScalar();
                        if (weeklyResult != DBNull.Value)
                        {
                            weeklyEarnings.Text = $"PHP {Convert.ToDecimal(weeklyResult):N2}";
                        }
                        else
                        {
                            weeklyEarnings.Text = "PHP 0.00";
                        }
                    }

                    // Calculate Monthly Earnings
                    string monthlyQuery = "SELECT SUM(Amount) FROM Transactions WHERE Transaction_Date >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)";
                    using (SqlCommand monthlyCmd = new SqlCommand(monthlyQuery, connect))
                    {
                        object monthlyResult = monthlyCmd.ExecuteScalar();
                        if (monthlyResult != DBNull.Value)
                        {
                            monthlyEarnings.Text = $"PHP {Convert.ToDecimal(monthlyResult):N2}";
                        }
                        else
                        {
                            monthlyEarnings.Text = "PHP 0.00";
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << DASHBOARD FORM / ActivityLog Widget Form >>
        // method to get activity log even get another user activity log
        public bool Get_AllUsersActivityLog(DataGridView view_activity_log)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Log_ID, User_Name, Log_Date, Description FROM Log_View ORDER BY Log_Date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Clear existing rows
                        view_activity_log.Rows.Clear();

                        while (reader.Read())
                        {
                            int logID = reader.GetInt32(reader.GetOrdinal("Log_ID"));
                            string userName = reader.GetString(reader.GetOrdinal("User_Name"));
                            DateTime logDate = reader.GetDateTime(reader.GetOrdinal("Log_Date"));
                            string description = reader.GetString(reader.GetOrdinal("Description"));

                            string formattedLogDate = FormatLogDate(logDate);

                            view_activity_log.Rows.Add(0, logID, userName, formattedLogDate, description);
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // instead showing the literal date in Log Date show minutes ago or hourse instead
        private string FormatLogDate(DateTime logDate)
        {
            TimeSpan timeDifference = DateTime.Now - logDate;

            if (timeDifference.TotalMinutes < 1)
            {
                return "Just now";
            }
            else if (timeDifference.TotalMinutes < 60)
            {
                int minutes = (int)timeDifference.TotalMinutes;
                return $"{minutes} minute{(minutes != 1 ? "s" : "")} ago";
            }
            else if (timeDifference.TotalHours < 24)
            {
                int hours = (int)timeDifference.TotalHours;
                return $"{hours} hour{(hours != 1 ? "s" : "")} ago";
            }
            else
            {
                return logDate.ToString(); // or any other formatting you prefer for older dates
            }
        }


        /*
        public void Get_DashboardDeliveryList(DataGridView view_delivery_list, bool inTransit, bool completed, bool canceled)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Define your SQL query to fetch data from multiple tables
                    string sql = "SELECT D.Delivery_ID, T.Transaction_ID, C.Customer_Name, D.Delivery_Address, T.Amount, D.Delivery_Status " +
                                 "FROM Deliveries D " +
                                 "JOIN Transactions T ON D.Transaction_ID = T.Transaction_ID " +
                                 "JOIN Laundry_Bookings LB ON T.Booking_ID = LB.Booking_ID " +
                                 "JOIN Customers C ON LB.Customer_ID = C.Customer_ID";

                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_delivery_list.Rows.Clear();
                   
                    while (reader.Read())
                    {
                        // Assuming you have the corresponding columns in your Deliveries, Transactions, Laundry_Bookings, and Customers tables
                        view_delivery_list.Rows.Add(0,
                            reader["Delivery_ID"],
                            reader["Transaction_ID"],
                            reader["Customer_Name"],
                            reader["Delivery_Address"],
                            reader["Amount"],
                            reader["Delivery_Status"]
                        );
                    }

                    reader.Close();
                }

                // Apply filtering based on checkboxes
                if (!inTransit && !completed && !canceled)
                {
                    // No checkboxes are checked, display all rows
                    return;
                }

                foreach (DataGridViewRow row in view_delivery_list.Rows)
                {
                    string status = row.Cells[6].Value.ToString();

                    // Check the status against the checkboxes and hide the row if it doesn't match
                    if ((inTransit && status == "In Transit") ||
                        (completed && status == "Completed") ||
                        (canceled && status == "Canceled"))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        public bool Get_DashboardDeliveryList(DataGridView view_delivery_list)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Define your SQL query to fetch data from multiple tables
                    string sql = "SELECT D.Delivery_ID, T.Transaction_ID, C.Customer_Name, D.Delivery_Address, T.Amount, D.Delivery_Status " +
                                 "FROM Deliveries D " +
                                 "JOIN Transactions T ON D.Transaction_ID = T.Transaction_ID " +
                                 "JOIN Laundry_Bookings LB ON T.Booking_ID = LB.Booking_ID " +
                                 "JOIN Customers C ON LB.Customer_ID = C.Customer_ID";

                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_delivery_list.Rows.Clear();

                    int i = 0;

                    while (reader.Read())
                    {
                        i += 1;

                        // Assuming you have the corresponding columns in your Deliveries, Transactions, Laundry_Bookings, and Customers tables
                        view_delivery_list.Rows.Add(
                            i,
                            reader["Delivery_ID"],
                            reader["Transaction_ID"],
                            reader["Customer_Name"],
                            reader["Delivery_Address"],
                            reader["Amount"],
                            reader["Delivery_Status"]
                        );
                    }

                    reader.Close();
                    connect.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        */

        // << ucProgressList Control  >>
        // method to check the timeleft based on bookingID proviced
        public DateTime RetrieveEndTimeFromDatabase(int bookingID)
        {
            DateTime endTime = DateTime.MinValue; // Default value if not found or in an error state

            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT End_Time FROM Bookings_View WHERE Booking_ID = @BookingID AND Bookings_Status = 'In-Progress';";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@BookingID", bookingID);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            endTime = (DateTime)result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, you might want to log it or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return endTime;
        }

        // << SERVICES FORM >>
        // method to get data in Laundry_Unit table to show it in ucUnit_Control
        public List<Unit_Class> Get_RetrieveUnits()
        {
            List<Unit_Class> units = new List<Unit_Class>();

            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT U.Unit_ID, U.Unit_Name, U.Unit_Status, " +
                                   "(SELECT TOP 1 B.Bookings_Status " +
                                   "FROM Bookings_View B " +
                                   "WHERE B.Unit_ID = U.Unit_ID AND B.Bookings_Status = 'Reserved' " +
                                   "ORDER BY B.Start_Time DESC) AS Bookings_Status " +
                                   "FROM Unit_View U " +
                                   "WHERE U.Archive = 0"; // Filter by Archive = 0

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Unit_Class unit = new Unit_Class()
                                {
                                    Unit_ID = reader.GetInt32(reader.GetOrdinal("Unit_ID")),
                                    Unit_Name = reader.GetString(reader.GetOrdinal("Unit_Name")),
                                    Avail_Status = reader.GetInt16(reader.GetOrdinal("Unit_Status")),
                                    Reserved = reader.IsDBNull(reader.GetOrdinal("Bookings_Status"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("Bookings_Status"))
                                };

                                units.Add(unit);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return units;
        }

        // << SERVICES FORM >>
        // method the get Reserved Bookings_Status in Laundry_Bookings and pass it to In_Reserved_Class then view in ucReservedList_Control
        public List<In_Reserved_Class> Get_RetrieveLaundryBookingsReserved()
        {
            List<In_Reserved_Class> inProgressList = new List<In_Reserved_Class>();
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"
                SELECT
                    LB.Booking_ID,
                    LB.Customer_ID,
                    LB.Unit_ID,
                    C.Customer_Name,
                    LU.Unit_Name,
                    LB.Services_Type,
                    FORMAT(LB.Start_Time, 'h:mm tt') AS ReservedStartTime,
                    FORMAT(LB.End_Time, 'h:mm tt') AS ReservedEndTime,
                    LB.Bookings_Status AS Status
                FROM
                    Bookings_View LB
                    INNER JOIN Customers_View C ON LB.Customer_ID = C.Customer_ID
                    INNER JOIN Unit_View LU ON LB.Unit_ID = LU.Unit_ID
                WHERE
                    LB.Bookings_Status = 'Reserved';";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                In_Reserved_Class inProgressBooking = new In_Reserved_Class()
                                {
                                    BookingID = (int)reader["Booking_ID"],
                                    CustomerID = (int)reader["Customer_ID"],
                                    UnitID = (int)reader["Unit_ID"],
                                    Customer_Name = reader["Customer_Name"].ToString(),
                                    Unit_Name = reader["Unit_Name"].ToString(),
                                    ServiceType = reader["Services_Type"].ToString(),
                                    ReservedStartTime = reader["ReservedStartTime"].ToString(),
                                    ReservedEndTime = reader["ReservedEndTime"].ToString(),
                                    Status = reader["Status"].ToString()
                                };

                                inProgressList.Add(inProgressBooking);
                            }
                        }
                    }
                }

                return inProgressList;
            }
            catch (Exception ex)
            {
                // Handle the exception (display a message, log it, etc.)
                MessageBox.Show($"An error occurred: {ex.Message}");
                return null; // Return null or an empty list, depending on your error handling strategy
            }
        }

        // << LOGIN FORM >>
        // method to get data or check the user account in User_Account table
        public Account_Class AuthenticateUser(string username, string password)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the username and password match in the User_Account table
                    string sql = "SELECT User_ID, User_Name FROM User_View WHERE User_Name COLLATE SQL_Latin1_General_CP1_CS_AS = @Username AND Password_Hash = @PasswordHash AND Archive = 0";

                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@PasswordHash", HashPassword(password));

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return new Account_Class
                            {
                                User_ID = reader.GetInt32(reader.GetOrdinal("User_ID")),
                                User_Name = reader.GetString(reader.GetOrdinal("User_Name"))
                            };
                        }

                        return null; // Authentication failed
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // << LOGIN FORM >>
        // method to check if the user id is super user or not
        public bool IsSuperUser(int userId)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the user is a Super User based on the Super_User column
                    string sql = "SELECT Super_User FROM User_View WHERE User_ID = @UserId AND Archive = 0";

                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        object result = command.ExecuteScalar();

                        return result != null && Convert.ToInt32(result) == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        // << LOGIN FORM >>
        // method for converting the password to hashpassword function (replace with a secure hashing algorithm)
        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // << REGULAR USER FORM >>
        // method to check the table User_Permission depend of what module can access in regular_user_form
        public User_Permissions_Class GetUserPermissions(int userId)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Fetch user permissions from User_Permissions table
                    string sql = "SELECT Dashboard, Available_Services, Schedule, Customer_Manage, Payments, User_Manage, Inventory, Settings FROM Permissions_View WHERE User_ID = @UserId";

                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return new User_Permissions_Class
                            {
                                Dashboard = reader.GetBoolean(reader.GetOrdinal("Dashboard")),
                                Available_Services = reader.GetBoolean(reader.GetOrdinal("Available_Services")),
                                Schedule = reader.GetBoolean(reader.GetOrdinal("Schedule")),
                                Customer_Manage = reader.GetBoolean(reader.GetOrdinal("Customer_Manage")),
                                Payments = reader.GetBoolean(reader.GetOrdinal("Payments")),
                                User_Manage = reader.GetBoolean(reader.GetOrdinal("User_Manage")),
                                Inventory = reader.GetBoolean(reader.GetOrdinal("Inventory")),
                                Settings = reader.GetBoolean(reader.GetOrdinal("Settings"))
                            };
                        }
                        return new User_Permissions_Class(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new User_Permissions_Class(); // Return default permissions on error
            }
        }

        // << USER FROM >>
        // method to display the data of User_Permissions table 
        public bool Get_RegularUserAndPermissions(DataGridView view_regular_user)
        {
            try
            {
                view_regular_user.Rows.Clear();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();

                    // Define your SQL query with a CASE statement to convert bit values to 'Yes' or 'No'
                    // Add a condition to filter out super users
                    string query = @"SELECT UA.User_ID, UA.User_Name,
                                CASE WHEN UP.Dashboard = 1 THEN 'Yes' ELSE 'No' END AS Dashboard,
                                CASE WHEN UP.Available_Services = 1 THEN 'Yes' ELSE 'No' END AS Available_Services,
                                CASE WHEN UP.Schedule = 1 THEN 'Yes' ELSE 'No' END AS Schedule,
                                CASE WHEN UP.Customer_Manage = 1 THEN 'Yes' ELSE 'No' END AS Customer_Manage,
                                CASE WHEN UP.Payments = 1 THEN 'Yes' ELSE 'No' END AS Payments,
                                CASE WHEN UP.User_Manage = 1 THEN 'Yes' ELSE 'No' END AS User_Manage,
                                CASE WHEN UP.Inventory = 1 THEN 'Yes' ELSE 'No' END AS Inventory,
                                CASE WHEN UP.Settings = 1 THEN 'Yes' ELSE 'No' END AS Settings
                         FROM User_View UA
                         INNER JOIN Permissions_View UP ON UA.User_ID = UP.User_ID
                         WHERE UA.Super_User = 0
                         AND UA.Archive = 0";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i += 1;
                            // Add rows to the DataGridView
                            view_regular_user.Rows.Add(i,
                                reader["User_ID"],
                                reader["User_Name"],
                                reader["Dashboard"],
                                reader["Available_Services"],
                                reader["Schedule"],
                                reader["Customer_Manage"],
                                reader["Payments"],
                                reader["User_Manage"],
                                reader["Inventory"],
                                reader["Settings"]
                            );
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Get_SuperUserAndPermissions(DataGridView view_super_user)
        {
            try
            {
                view_super_user.Rows.Clear();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();

                    // Define your SQL query with a CASE statement to convert bit values to 'Yes' or 'No'
                    // Add a condition to filter out super users
                    string query = @"SELECT UA.User_ID, UA.User_Name,
                                CASE WHEN UP.Dashboard = 1 THEN 'Yes' ELSE 'No' END AS Dashboard,
                                CASE WHEN UP.Available_Services = 1 THEN 'Yes' ELSE 'No' END AS Available_Services,
                                CASE WHEN UP.Schedule = 1 THEN 'Yes' ELSE 'No' END AS Schedule,
                                CASE WHEN UP.Customer_Manage = 1 THEN 'Yes' ELSE 'No' END AS Customer_Manage,
                                CASE WHEN UP.Payments = 1 THEN 'Yes' ELSE 'No' END AS Payments,
                                CASE WHEN UP.User_Manage = 1 THEN 'Yes' ELSE 'No' END AS User_Manage,
                                CASE WHEN UP.Inventory = 1 THEN 'Yes' ELSE 'No' END AS Inventory,
                                CASE WHEN UP.Settings = 1 THEN 'Yes' ELSE 'No' END AS Settings
                         FROM User_View UA
                         INNER JOIN Permissions_View UP ON UA.User_ID = UP.User_ID
                         WHERE UA.Super_User = 1
                         AND UA.Archive = 0";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            i += 1;
                            // Add rows to the DataGridView
                            view_super_user.Rows.Add(i,
                                reader["User_ID"],
                                reader["User_Name"],
                                reader["Dashboard"],
                                reader["Available_Services"],
                                reader["Schedule"],
                                reader["Customer_Manage"],
                                reader["Payments"],
                                reader["User_Manage"],
                                reader["Inventory"],
                                reader["Settings"]
                            );
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Get_IsUserNameExists(string userName)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT COUNT(*) FROM User_Account WHERE User_Name = @UserName";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);

                        int count = (int)command.ExecuteScalar();

                        // If count is greater than 0, it means the username already exists
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //
        public bool Get_IsUserNameExistsWhenUpdating(string userName, int userID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the new username exists, excluding the current user
                    string query = "SELECT COUNT(*) FROM User_Account WHERE User_Name = @UserName AND User_ID != @UserID";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@UserID", userID);

                        int count = (int)command.ExecuteScalar();

                        // If count is greater than 0, it means the username already exists for another user
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Get_DeletedUser(DataGridView view_deleted_user)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string sql = "SELECT * FROM User_View WHERE Archive = 1";
                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_deleted_user.Rows.Clear();

                    while (reader.Read())
                    {
                        i += 1;
                        view_deleted_user.Rows.Add(i, reader["User_ID"], reader["User_Name"], reader["Date_Added"].ToString());
                    }
                    reader.Close();
                    connect.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }





        // << ADD RESERVED FORM >>
        // metyhod to check if have available unit and cannot proceed to reserved if have available unit
        public bool IsAnyUnitAvailable()
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                string sql = "SELECT TOP 1 Unit_ID FROM Unit_View WHERE Unit_Status = 0 AND Archive = 0";

                using (SqlCommand command = new SqlCommand(sql, connect))
                {
                    object result = command.ExecuteScalar();

                    return (result != null);
                }
            }
        }




        public bool Get_ClosestUnit(Label r_unitID, Label lbl_UnitName, Label lblReservedStartTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"
                SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time
                FROM Bookings_View lb
                JOIN Unit_View lu ON lb.Unit_ID = lu.Unit_ID
                WHERE lb.Bookings_Status = 'In-Progress'
                AND NOT EXISTS (
                    SELECT 1
                    FROM Bookings_View lb_reserved
                    WHERE lb_reserved.Unit_ID = lb.Unit_ID
                    AND lb_reserved.Bookings_Status = 'Reserved'
                )
                ORDER BY ABS(DATEDIFF(minute, lb.End_Time, GETDATE()))";

                    using (SqlCommand closestUnitCmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = closestUnitCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                r_unitID.Text = reader["Unit_ID"].ToString();
                                lbl_UnitName.Text = reader["Unit_Name"].ToString();
                                DateTime possibleNextStartTime = (DateTime)reader["End_Time"];
                                lblReservedStartTime.Text = possibleNextStartTime.ToString("h:mm:ss tt");
                                // Additional processing, if needed
                                return true; // Unit found
                            }
                            else
                            {
                                // No suitable unit found
                                r_unitID.Text = ""; // Reset to empty
                                lbl_UnitName.Text = "No available units";
                                lblReservedStartTime.Text = "";
                                return false; // Unit not found
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Error occurred, unit not found
            }
        }
        
        // get and filter in-progress booking status in laundry bookings 
        public bool Get_BookingProgress(DataGridView view_progress)
        {
            try
            {
                view_progress.Rows.Clear();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT B.Booking_ID, B.Unit_ID, B.Customer_ID, C.Customer_Name, U.Unit_Name, B.Services_Type, B.Weight, B.Start_Time, B.End_Time, DATEDIFF(MINUTE, GETDATE(), B.End_Time) AS Time_Left " +
                                   "FROM Bookings_View B " +
                                   "INNER JOIN Unit_View U ON B.Unit_ID = U.Unit_ID " +
                                   "INNER JOIN Customers_View C ON B.Customer_ID = C.Customer_ID " +
                                   "WHERE B.Bookings_Status = 'In-Progress'";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Concatenate Start Time and End Time in a single string
                                string timeRange = $"Start Time: {reader["Start_Time"]:MM/dd/yyyy h:mm:ss tt}\nEnd Time: {reader["End_Time"]: MM/dd/yyyy h:mm:ss tt}";

                                // Add "minutes" to Time Left
                                //string timeLeft = $"{reader["Time_Left"]} minutes";
                                string timeLeft = $"{Math.Max((int)reader["Time_Left"], 0)} minutes";

                                // Add rows to the DataGridView
                                view_progress.Rows.Add(0,
                                    reader["Booking_ID"],
                                    reader["Unit_ID"],
                                    reader["Customer_ID"],
                                    reader["Customer_Name"],
                                    reader["Unit_Name"],
                                    reader["Services_Type"],
                                    reader["Weight"],
                                    timeRange,  // Display Start Time and End Time in a single column
                                    timeLeft    // Display Time Left with "minutes" added
                                );
                            }
                        }
                    }

                    connect.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // get and filter reserved booking status in laundry bookings 
        public bool Get_BookingReserved(DataGridView view_reserved)
        {
            try
            {
                view_reserved.Rows.Clear();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT B.Booking_ID, B.Unit_ID, B.Customer_ID, C.Customer_Name, U.Unit_Name, B.Services_Type, B.Weight, B.Start_Time, B.End_Time, DATEDIFF(MINUTE, GETDATE(), B.End_Time) AS Time_Left " +
                                   "FROM Bookings_View B " +
                                   "INNER JOIN Unit_View U ON B.Unit_ID = U.Unit_ID " +
                                   "INNER JOIN Customers_View C ON B.Customer_ID = C.Customer_ID " +
                                   "WHERE B.Bookings_Status = 'Reserved'";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Concatenate Start Time and End Time in a single string
                                string timeRange = $"Reservation start time: {reader["Start_Time"]:MM/dd/yyyy h:mm:ss tt}\nReservation end time: {reader["End_Time"]: MM/dd/yyyy h:mm:ss tt}";

                                // Add "minutes" to Time Left
                                //string timeLeft = $"{reader["Time_Left"]} minutes";
                                string timeLeft = $"{Math.Max((int)reader["Time_Left"], 0)} minutes";

                                // Add rows to the DataGridView
                                view_reserved.Rows.Add(0,
                                    reader["Booking_ID"],
                                    reader["Unit_ID"],
                                    reader["Customer_ID"],
                                    reader["Customer_Name"],
                                    reader["Unit_Name"],
                                    reader["Services_Type"],
                                    reader["Weight"],
                                    timeRange,  // Display Start Time and End Time in a single column
                                    timeLeft    // Display Time Left with "minutes" added
                                );
                            }
                        }
                    }

                    connect.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
       
        // << CUSTOMER FORM >>
        // get the registered customer information in customer table in database 
        public bool Get_RegisteredCustomer(DataGridView view_register_customer)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string sql = "SELECT * FROM Customers_View WHERE Customer_Type = 0 AND Archive = 0";
                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_register_customer.Rows.Clear();
                    while (reader.Read())
                    {
                        i += 1;
                        view_register_customer.Rows.Add(i, reader["Customer_ID"], reader["Customer_Name"], reader["Email_Address"], reader["Contact_Number"].ToString(), reader["Address"].ToString());
                    }
                    reader.Close();
                    connect.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        // << CUSTOMER FORM >>
        // get the guest customer information in customer table in database 
        public bool Get_GuestsCustomer(DataGridView view_guests_customer)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string sql = "SELECT * FROM Customers_View WHERE Customer_Type = 1 AND Archive = 0";
                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    //tab_Customer.SelectedTab = tabPage2;
                    view_guests_customer.Rows.Clear();
                    while (reader.Read())
                    {
                        i += 1;
                        view_guests_customer.Rows.Add(i, reader["Customer_ID"], reader["Customer_Name"], reader["Date_Added"].ToString());
                    }
                    reader.Close();
                    connect.Close();

                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << CUSTOMER FORM / Archive tab >>  
        // using customer view get the customer with archive 1
        public bool Get_CustomerDeleted(DataGridView view_customer_deleted)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string sql = "SELECT * FROM Customers_View WHERE Archive = 1";
                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_customer_deleted.Rows.Clear();

                    while (reader.Read())
                    {
                        i += 1;
                        view_customer_deleted.Rows.Add(i, reader["Customer_ID"], reader["Customer_Name"], reader["Date_Added"].ToString());
                    }
                    reader.Close();
                    connect.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // << PAYMENT DETAILS FORM / Enable & Disable ckFreeShipping >>
        // method to get the customer type
        public bool Get_CustomerType(int customerID, KryptonCheckBox ckFreeShipping)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Retrieve Customer_Type based on customerID
                    string query = "SELECT Customer_Type FROM Customers WHERE Customer_ID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        // Execute the query to get Customer_Type
                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int customerType))
                        {
                            // Check the retrieved Customer_Type
                            if (customerType == 0)
                            {
                                // Enable the checkbox if Customer_Type is 0
                                ckFreeShipping.Enabled = true;
                            }
                            else
                            {
                                // Disable the checkbox if Customer_Type is 1
                                ckFreeShipping.Enabled = false;
                            }

                            return true;
                        }
                        else
                        {
                            // Handle the case where the Customer_Type is not retrieved or cannot be parsed
                            MessageBox.Show("Error retrieving Customer_Type information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public bool Get_AllActivityLog(DataGridView view_activity_log)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string query = "SELECT Log_ID, User_ID, Log_Date, User_Name, Activity_Type, Description, Status FROM Log_View";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            view_activity_log.Rows.Clear();

                            while (reader.Read())
                            {
                                i += 1;
                                view_activity_log.Rows.Add(i, reader["Log_ID"], reader["User_ID"], reader["User_Name"], reader["Activity_Type"], reader["Log_Date"], reader["Description"], reader["Status"]);
                            }
                        }
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << SCHE
        public bool Get_BookingPending(DataGridView view_pending)
        {
            try
            {
                view_pending.Rows.Clear();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT B.Booking_ID, B.Unit_ID, B.Customer_ID, C.Customer_Name, U.Unit_Name, B.Services_Type, B.Weight, B.Bookings_Status " +
                                   "FROM Laundry_Bookings B " +
                                   "INNER JOIN Laundry_Unit U ON B.Unit_ID = U.Unit_ID " +
                                   "INNER JOIN Customers C ON B.Customer_ID = C.Customer_ID " +
                                   "WHERE B.Bookings_Status = 'Pending'";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add rows to the DataGridView
                                view_pending.Rows.Add(0,
                                    reader["Booking_ID"],
                                    reader["Unit_ID"],
                                    reader["Customer_ID"],
                                    reader["Customer_Name"],
                                    reader["Unit_Name"],
                                    reader["Services_Type"],
                                    reader["Weight"],
                                    reader["Bookings_Status"]
                               
                                );
                            }
                        }
                    }

                    connect.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Get_CountBookingPending(Label label_count_pending)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT COUNT(*) FROM Bookings_View WHERE Bookings_Status = 'Pending'";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        label_count_pending.Text = count.ToString();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << PAYMENTS FORM >>
        // get the tranaction history in table tranasction with join table of customer , user account and additonal payments
        public bool Get_TransactionHistory(DataGridView view_transaction_history)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT T.Transaction_ID, U.User_Name, T.Amount, T.Transaction_Date, T.Payment_Method, C.Customer_Name
                                     FROM Transaction_View T
                                     JOIN Bookings_View L ON T.Booking_ID = L.Booking_ID
                                     JOIN Customers_View C ON L.Customer_ID = C.Customer_ID
                                     JOIN User_View U ON T.User_ID = U.User_ID
                                     ORDER BY T.Transaction_Date DESC";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing rows in the DataGridView
                            view_transaction_history.Rows.Clear();

                            // Read data from the SqlDataReader and add rows to the DataGridView
                            while (reader.Read())
                            {
                                int transactionID = reader.GetInt32(reader.GetOrdinal("Transaction_ID"));
                                //int bookingID = reader.GetInt32(reader.GetOrdinal("Booking_ID"));
                                //int userID = reader.GetInt32(reader.GetOrdinal("User_ID"));
                                string userName = reader.GetString(reader.GetOrdinal("User_Name"));
                                decimal amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                                DateTime transactionDate = reader.GetDateTime(reader.GetOrdinal("Transaction_Date"));
                                string paymentMethod = reader.GetString(reader.GetOrdinal("Payment_Method"));
                                string customerName = reader.IsDBNull(reader.GetOrdinal("Customer_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Customer_Name"));

                                // Add a new row to the DataGridView
                                view_transaction_history.Rows.Add(0, transactionID,  userName, customerName, transactionDate, paymentMethod, amount );
                            }

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public bool Get_TransactionHistossry(DataGridView view_transaction_history)
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

        // << PAYMENTS FORM >>
        // get the additional payments in table tranasction item with getting the transaction id in datagridview
        public bool Get_TransactinoAdditionalPayments(DataGridView view_additonal_payments)
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

        // << SETTINGS MODULE / LAUNDRY UNIT CONFIGURATION FORM >>
        // method to get the all unit information in database in Laundry_Unit table
        public bool Get_UnitTable(DataGridView view_unit)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();

                    // SQL query to select Unit_ID, Unit_Name, and Unit_Status with Archive = 0
                    string query = "SELECT Unit_ID, Unit_Name, Unit_Status FROM Unit_View WHERE Archive = 0";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            view_unit.Rows.Clear();
                            // Iterate over the rows and read data
                            while (reader.Read())
                            {
                                int unitID = (int)reader["Unit_ID"];
                                string unitName = reader["Unit_Name"].ToString();
                                int unitStatus = (Int16)reader["Unit_Status"];

                                // Map the unit status values to "Available", "Occupied", "Not Available"

                                string statusText = "";
                                switch (unitStatus)
                                {
                                    case 0:
                                        statusText = "Available";
                                        break;
                                    case 1:
                                        statusText = "Occupied";
                                        break;
                                    case 2:
                                        statusText = "Not Available";
                                        break;
                                    // Add more cases if needed

                                    default:
                                        break;
                                }

                                i += 1;
                                view_unit.Rows.Add(i, unitID, unitName, statusText);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\nStack Trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Get_UnitDeleted(DataGridView view_unit_delete)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();

                    // SQL query to select Unit_ID, Unit_Name, and Unit_Status with Archive = 0
                    string query = "SELECT Unit_ID, Unit_Name, Unit_Status FROM Unit_View WHERE Archive = 1";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            view_unit_delete.Rows.Clear();
                            // Iterate over the rows and read data
                            while (reader.Read())
                            {
                                int unitID = (int)reader["Unit_ID"];
                                string unitName = reader["Unit_Name"].ToString();
                                int unitStatus = (Int16)reader["Unit_Status"];

                                // Map the unit status values to "Available", "Occupied", "Not Available"

                                string statusText = "";
                                switch (unitStatus)
                                {
                                    case 0:
                                        statusText = "Available";
                                        break;
                                    case 1:
                                        statusText = "Occupied";
                                        break;
                                    case 2:
                                        statusText = "Not Available";
                                        break;
                                    // Add more cases if needed

                                    default:
                                        break;
                                }

                                i += 1;
                                view_unit_delete.Rows.Add(i, unitID, unitName, statusText);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\nStack Trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Get_CountAllUnitStatus(Label label_count_available, Label label_count_occupied, Label label_count_notavailable)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Count the number of available units (Unit_Status = 0) with Archive = 0
                    string queryAvailable = "SELECT COUNT(*) FROM Unit_View WHERE Unit_Status = 0 AND Archive = 0";
                    using (SqlCommand commandAvailable = new SqlCommand(queryAvailable, connect))
                    {
                        int countAvailable = Convert.ToInt32(commandAvailable.ExecuteScalar());
                        label_count_available.Text = countAvailable.ToString();
                    }

                    // Count the number of occupied units (Unit_Status = 1) with Archive = 0
                    string queryOccupied = "SELECT COUNT(*) FROM Unit_View WHERE Unit_Status = 1 AND Archive = 0";
                    using (SqlCommand commandOccupied = new SqlCommand(queryOccupied, connect))
                    {
                        int countOccupied = Convert.ToInt32(commandOccupied.ExecuteScalar());
                        label_count_occupied.Text = countOccupied.ToString();
                    }

                    // Count the number of not available units (Unit_Status = 3) with Archive = 0
                    string queryNotAvailable = "SELECT COUNT(*) FROM Unit_View WHERE Unit_Status = 3 AND Archive = 0";
                    using (SqlCommand commandNotAvailable = new SqlCommand(queryNotAvailable, connect))
                    {
                        int countNotAvailable = Convert.ToInt32(commandNotAvailable.ExecuteScalar());
                        label_count_notavailable.Text = countNotAvailable.ToString();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << MAIN FORM >>
        // method that counting pending in table laundry bookings
        public async Task<int> Get_UpdateCountPendingLabelAsync(Label label_count_pending)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(database.MyConnection()))
                {
                    await connection.OpenAsync();

                    string query = "SELECT COUNT(*) FROM Bookings_View WHERE Bookings_Status = 'Pending'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int count = Convert.ToInt32(await command.ExecuteScalarAsync());

                        // Use Control.Invoke to update the UI on the UI thread
                        label_count_pending.Invoke((MethodInvoker)delegate {
                            label_count_pending.Text = count.ToString();
                        });

                        // Return the count
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Return 0 if an error occurs
            }
        }

        // -- USING VIEW TABLE IN DATABASE
        //<< INVENTORY MODULE >>
        // get the data of Item and use view Item_View to show the data in table Item
        public bool Get_InventoryItem(DataGridView view_inventory_item)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string sql = "SELECT * FROM Item_View WHERE Archive = 0"; 
                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_inventory_item.Rows.Clear();

                    while (reader.Read())
                    {
                        i += 1;
                        view_inventory_item.Rows.Add(i, reader["Item_ID"], reader["Item_Code"], reader["Item_Name"], reader["Category_Type"], reader["Quantity"].ToString(), reader["Price"].ToString());
                    }
                    reader.Close();
                    connect.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //<< INVENTORY MODULE >>
        // get the data of Item and use view Item_View to show the archive 1 mean deleted item in table Item
        public bool Get_InventoryDeleted(DataGridView view_inventory_deleted)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string sql = "SELECT * FROM Item_View WHERE Archive = 1";
                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_inventory_deleted.Rows.Clear();

                    while (reader.Read())
                    {
                        i += 1;
                        view_inventory_deleted.Rows.Add(i, reader["Item_ID"], reader["Item_Code"], reader["Item_Name"], reader["Category_Type"], reader["Quantity"].ToString(), reader["Price"].ToString());
                    }
                    reader.Close();
                    connect.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << SCHEDULE FORM >>
        // get the list of in progress in Laundry Bookings and show it to calendar
        public bool Get_CalendarListInProgress()
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

        public bool Get_CalendarListInReserved()
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

        // method to get the notification log based on user_ID
        public List<NotificationLog> GetNotificationLog(int userID)
        {
            List<NotificationLog> notificationLogs = new List<NotificationLog>();
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Log_ID, Log_Date, User_Name, Activity_Type, Description FROM Activity_Log " +
                                   "WHERE User_ID = @UserID AND Status = 1 AND Activity_Type = 'Notifications'";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NotificationLog log = new NotificationLog
                                {
                                    LogID = Convert.ToInt32(reader["Log_ID"]),
                                    LogDate = Convert.ToDateTime(reader["Log_Date"]),
                                    UserName = reader["User_Name"].ToString(),
                                    ActivityType = reader["Activity_Type"].ToString(),
                                    Description = reader["Description"].ToString()
                                };

                                notificationLogs.Add(log);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return notificationLogs;
        }

        // << PAYMENT DETAILS FORM / Receipt Form >>
        // method to get customer address based on customer id
        public string Get_CustomerAddressForReceipt(int customerID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Address FROM Customers WHERE Customer_ID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Check if the 'Address' column is not DBNull.Value before retrieving it
                                int addressOrdinal = reader.GetOrdinal("Address");
                                if (!reader.IsDBNull(addressOrdinal))
                                {
                                    string address = reader.GetString(addressOrdinal);
                                    return address;
                                }
                                else
                                {
                                    // If the address is DBNull.Value (NULL) in the database, return "None"
                                    return "None";
                                }
                            }
                        }
                    }

                    // If no address found, return "None"
                    return "None";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "None";
            }
        }

        public bool Get_UserOnlineorOffline(DataGridView view_user, int accountUserID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT User_ID, User_Name, Last_Active, Status FROM User_Account";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Clear existing rows in the DataGridView
                            view_user.Rows.Clear();

                            while (reader.Read())
                            {
                                int userId = reader.GetInt32(reader.GetOrdinal("User_ID"));
                                string userName = reader.GetString(reader.GetOrdinal("User_Name"));
                                DateTime? lastActive = reader.IsDBNull(reader.GetOrdinal("Last_Active")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Last_Active"));
                                string status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status"));

                                // Exclude the row corresponding to the specified accountUserID
                                if (userId != accountUserID)
                                {
                                    // Add a new row to the DataGridView
                                    view_user.Rows.Add(0,userId, userName, lastActive, status);
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}

