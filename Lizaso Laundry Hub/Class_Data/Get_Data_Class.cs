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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms.DataVisualization.Charting;
using static Lizaso_Laundry_Hub.Dashboard_Widget.Calendar_Widget_Form;
using Lizaso_Laundry_Hub.Class_Data;

namespace Lizaso_Laundry_Hub
{
    public class Get_Data_Class
    {
        private DB_Connection database = new DB_Connection();
        private Activity_Log_Class activityLogger;

        public Get_Data_Class()
        {
            activityLogger = new Activity_Log_Class();
        }

        // << DASHBOARD FROM / Delivery Widget Form >>
        // method to get delivery list in table Delivires in database
        public bool Get_DashboardDeliveryList(DataGridView view_delivery_list)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string sql = "SELECT D.Delivery_ID, T.Transaction_ID, C.Customer_Name, D.Delivery_Address, T.Amount, D.Delivery_Status " +
                                 "FROM Delivery_View D " +
                                 "JOIN Transaction_View T ON D.Transaction_ID = T.Transaction_ID " +
                                 "JOIN Bookings_View LB ON T.Booking_ID = LB.Booking_ID " +
                                 "JOIN Customers_View C ON LB.Customer_ID = C.Customer_ID " +
                                 "WHERE D.Delivery_Status = 'In Transit'";  

