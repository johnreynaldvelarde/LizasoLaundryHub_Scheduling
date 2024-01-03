﻿using System;
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

namespace Lizaso_Laundry_Hub
{
    public class Get_Data_Class
    {
        private DB_Connection database = new DB_Connection();

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

                    string query = @"SELECT End_Time FROM Laundry_Bookings WHERE Booking_ID = @BookingID AND Bookings_Status = 'In-Progress';";

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
                                   "FROM Laundry_Bookings B " +
                                   "WHERE B.Unit_ID = U.Unit_ID AND B.Bookings_Status = 'Reserved' " +
                                   "ORDER BY B.Start_Time DESC) AS Bookings_Status " +
                                   "FROM Laundry_Unit U " +
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

        /*
        public List<Unit_Class> Get_RetrieveUnits()
        {
            List<Unit_Class> units = new List<Unit_Class>();

            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Replace the SQL query with your actual query to fetch data from Laundry_Unit and Laundry_Bookings tables
                    string query = "SELECT U.Unit_ID, U.Unit_Name, U.Unit_Status, " +
                                   "(SELECT TOP 1 B.Bookings_Status " +
                                   "FROM Laundry_Bookings B " +
                                   "WHERE B.Unit_ID = U.Unit_ID AND B.Bookings_Status = 'Reserved' " +
                                   "ORDER BY B.Start_Time DESC) AS Bookings_Status " +
                                   "FROM Laundry_Unit U";

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
                // Handle the exception, you might want to log it or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return units;
        }
        */

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
                    Laundry_Bookings LB
                    INNER JOIN Customers C ON LB.Customer_ID = C.Customer_ID
                    INNER JOIN Laundry_Unit LU ON LB.Unit_ID = LU.Unit_ID
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
                    string sql = "SELECT User_ID, User_Name FROM User_Account WHERE User_Name COLLATE SQL_Latin1_General_CP1_CS_AS = @Username AND Password_Hash = @PasswordHash AND Archive = 0";

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
                    string sql = "SELECT Super_User FROM User_Account WHERE User_ID = @UserId AND Archive = 0";

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
                    string sql = "SELECT Available_Services, Schedule, Customer_Manage, Payments, User_Manage, Inventory, Settings FROM User_Permissions WHERE User_ID = @UserId";

                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return new User_Permissions_Class
                            {
                                Available_Services = reader.GetBoolean(reader.GetOrdinal("Available_Services")),
                                Schedule = reader.GetBoolean(reader.GetOrdinal("Schedule")),
                                Customer_Manage = reader.GetBoolean(reader.GetOrdinal("Customer_Manage")),
                                Payments = reader.GetBoolean(reader.GetOrdinal("Payments")),
                                User_Manage = reader.GetBoolean(reader.GetOrdinal("User_Manage")),
                                Inventory = reader.GetBoolean(reader.GetOrdinal("Inventory")),
                                Settings = reader.GetBoolean(reader.GetOrdinal("Settings"))
                            };
                        }

                        return new User_Permissions_Class(); // Return default permissions if not found
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
                                CASE WHEN UP.Available_Services = 1 THEN 'Yes' ELSE 'No' END AS Available_Services,
                                CASE WHEN UP.Schedule = 1 THEN 'Yes' ELSE 'No' END AS Schedule,
                                CASE WHEN UP.Customer_Manage = 1 THEN 'Yes' ELSE 'No' END AS Customer_Manage,
                                CASE WHEN UP.Payments = 1 THEN 'Yes' ELSE 'No' END AS Payments,
                                CASE WHEN UP.User_Manage = 1 THEN 'Yes' ELSE 'No' END AS User_Manage,
                                CASE WHEN UP.Inventory = 1 THEN 'Yes' ELSE 'No' END AS Inventory,
                                CASE WHEN UP.Settings = 1 THEN 'Yes' ELSE 'No' END AS Settings
                         FROM User_Account UA
                         INNER JOIN User_Permissions UP ON UA.User_ID = UP.User_ID
                         WHERE UA.Super_User = 0"; // Filter out super users

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

        // << ADD RESERVED FORM >>
        // metyhod to check if have available unit and cannot proceed to reserved if have available unit
        public bool IsAnyUnitAvailable()
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                string sql = "SELECT TOP 1 Unit_ID FROM Laundry_Unit WHERE Unit_Status = 0 AND Archive = 0";