                    SqlCommand command = new SqlCommand(sql, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    view_delivery_list.Rows.Clear();

                    while (reader.Read())
                    {
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
                    string totalQuery = "SELECT SUM(Amount) FROM Transaction_View";
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
                    string dailyQuery = "SELECT SUM(Amount) FROM Transaction_View WHERE Transaction_Date >= CAST(GETDATE() AS DATE)";
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
                    string weeklyQuery = "SELECT SUM(Amount) FROM Transaction_View WHERE Transaction_Date >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0)";
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
                    string monthlyQuery = "SELECT SUM(Amount) FROM Transaction_View WHERE Transaction_Date >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)";
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

        // << DASHBOARD FORM / Dashboard Widget/ CustomerList Widget Form >>
        // method get all customer data 
        public bool Get_AllCustomerNameandItsCustomerType(DataGridView view_allcustomer)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT Customer_ID, Customer_Name, Customer_Type
                                     FROM Customers_View
                                     WHERE Archive = 0";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            view_allcustomer.Rows.Clear();

                            while (reader.Read())
                            {
                                int customerID = reader.GetInt32(reader.GetOrdinal("Customer_ID"));
                                string customerName = reader.GetString(reader.GetOrdinal("Customer_Name"));

                                int customerTypeValueIndex = reader.GetOrdinal("Customer_Type");

                                if (!reader.IsDBNull(customerTypeValueIndex))
                                {
                                    short customerTypeValue = reader.GetByte(customerTypeValueIndex); 

                                    Console.WriteLine($"Customer_Type Value: {customerTypeValue}");

                                    string customerTypeString = (customerTypeValue == 0) ? "Registered Customer" : "Guest Customer";

                                    view_allcustomer.Rows.Add(0, customerID, customerName, customerTypeString);
                                }
                                else
                                {
                                    Console.WriteLine("Customer_Type is DBNull");
                                }
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

        // << DASHBOARD FORM / Dashboard Widget/  Inventory Widget Form / 
        // method to get all item name and quantity
        public bool Get_AllItem(DataGridView grid_view_item)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Item_Name, Quantity FROM Item_View";
                    SqlCommand cmd = new SqlCommand(query, connect);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        grid_view_item.Rows.Clear(); 

                        while (reader.Read())
                        {
                            string itemName = reader["Item_Name"].ToString();
                            int quantity = Convert.ToInt32(reader["Quantity"]);

                            grid_view_item.Rows.Add(0, itemName, quantity);
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

        // << DASHBOARD FORM / Dashboard Widget/  Inventory Widget Form / 
        // method to count all quantity in inventory and transaction item
        public bool Get_AllCountItemQytAndLoss(Label allItemQyt, Label allItemLoss, Label itemName)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string queryAllItemQyt = "SELECT ISNULL(SUM(Quantity), 0) FROM Item_View";
                    using (SqlCommand cmdAllItemQyt = new SqlCommand(queryAllItemQyt, connect))
                    {
                        int totalItemQuantity = (int)cmdAllItemQyt.ExecuteScalar();
                        allItemQyt.Text = totalItemQuantity.ToString();
                    }

                    string queryAllItemLoss = "SELECT ISNULL(SUM(Item_Quantity), 0) FROM TransactionItem_View";
                    using (SqlCommand cmdAllItemLoss = new SqlCommand(queryAllItemLoss, connect))
                    {
                        int totalItemLoss = (int)cmdAllItemLoss.ExecuteScalar();
                        allItemLoss.Text = totalItemLoss.ToString();
                    }

                    string queryLowestQuantityItem = "SELECT TOP 1 ISNULL(Item_Name, 'No Item Yet') AS Item_Name, ISNULL(Quantity, 0) AS Quantity FROM Item_View ORDER BY Quantity ASC";
                    using (SqlCommand cmdLowestQuantityItem = new SqlCommand(queryLowestQuantityItem, connect))
                    {
                        using (SqlDataReader reader = cmdLowestQuantityItem.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string itemname = reader["Item_Name"].ToString();
                                string itemQyt = reader["Quantity"].ToString();

                                itemName.Text = $"{itemname} ----> {itemQyt}";
                            }
                        }
                    }
                    connect.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << DASHBOARD FORM / Stats Widget Form >> 
        // method to get the most visited customer in store
        public bool Get_ChartMostVisitedCustomer(Chart visited_customer_chart)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT TOP 5 C.Customer_ID, C.Customer_Name, COUNT(LB.Booking_ID) AS VisitCount
                                     FROM Customers_View C
                                     JOIN Bookings_View LB ON C.Customer_ID = LB.Customer_ID
                                     WHERE LB.Bookings_Status = 'Completed'
                                     GROUP BY C.Customer_ID, C.Customer_Name
                                     ORDER BY VisitCount DESC";

                    SqlCommand command = new SqlCommand(query, connect);
                    SqlDataReader reader = command.ExecuteReader();

                    visited_customer_chart.Series.Clear();

                    Series series = visited_customer_chart.Series.Add("Most Visited Customers");
                    series.ChartType = SeriesChartType.Bar;

                    while (reader.Read())
                    {
                        string customerName = reader["Customer_Name"].ToString();
                        int visitCount = Convert.ToInt32(reader["VisitCount"]);

                        series.Points.AddXY(customerName, visitCount);
                    }

                    visited_customer_chart.ChartAreas[0].AxisX.Title = "Customers";
                    visited_customer_chart.ChartAreas[0].AxisY.Title = "Visit Count";

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

        // << DASHBOARD FORM / Stats Widget Form >> 
        // method to get the most busiest day and post it to chart
        public bool Get_ChartMostBusiestDays(Chart busiest_day_chart)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Query to find the top 5 busiest days of the week based on completed bookings
                    string sql = "SELECT TOP 5 DATEPART(WEEKDAY, Start_Time) AS BusiestDay, COUNT(*) AS BookingCount " +
                                 "FROM Laundry_Bookings " +
                                 "WHERE Bookings_Status = 'Completed' " +
                                 "GROUP BY DATEPART(WEEKDAY, Start_Time) " +
                                 "ORDER BY BookingCount DESC";

                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int busiestDay = Convert.ToInt32(reader["BusiestDay"]);
                                int bookingCount = Convert.ToInt32(reader["BookingCount"]);

                                string seriesName = "BookingCount";
                                if (!busiest_day_chart.Series.Any(s => s.Name == seriesName))
                                {
                                    busiest_day_chart.Series.Add(seriesName);
                                    busiest_day_chart.Series[seriesName].ChartType = SeriesChartType.Column;
                                }

                                busiest_day_chart.Series[seriesName].Points.AddXY(GetDayOfWeek(busiestDay), bookingCount);
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

        // Helper method to convert day of week number to string
        private string GetDayOfWeek(int dayOfWeek)
        {
            return Enum.GetName(typeof(DayOfWeek), dayOfWeek - 1);
        }

        // << DASHBOARD FORM / Calendar Widget Form >> 
        // method to get in progress and reserved
        public bool Get_CalendarBookingsInProgressAndReserved(DataGridView grid_progressreserved_view)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT C.Customer_Name, U.Unit_Name, LB.Start_Time, LB.End_Time, LB.Bookings_Status
                                     FROM Bookings_View LB
                                     JOIN Customers_View C ON LB.Customer_ID = C.Customer_ID
                                     JOIN Unit_View U ON LB.Unit_ID = U.Unit_ID
                                     WHERE LB.Bookings_Status IN ('In-Progress', 'Reserved')
                                     ORDER BY LB.Start_Time";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            grid_progressreserved_view.Rows.Clear(); 

                            while (reader.Read())
                            {
                                string customerName = reader["Customer_Name"].ToString();
                                string unitName = reader["Unit_Name"].ToString();
                                DateTime startTime = Convert.ToDateTime(reader["Start_Time"]);
                                DateTime endTime = Convert.ToDateTime(reader["End_Time"]);
                                string bookingStatus = reader["Bookings_Status"].ToString();

                                string timeRange = $"Start Time: {reader["Start_Time"]:MM/dd/yyyy h:mm:ss tt}\nEnd Time: {reader["End_Time"]: MM/dd/yyyy h:mm:ss tt}";

                                grid_progressreserved_view.Rows.Add(customerName, unitName, timeRange, bookingStatus);
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

        // << DASHBOARD FORM / Calendar Widget Form >> 
        // method to get In-Progress Status in Laundry Bookings and put in Calendar
        public List<Calendar_InProgress_Class> Get_CalendarInProgressBookings()
        {
            try
            {
                List<Calendar_InProgress_Class> inProgressBookings = new List<Calendar_InProgress_Class>();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT Unit_Name, Start_Time, End_Time, Bookings_Status
                                     FROM Bookings_View LB
                                     JOIN Unit_View LU ON LB.Unit_ID = LU.Unit_ID
                                     WHERE Bookings_Status = 'In-Progress'";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Calendar_InProgress_Class booking = new Calendar_InProgress_Class
                                {
                                    UnitName = reader["Unit_Name"].ToString(),
                                    StartTime = Convert.ToDateTime(reader["Start_Time"]),
                                    EndTime = Convert.ToDateTime(reader["End_Time"]),
                                    Status = reader["Bookings_Status"].ToString()
                                };

                                inProgressBookings.Add(booking);
                            }
                        }
                    }
                }
                return inProgressBookings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; 
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
                return logDate.ToString(); 
            }
        }

        // << ucProgressList Control  >>
        // method to check the timeleft based on bookingID proviced
        public DateTime RetrieveEndTimeFromDatabase(int bookingID)
        {
            DateTime endTime = DateTime.MinValue; 

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
                                   "WHERE U.Archive = 0"; 

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

                    string query = @"SELECT
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
                MessageBox.Show($"An error occurred: {ex.Message}");
                return null; 
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
                       
                        return null; 
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
                return new User_Permissions_Class(); 
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

                    // its define query with a CASE statement to convert bit values to 'Yes' or 'No'
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

        // << USER FORM >>
        // method to get all permissions of super user
        public bool Get_SuperUserAndPermissions(DataGridView view_super_user)
        {
            try
            {
                view_super_user.Rows.Clear();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();

                    // its define query with a CASE statement to convert bit values to 'Yes' or 'No'
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

        // method to check if that username is exists
        public bool Get_IsUserNameExists(string userName)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT COUNT(*) FROM User_View WHERE User_Name = @UserName";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);

                        int count = (int)command.ExecuteScalar();

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

        // method to prevent to update a certain username
        public bool Get_IsUserNameExistsWhenUpdating(string userName, int userID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT COUNT(*) FROM User_View WHERE User_Name = @UserName AND User_ID != @UserID";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@UserID", userID);

                        int count = (int)command.ExecuteScalar();

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

        // method to show the deleted user means archive is 1 
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
        }

        // << ADD RESERVED FORM >>
        // method to get the closest laundry unit na matatapos na and show it 
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

                                return true; // Unit found
                            }
                            else
                            {
                                // No suitable unit found
                                r_unitID.Text = ""; 
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
                                string timeRange = $"Start Time: {reader["Start_Time"]:MM/dd/yyyy h:mm:ss tt}\nEnd Time: {reader["End_Time"]: MM/dd/yyyy h:mm:ss tt}";

                                // Add "minutes" to Time Left
                                string timeLeft = $"{Math.Max((int)reader["Time_Left"], 0)} minutes";

                                view_progress.Rows.Add(0,
                                    reader["Booking_ID"],
                                    reader["Unit_ID"],
                                    reader["Customer_ID"],
                                    reader["Customer_Name"],
                                    reader["Unit_Name"],
                                    reader["Services_Type"],
                                    reader["Weight"],
                                    timeRange, 
                                    timeLeft    
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
                                string timeRange = $"Reservation start time: {reader["Start_Time"]:MM/dd/yyyy h:mm:ss tt}\nReservation end time: {reader["End_Time"]: MM/dd/yyyy h:mm:ss tt}";

                                // Add "minutes" to Time Left
                                string timeLeft = $"{Math.Max((int)reader["Time_Left"], 0)} minutes";

                                view_reserved.Rows.Add(0,
                                    reader["Booking_ID"],
                                    reader["Unit_ID"],
                                    reader["Customer_ID"],
                                    reader["Customer_Name"],
                                    reader["Unit_Name"],
                                    reader["Services_Type"],
                                    reader["Weight"],
                                    timeRange,  
                                    timeLeft    
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

                    string query = "SELECT Customer_Type FROM Customers WHERE Customer_ID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int customerType))
                        {
                            if (customerType == 0)
                            {
                                ckFreeShipping.Enabled = true;
                            }
                            else
                            {
                                ckFreeShipping.Enabled = false;
                            }

                            return true;
                        }
                        else
                        {
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

        // << USER FORM / Activity Log >>
        // method to get all user activity log
        public bool Get_AllActivityLog(DataGridView view_activity_log)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();
                    string query = "SELECT Log_ID, User_ID, Log_Date, User_Name, Activity_Type, Description, Status FROM Log_View ORDER BY Log_Date DESC";

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

        // << PAYMENTS FORM >>
        //  method to get all pending in laundry bookings
        public bool Get_BookingPending(DataGridView view_pending)
        {
            try
            {
                view_pending.Rows.Clear();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT B.Booking_ID, B.Unit_ID, B.Customer_ID, C.Customer_Name, U.Unit_Name, B.Services_Type, B.Weight, B.Bookings_Status " +
                                   "FROM Bookings_View B " +
                                   "INNER JOIN Unit_View U ON B.Unit_ID = U.Unit_ID " +
                                   "INNER JOIN Customers_View C ON B.Customer_ID = C.Customer_ID " +
                                   "WHERE B.Bookings_Status = 'Pending'";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
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

        // method to count laundry bookings that have pending
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
                            view_transaction_history.Rows.Clear();

                            while (reader.Read())
                            {
                                int transactionID = reader.GetInt32(reader.GetOrdinal("Transaction_ID"));
                                string userName = reader.GetString(reader.GetOrdinal("User_Name"));
                                decimal amount = reader.GetDecimal(reader.GetOrdinal("Amount"));
                                DateTime transactionDate = reader.GetDateTime(reader.GetOrdinal("Transaction_Date"));
                                string paymentMethod = reader.GetString(reader.GetOrdinal("Payment_Method"));
                                string customerName = reader.IsDBNull(reader.GetOrdinal("Customer_Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Customer_Name"));

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

                    string query = "SELECT Unit_ID, Unit_Name, Unit_Status FROM Unit_View WHERE Archive = 0";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            view_unit.Rows.Clear();

                            while (reader.Read())
                            {
                                int unitID = (int)reader["Unit_ID"];
                                string unitName = reader["Unit_Name"].ToString();
                                int unitStatus = (Int16)reader["Unit_Status"];

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

        // SETTINGS FORM / Laundry Unit Configuration / Archive Tab
        // method to show the unit that have archive 1
        public bool Get_UnitDeleted(DataGridView view_unit_delete)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    int i = 0;
                    connect.Open();

                    string query = "SELECT Unit_ID, Unit_Name, Unit_Status FROM Unit_View WHERE Archive = 1";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            view_unit_delete.Rows.Clear();

                            while (reader.Read())
                            {
                                int unitID = (int)reader["Unit_ID"];
                                string unitName = reader["Unit_Name"].ToString();
                                int unitStatus = (Int16)reader["Unit_Status"];

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

        // << SETTINGS FORM >>
        // method to count unit status if its available occupied or not available
        public bool Get_CountAllUnitStatus(Label label_count_available, Label label_count_occupied, Label label_count_notavailable)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string queryAvailable = "SELECT COUNT(*) FROM Unit_View WHERE Unit_Status = 0 AND Archive = 0";
                    using (SqlCommand commandAvailable = new SqlCommand(queryAvailable, connect))
                    {
                        int countAvailable = Convert.ToInt32(commandAvailable.ExecuteScalar());
                        label_count_available.Text = countAvailable.ToString();
                    }

                    string queryOccupied = "SELECT COUNT(*) FROM Unit_View WHERE Unit_Status = 1 AND Archive = 0";
                    using (SqlCommand commandOccupied = new SqlCommand(queryOccupied, connect))
                    {
                        int countOccupied = Convert.ToInt32(commandOccupied.ExecuteScalar());
                        label_count_occupied.Text = countOccupied.ToString();
                    }

                    string queryNotAvailable = "SELECT COUNT(*) FROM Unit_View WHERE Unit_Status = 2 AND Archive = 0";
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

                        label_count_pending.Invoke((MethodInvoker)delegate {
                            label_count_pending.Text = count.ToString();
                        });

                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
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

        // method to get the notification log based on user_ID
        public List<NotificationLog> GetNotificationLog(int userID)
        {
            List<NotificationLog> notificationLogs = new List<NotificationLog>();
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Log_ID, Log_Date, User_Name, Activity_Type, Description FROM Log_View " +
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

        // << MAIN FORM / REGULAR USER FORM >>
        // method to get count of log active to a certain user
        public bool GetActivityLogCount(int userID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT COUNT(*) FROM Log_View WHERE User_ID = @UserID AND Activity_Type = 'Notifications' AND Status = 1";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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

                    string query = "SELECT Address FROM Customers_View WHERE Customer_ID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int addressOrdinal = reader.GetOrdinal("Address");
                                if (!reader.IsDBNull(addressOrdinal))
                                {
                                    string address = reader.GetString(addressOrdinal);
                                    return address;
                                }
                                else
                                {
                                    return "None";
                                }
                            }
                        }
                    }
                    return "None";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "None";
            }
        }

        // << USER FORM >>
        //  method to get all user even its online or offline
        public bool Get_UserOnlineorOffline(DataGridView view_user, int accountUserID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT User_ID, User_Name, Last_Active, Status FROM User_View";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            view_user.Rows.Clear();

                            while (reader.Read())
                            {
                                int userId = reader.GetInt32(reader.GetOrdinal("User_ID"));
                                string userName = reader.GetString(reader.GetOrdinal("User_Name"));
                                DateTime? lastActive = reader.IsDBNull(reader.GetOrdinal("Last_Active")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Last_Active"));
                                string status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : reader.GetString(reader.GetOrdinal("Status"));

                                if (userId != accountUserID)
                                {
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

        // << AVAILABLE SERVICES / ucUnit_Control >>
        // method check who reserved in that laundry unit
        public bool Get_WhoCustomerReserved(int unitID, Label customerNameLabel, Label startTimeLabel, Label endTimeLabel)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = @"SELECT C.Customer_Name, LB.Start_Time, LB.End_Time
                                     FROM Bookings_View LB
                                     INNER JOIN Customers_View C ON LB.Customer_ID = C.Customer_ID
                                     WHERE LB.Unit_ID = @UnitID AND LB.Bookings_Status = 'Reserved'";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@UnitID", unitID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerNameLabel.Text = reader["Customer_Name"].ToString();
                                startTimeLabel.Text = reader["Start_Time"].ToString();
                                endTimeLabel.Text = reader["End_Time"].ToString();

                                return true; 
                            }
                            else
                            {
                                MessageBox.Show("No reservation found for the given unit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // << PAYMENTS FORM / Transaction History Tab >>
        // method get the additional item based on transactionID
        public bool Get_AdditionalItems(int transactionID, DataGridView gridAdditionalItem)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT i.Item_Name, ti.Item_Quantity, ti.Amount " +
                                   "FROM TransactionItem_View ti " +
                                   "JOIN Item_View i ON ti.Item_ID = i.Item_ID " +
                                   "WHERE ti.Transaction_ID = @TransactionID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@TransactionID", transactionID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string itemName = reader.GetString(0);
                                    int itemQuantity = reader.GetInt32(1);
                                    decimal amount = reader.GetDecimal(2);

                                    gridAdditionalItem.Rows.Add(0, itemName, itemQuantity, amount);
                                }
                                return true;
                            }
                            else
                            {
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
    }
}