                using (SqlCommand command = new SqlCommand(sql, connect))
                {
                    object result = command.ExecuteScalar();

                    return (result != null);
                }
            }
        }

        /*
        public bool IsAnyUnitAvailable()
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                string sql = "SELECT TOP 1 Unit_ID FROM Laundry_Unit WHERE Unit_Status = 0";

                using (SqlCommand command = new SqlCommand(sql, connect))
                {
                    object result = command.ExecuteScalar();

                    return (result != null);
                }
            }
        }
        */

        public bool Get_ClosestUnit(Label r_unitID, Label lbl_UnitName, Label lblReservedStartTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"
                SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time
                FROM Laundry_Bookings lb
                JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID
                WHERE lb.Bookings_Status = 'In-Progress'
                AND NOT EXISTS (
                    SELECT 1
                    FROM Laundry_Bookings lb_reserved
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

       
        /*
        public bool Get_ClosestUnit(Label r_unitID, Label lbl_UnitName, Label lblReservedStartTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"
                SELECT TOP 1 lb.Unit_ID, lu.Unit_Name, lb.End_Time AS ReservedEndTime, 
                DATEADD(MINUTE, 1, lb.End_Time) AS PossibleNextStartTime
                FROM Laundry_Bookings lb
                INNER JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID
                WHERE lb.Bookings_Status IN ('In-Progress', 'Reserved')
                AND NOT EXISTS (
                    SELECT 1
                    FROM Laundry_Bookings lb_reserved
                    WHERE lb_reserved.Unit_ID = lb.Unit_ID
                    AND lb_reserved.Bookings_Status = 'Reserved'
                    AND DATEDIFF(MINUTE, GETDATE(), lb_reserved.End_Time) < 15 -- Adjust as needed
                )
                ORDER BY 
                    CASE WHEN lb.Bookings_Status = 'In-Progress' THEN 0 ELSE 1 END,
                    ReservedEndTime ASC";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                r_unitID.Text = reader["Unit_ID"].ToString();
                                lbl_UnitName.Text = reader["Unit_Name"].ToString();
                                DateTime possibleNextStartTime = (DateTime)reader["PossibleNextStartTime"];
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
        */

        /*
        public bool Get_ClosestUnit(Label r_unitID, Label lbl_UnitName, Label lblReservedStartTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"
                SELECT TOP 1 lb.Unit_ID, lu.Unit_Name, lb.End_Time AS ReservedEndTime, 
                DATEADD(MINUTE, 1, lb.End_Time) AS PossibleNextStartTime
                FROM Laundry_Bookings lb
                INNER JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID
                WHERE lb.Bookings_Status = 'In-Progress'
                ORDER BY ReservedEndTime ASC";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                r_unitID.Text = reader["Unit_ID"].ToString();
                                lbl_UnitName.Text = reader["Unit_Name"].ToString();
                                DateTime possibleNextStartTime = (DateTime)reader["PossibleNextStartTime"];
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
        */

        /*
        public bool Get_PasswordBasedOnUserID(int _userID, KryptonTextBox passwordTextBox)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Password_Hash FROM User_Account WHERE User_ID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@UserID", _userID);

                        // Execute the query and retrieve the encrypted password
                        object encryptedPassword = command.ExecuteScalar();

                        if (encryptedPassword != null)
                        {
                            // Decrypt the encrypted password to get the original password
                            string originalPassword = DecryptPassword((byte[])encryptedPassword, encryptionKey);
                            passwordTextBox.Text = originalPassword;
                        }
                        else
                        {
                            MessageBox.Show("Password not found for the specified user ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
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
        
        private string encryptionKey = "YourEncryptionKey1234567890123456"; // Replace with a 256-bit key

        private string DecryptPassword(byte[] encryptedPassword, string key)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    // Ensure the key is a valid size for the AES algorithm
                    aesAlg.Key = Encoding.UTF8.GetBytes(key).Take(32).ToArray();
                    aesAlg.IV = new byte[16]; // Use a unique IV for each encryption

                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (MemoryStream msDecrypt = new MemoryStream(encryptedPassword))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] decryptedBytes = new byte[encryptedPassword.Length];
                                int decryptedByteCount = csDecrypt.Read(decryptedBytes, 0, decryptedBytes.Length);

                                // Trim any padding bytes
                                return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
            catch (CryptographicException ex)
            {
                // Log the exception details
                Console.WriteLine($"Decryption Error: {ex.Message}");
                return null; // Or handle the error in an appropriate way for your application
            }
        }
        */

        // check the near end time in laundry bookings 
        /*

        public void Get_ClosestUnit(Label lblUnitID, Label lbl_UnitName, Label lblStartTime, Label lblEndTime, Label lblTimeLeft, Label lbReservedCount)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Retrieve the closest unit based on Laundry_Bookings and count of reserved bookings
                    using (SqlCommand closestUnitCmd = new SqlCommand("SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time, " +
                                                                      "(SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') AS ReservedCount " +
                                                                      "FROM Laundry_Bookings lb " +
                                                                      "JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID " +
                                                                      "WHERE lb.Bookings_Status = 'In-Progress' " +
                                                                      "ORDER BY ABS(DATEDIFF(minute, lb.End_Time, GETDATE()))", connect))
                    {
                        // Execute the query to get the closest unit
                        using (SqlDataReader reader = closestUnitCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update labels with the closest unit information
                                lblUnitID.Text = reader.GetInt32(0).ToString();
                                lbl_UnitName.Text = reader.GetString(1);

                                // Format and display start time
                                DateTime startTime = reader.GetDateTime(2);
                                lblStartTime.Text = startTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Format and display end time
                                DateTime endTime = reader.GetDateTime(3);
                                lblEndTime.Text = endTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Calculate time left
                                TimeSpan timeLeft = endTime - DateTime.Now;
                                lblTimeLeft.Text = timeLeft.ToString(@"dd\.hh\:mm\:ss");

                                // Display the count of reserved bookings
                                int reservedCount = reader.GetInt32(4);
                                //lbReservedCount.Text = $"Reserved Count: {reservedCount}";
                                lbReservedCount.Text = $"{reservedCount}";

                                //MessageBox.Show($"The closest unit based on Laundry_Bookings is Unit_ID: {lblUnitID.Text}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No available units found in Laundry_Bookings.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        */

        public void ss()
        {
            /*
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();


                // Retrieve the closest unit based on Laundry_Bookings and count of reserved bookings
                using (SqlCommand closestUnitCmd = new SqlCommand("SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time, " +
                                                                  "(SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') AS ReservedCount " +
                                                                  "FROM Laundry_Bookings lb " +
                                                                  "JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID " +
                                                                  "WHERE lb.Bookings_Status = 'In-Progress' " +
                                                                  "ORDER BY ABS(DATEDIFF(minute, lb.End_Time, GETDATE()))", connect))
                {



                    // Execute the query to get the closest unit
                    using (SqlDataReader reader = closestUnitCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Update labels with the closest unit information
                            lblUnitID.Text = reader.GetInt32(0).ToString();
                            lbl_UnitName.Text = reader.GetString(1);

                            // Format and display start time
                            DateTime startTime = reader.GetDateTime(2);
                            lblStartTime.Text = startTime.ToString("h:mm tt / yyyy-MM-dd");

                            // Format and display end time
                            DateTime endTime = reader.GetDateTime(3);
                            lblEndTime.Text = endTime.ToString("h:mm tt / yyyy-MM-dd");

                            // Calculate time left
                            TimeSpan timeLeft = endTime - DateTime.Now;
                            lblTimeLeft.Text = timeLeft.ToString(@"dd\.hh\:mm\:ss");

                            // Display the count of reserved bookings
                            int reservedCount = reader.GetInt32(4);
                            lbReservedCount.Text = $"{reservedCount}";

                            // Calculate the next reserved start time and end time
                            DateTime nextReservedStartTime = endTime.AddMinutes(1); // Assuming a 1-minute buffer
                            DateTime nextReservedEndTime = nextReservedStartTime.AddHours(2); // Assuming a 2-hour reservation

                            // Update labels with the next reserved time information
                            lblReservedStartTime.Text = nextReservedStartTime.ToString("h:mm tt / yyyy-MM-dd");
                            lblReservedEndTime.Text = nextReservedEndTime.ToString("h:mm tt / yyyy-MM-dd");

                            //MessageBox.Show($"The closest unit based on Laundry_Bookings is Unit_ID: {lblUnitID.Text}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No available units found in Laundry_Bookings.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            */
        }

        /*
        public bool Get_ClosestUnit(Label r_unitID, Label lbl_UnitName, Label lblReservedStartTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"
                                   SELECT TOP 1 lu.Unit_Name, lb.End_Time AS ReservedEndTime, 
                                   DATEADD(MINUTE, 1, lb.End_Time) AS PossibleNextStartTime
                                   FROM Laundry_Bookings lb
                                   INNER JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID
                                   WHERE lb.Unit_ID <> @RequestedUnitID
                                   AND lb.Bookings_Status = 'In-Progress'
                                   ORDER BY ReservedEndTime ASC";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@RequestedUnitID", r_unitID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lbl_UnitName.Text = reader["Unit_Name"].ToString();
                                //lblReservedStartTime.Text = reader["PossibleNextStartTime"].ToString();
                                DateTime possibleNextStartTime = (DateTime)reader["PossibleNextStartTime"];
                                lblReservedStartTime.Text = possibleNextStartTime.ToString("h:mm:ss tt");
                                // Additional processing, if needed
                                return true; // Unit found
                            }
                            else
                            {
                                // No suitable unit found
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
        */

        /*
        public void Get_ClosestUnit(int r_unitID,  Label lbl_UnitName, Label lblReservedStartTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public bool Get_ClosestUnit(Label lblUnitID, Label lbl_UnitName, Label lblStartTime, Label lblEndTime, Label lblTimeLeft, Label lbReservedCount, string r_startTime, string r_endTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check for an in-progress booking without any 'Reserved' status for the same Unit_ID
                    using (SqlCommand inProgressCmd = new SqlCommand("SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time, " +
                                                                        "(SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') AS ReservedCount " +
                                                                        "FROM Laundry_Bookings lb " +
                                                                        "JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID " +
                                                                        "WHERE lb.Bookings_Status = 'In-Progress' " +
                                                                        "AND NOT EXISTS (SELECT 1 FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') " +
                                                                        "ORDER BY ABS(DATEDIFF(minute, lb.End_Time, GETDATE()))", connect))
                    {
                        using (SqlDataReader reader = inProgressCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update labels with the closest unit information
                                lblUnitID.Text = reader.GetInt32(0).ToString();
                                lbl_UnitName.Text = reader.GetString(1);

                                // Format and display start time
                                DateTime startTime = reader.GetDateTime(2);
                                lblStartTime.Text = startTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Format and display end time
                                DateTime endTime = reader.GetDateTime(3);
                                lblEndTime.Text = endTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Calculate time left
                                TimeSpan timeLeft = endTime - DateTime.Now;
                                lblTimeLeft.Text = timeLeft.ToString(@"dd\.hh\:mm\:ss");

                                // Display the count of reserved bookings
                                int reservedCount = reader.GetInt32(4);
                                lbReservedCount.Text = $"{reservedCount}";

                                // Calculate the next reserved start time and end time
                                DateTime nextReservedStartTime = endTime.AddMinutes(1); // Assuming a 1-minute buffer
                                DateTime nextReservedEndTime = nextReservedStartTime.AddHours(2); // Assuming a 2-hour reservation

                                // Update labels with the next reserved time information
                                r_startTime = nextReservedStartTime.ToString("h:mm tt / yyyy-MM-dd");
                                r_endTime = nextReservedEndTime.ToString("h:mm tt / yyyy-MM-dd");

                                //MessageBox.Show($"The closest unit based on Laundry_Bookings is Unit_ID: {lblUnitID.Text}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                        }
                    }

                    // If no in-progress booking is found, check for the nearest available booking without 'Reserved'
                    using (SqlCommand closestAvailableCmd = new SqlCommand("SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time, " +
                                                                            "(SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') AS ReservedCount " +
                                                                            "FROM Laundry_Bookings lb " +
                                                                            "JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID " +
                                                                            "WHERE lb.Bookings_Status != 'Reserved' " +
                                                                            "AND NOT EXISTS (SELECT 1 FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') " +
                                                                            "ORDER BY ABS(DATEDIFF(minute, lb.End_Time, GETDATE())), lb.End_Time", connect))
                    {
                        using (SqlDataReader reader = closestAvailableCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update labels with the closest unit information
                                lblUnitID.Text = reader.GetInt32(0).ToString();
                                lbl_UnitName.Text = reader.GetString(1);

                                // Format and display start time
                                DateTime startTime = reader.GetDateTime(2);
                                lblStartTime.Text = startTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Format and display end time
                                DateTime endTime = reader.GetDateTime(3);
                                lblEndTime.Text = endTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Calculate time left
                                TimeSpan timeLeft = endTime - DateTime.Now;
                                lblTimeLeft.Text = timeLeft.ToString(@"dd\.hh\:mm\:ss");

                                // Display the count of reserved bookings
                                int reservedCount = reader.GetInt32(4);
                                lbReservedCount.Text = $"{reservedCount}";

                                // Calculate the next reserved start time and end time
                                DateTime nextReservedStartTime = endTime.AddMinutes(1); // Assuming a 1-minute buffer
                                DateTime nextReservedEndTime = nextReservedStartTime.AddHours(2); // Assuming a 2-hour reservation

                                // Update labels with the next reserved time information
                                r_startTime = nextReservedStartTime.ToString("h:mm tt / yyyy-MM-dd");
                                r_endTime = nextReservedEndTime.ToString("h:mm tt / yyyy-MM-dd");

                                //MessageBox.Show($"The closest unit based on Laundry_Bookings is Unit_ID: {lblUnitID.Text}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            else
                            {
                                // No available units found
                                return false;
                            }
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

        //last try
        /*
        public void Get_ClosestUnit(Label lblUnitID, Label lbl_UnitName, Label lblStartTime, Label lblEndTime, Label lblTimeLeft, Label lbReservedCount, KryptonTextBox lblReservedStartTime, KryptonTextBox lblReservedEndTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check for an in-progress booking
                    using (SqlCommand inProgressCmd = new SqlCommand("SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time, " +
                                                                        "(SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') AS ReservedCount " +
                                                                        "FROM Laundry_Bookings lb " +
                                                                        "JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID " +
                                                                        "WHERE lb.Bookings_Status = 'In-Progress' " +
                                                                        "ORDER BY ABS(DATEDIFF(minute, lb.End_Time, GETDATE()))", connect))
                    {
                        using (SqlDataReader reader = inProgressCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update labels with the closest unit information
                                lblUnitID.Text = reader.GetInt32(0).ToString();
                                lbl_UnitName.Text = reader.GetString(1);

                                // Format and display start time
                                DateTime startTime = reader.GetDateTime(2);
                                lblStartTime.Text = startTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Format and display end time
                                DateTime endTime = reader.GetDateTime(3);
                                lblEndTime.Text = endTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Calculate time left
                                TimeSpan timeLeft = endTime - DateTime.Now;
                                lblTimeLeft.Text = timeLeft.ToString(@"dd\.hh\:mm\:ss");

                                // Display the count of reserved bookings
                                int reservedCount = reader.GetInt32(4);
                                lbReservedCount.Text = $"{reservedCount}";

                                // Calculate the next reserved start time and end time
                                DateTime nextReservedStartTime = endTime.AddMinutes(1); // Assuming a 1-minute buffer
                                DateTime nextReservedEndTime = nextReservedStartTime.AddHours(2); // Assuming a 2-hour reservation

                                // Update labels with the next reserved time information
                                lblReservedStartTime.Text = nextReservedStartTime.ToString("h:mm tt / yyyy-MM-dd");
                                lblReservedEndTime.Text = nextReservedEndTime.ToString("h:mm tt / yyyy-MM-dd");

                                //MessageBox.Show($"The closest unit based on Laundry_Bookings is Unit_ID: {lblUnitID.Text}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }

                    // If no in-progress booking is found, check for the nearest available booking
                    using (SqlCommand closestAvailableCmd = new SqlCommand("SELECT TOP 1 lu.Unit_ID, lu.Unit_Name, lb.Start_Time, lb.End_Time, " +
                                                                            "(SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = lu.Unit_ID AND Bookings_Status = 'Reserved') AS ReservedCount " +
                                                                            "FROM Laundry_Bookings lb " +
                                                                            "JOIN Laundry_Unit lu ON lb.Unit_ID = lu.Unit_ID " +
                                                                            "WHERE lb.Bookings_Status != 'Reserved' " +
                                                                            "ORDER BY ABS(DATEDIFF(minute, lb.End_Time, GETDATE()))", connect))
                    {
                        using (SqlDataReader reader = closestAvailableCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update labels with the closest unit information
                                lblUnitID.Text = reader.GetInt32(0).ToString();
                                lbl_UnitName.Text = reader.GetString(1);

                                // Format and display start time
                                DateTime startTime = reader.GetDateTime(2);
                                lblStartTime.Text = startTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Format and display end time
                                DateTime endTime = reader.GetDateTime(3);
                                lblEndTime.Text = endTime.ToString("h:mm tt / yyyy-MM-dd");

                                // Calculate time left
                                TimeSpan timeLeft = endTime - DateTime.Now;
                                lblTimeLeft.Text = timeLeft.ToString(@"dd\.hh\:mm\:ss");

                                // Display the count of reserved bookings
                                int reservedCount = reader.GetInt32(4);
                                lbReservedCount.Text = $"{reservedCount}";

                                // Calculate the next reserved start time and end time
                                DateTime nextReservedStartTime = endTime.AddMinutes(1); // Assuming a 1-minute buffer
                                DateTime nextReservedEndTime = nextReservedStartTime.AddHours(2); // Assuming a 2-hour reservation

                                // Update labels with the next reserved time information
                                lblReservedStartTime.Text = nextReservedStartTime.ToString("h:mm tt / yyyy-MM-dd");
                                lblReservedEndTime.Text = nextReservedEndTime.ToString("h:mm tt / yyyy-MM-dd");

                                //MessageBox.Show($"The closest unit based on Laundry_Bookings is Unit_ID: {lblUnitID.Text}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("No available units found in Laundry_Bookings.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
        
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
                                   "FROM Laundry_Bookings B " +
                                   "INNER JOIN Laundry_Unit U ON B.Unit_ID = U.Unit_ID " +
                                   "INNER JOIN Customers C ON B.Customer_ID = C.Customer_ID " +
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
                                   "FROM Laundry_Bookings B " +
                                   "INNER JOIN Laundry_Unit U ON B.Unit_ID = U.Unit_ID " +
                                   "INNER JOIN Customers C ON B.Customer_ID = C.Customer_ID " +
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




        public bool Get_AllActivityLog(DataGridView view_activity_log)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Log_ID, User_ID, Log_Date, User_Name, Activity_Type, Description, Status FROM Activity_Log";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int i = 0;

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

        //<<INVENTORY MODULE>>
        // get the guest customer information in customer table in database 
        /*
        public bool Get_InventoryItem(DataGridView view_inventory_item)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string sql = "SELECT * FROM Item WHERE Archive = 0";
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
        */
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

                    string query = "SELECT COUNT(*) FROM Laundry_Bookings WHERE Bookings_Status = 'Pending'";

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



                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /*
      public async Task Get_UpdateCountPendingLabelAsync(Label count_pending)
      {
          try
          {
              using (SqlConnection connection = new SqlConnection(database.MyConnection()))
              {
                  await connection.OpenAsync();

                  string query = "SELECT COUNT(*) FROM Laundry_Bookings WHERE Bookings_Status = 'Pending'";

                  using (SqlCommand command = new SqlCommand(query, connection))
                  {
                      int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                      //count_pending.Text = count.ToString();
                      count_pending.Invoke((MethodInvoker)delegate {
                      count_pending.Text = count.ToString();
                      });
                  }
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
      }
      */

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
                    string query = "SELECT Unit_ID, Unit_Name, Unit_Status FROM Laundry_Unit WHERE Archive = 0";

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
                    string query = "SELECT Unit_ID, Unit_Name, Unit_Status FROM Laundry_Unit WHERE Archive = 1";

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
                    string queryAvailable = "SELECT COUNT(*) FROM Laundry_Unit WHERE Unit_Status = 0 AND Archive = 0";
                    using (SqlCommand commandAvailable = new SqlCommand(queryAvailable, connect))
                    {
                        int countAvailable = Convert.ToInt32(commandAvailable.ExecuteScalar());
                        label_count_available.Text = countAvailable.ToString();
                    }

                    // Count the number of occupied units (Unit_Status = 1) with Archive = 0
                    string queryOccupied = "SELECT COUNT(*) FROM Laundry_Unit WHERE Unit_Status = 1 AND Archive = 0";
                    using (SqlCommand commandOccupied = new SqlCommand(queryOccupied, connect))
                    {
                        int countOccupied = Convert.ToInt32(commandOccupied.ExecuteScalar());
                        label_count_occupied.Text = countOccupied.ToString();
                    }

                    // Count the number of not available units (Unit_Status = 3) with Archive = 0
                    string queryNotAvailable = "SELECT COUNT(*) FROM Laundry_Unit WHERE Unit_Status = 3 AND Archive = 0";
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

                    string query = "SELECT COUNT(*) FROM Laundry_Bookings WHERE Bookings_Status = 'Pending'";

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


      
    }
}
