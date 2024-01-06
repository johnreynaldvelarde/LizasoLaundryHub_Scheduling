﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Lizaso_Laundry_Hub
{
    public class Insert_Data_Class
    {
        private DB_Connection database = new DB_Connection();
        Notify_Module.Side_Notification_Form notify = new Notify_Module.Side_Notification_Form();
        // << LOGIN MODULE >>
        // Create superuser when the user table in database is empty
        public void Automatic_Create_Super_User()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) FROM User_Account", connect);
                    int userCount = (int)countCommand.ExecuteScalar();

                    if (userCount == 0)
                    {
                        // Create a new superuser account
                        string username = "Admin";
                        string password = "secret12345";

                        // Hash the password (you may want to use a secure hashing algorithm)
                        byte[] passwordHash = HashPassword(password);

                        // Insert the new superuser account into the User_Account table
                        string userSql = "INSERT INTO User_Account (User_Name, Super_User, Password_Hash, Date_Added, Archive) VALUES (@User_Name, 1,  @Password_Hash,  @Date_Added, @Archive)";
                        using (SqlCommand userCommand = new SqlCommand(userSql, connect))
                        {
                            userCommand.Parameters.AddWithValue("@User_Name", username);
                            userCommand.Parameters.AddWithValue("@Password_Hash", passwordHash);
                            userCommand.Parameters.AddWithValue("@Date_Added", DateTime.Now);
                            userCommand.Parameters.AddWithValue("@Archive", 0);
                            userCommand.ExecuteNonQuery();
                        }

                        // Get the User_ID of the newly created superuser
                        string getUserIdSql = "SELECT User_ID FROM User_Account WHERE User_Name = @User_Name";
                        int userId;
                        using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdSql, connect))
                        {
                            getUserIdCommand.Parameters.AddWithValue("@User_Name", username);
                            userId = (int)getUserIdCommand.ExecuteScalar();
                        }

                        // Insert record into User_Permissions with all permissions set to 1
                        string permissionsSql = "INSERT INTO User_Permissions (User_ID, Available_Services, Schedule, Customer_Manage, Payments, User_Manage, Inventory, Settings) VALUES (@User_ID, 1, 1, 1, 1, 1, 1, 1)";
                        using (SqlCommand permissionsCommand = new SqlCommand(permissionsSql, connect))
                        {
                            permissionsCommand.Parameters.AddWithValue("@User_ID", userId);
                            permissionsCommand.ExecuteNonQuery();
                        }

                        //MessageBox.Show("Superuser account created successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // << LOGIN MODULE >>
        // HashPassword function (replace with a secure hashing algorithm)
        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // << AVAILABLE SERVICES >>
        // Insert data in Laundry Bookings table
        public bool Set_LaundryBookings(int _unitID, int _customerID, string _services, decimal _weight, string startTime, string endTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string sqlBookings = "INSERT INTO Laundry_Bookings (Unit_ID, Customer_ID, Services_Type, Weight, Start_Time, End_Time, Bookings_Status) " +
                                        "VALUES (@Unit_ID, @Customer_ID, @Services_Type, @Weight, @Start_Time, @End_Time, 'In-Progress')";
                    using (SqlCommand command = new SqlCommand(sqlBookings, connect))
                    {
                        command.Parameters.AddWithValue("@Unit_ID", _unitID);
                        command.Parameters.AddWithValue("@Customer_ID", _customerID);
                        command.Parameters.AddWithValue("@Services_Type", _services);
                        command.Parameters.AddWithValue("@Weight", _weight);
                        command.Parameters.AddWithValue("@Start_Time", startTime);
                        command.Parameters.AddWithValue("@End_Time", endTime);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update Laundry_Unit status (assuming a condition, replace with your logic)
                            string sqlUpdateUnit = "UPDATE Laundry_Unit SET Unit_Status = 1 WHERE Unit_ID = @Unit_ID";
                            using (SqlCommand commandUpdateStatus = new SqlCommand(sqlUpdateUnit, connect))
                            {
                                commandUpdateStatus.Parameters.AddWithValue("@Unit_ID", _unitID);

                                int statusRowsAffected = commandUpdateStatus.ExecuteNonQuery();

                                if (statusRowsAffected > 0)
                                {
                                    MessageBox.Show("Laundry booking successful.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to update Laundry_Unit status.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Laundry booking failed.");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception (display a message, log it, etc.)
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }


        /*
        public bool Set_LaundryBookings(int _unitID, int _customerID, string _services, decimal _weight, string startTime, string endTime)
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                string sqlBookings = "INSERT INTO Laundry_Bookings (Unit_ID, Customer_ID, Services_Type, Weight, Start_Time, End_Time, Bookings_Status) " +
                                     "VALUES (@Unit_ID, @Customer_ID, @Services_Type, @Weight, GETDATE(), DATEADD(HOUR, 2, GETDATE()), 'In-Progress')";
                using (SqlCommand command = new SqlCommand(sqlBookings, connect))
                {
                    command.Parameters.AddWithValue("@Unit_ID", _unitID);
                    command.Parameters.AddWithValue("@Customer_ID", _customerID);
                    command.Parameters.AddWithValue("@Services_Type", _services);
                    command.Parameters.AddWithValue("@Weight", _weight);
                    command.Parameters.AddWithValue("@Start_Time", _weight);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update Laundry_Unit status (assuming a condition, replace with your logic)
                        string sqlUpdateUnit = "UPDATE Laundry_Unit SET Unit_Status = 1 WHERE Unit_ID = @Unit_ID";
                        using (SqlCommand commandUpdateStatus = new SqlCommand(sqlUpdateUnit, connect))
                        {
                            commandUpdateStatus.Parameters.AddWithValue("@Unit_ID", _unitID);

                            int statusRowsAffected = commandUpdateStatus.ExecuteNonQuery();

                            if (statusRowsAffected > 0)
                            {
                                MessageBox.Show("Laundry booking successful.");


                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Laundry booking failed.");
                    }
                }
            }
            return true;
        }
        */

        // << ADD LAUNDRY UNIT >>
        // Create Laundry Unit
        public bool Set_Unit(int _status)
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                // Retrieve the current maximum unit number
                string getMaxUnitNumberSql = "SELECT ISNULL(MAX(CONVERT(INT, SUBSTRING(Unit_Name, 6, LEN(Unit_Name) - 5))), 0) + 1 FROM Laundry_Unit";
                SqlCommand getMaxUnitNumberCommand = new SqlCommand(getMaxUnitNumberSql, connect);

                // Check if the table is empty
                object result = getMaxUnitNumberCommand.ExecuteScalar();
                int maxUnitNumber = result == DBNull.Value ? 1 : Convert.ToInt32(result);

                // Construct the new Unit_Name
                string newUnitName = "Unit " + maxUnitNumber;

                string sql = "INSERT INTO Laundry_Unit (Unit_Name, Unit_Status, Archive) " +
                             "VALUES (@Unit_Name, @Unit_Status, @Archive)";

                SqlCommand command = new SqlCommand(sql, connect);
                command.Parameters.AddWithValue("@Unit_Name", newUnitName);
                command.Parameters.AddWithValue("@Unit_Status", _status);
                command.Parameters.AddWithValue("@Archive", 0); // Set Archive to 0

                command.ExecuteNonQuery();

                connect.Close();
                MessageBox.Show("Successfully saved with Unit_Name: " + newUnitName);
            }
            return true;
        }




        /*
        public bool Set_Unit(int _status)
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                // Retrieve the current maximum unit number
                string getMaxUnitNumberSql = "SELECT ISNULL(MAX(CONVERT(INT, SUBSTRING(Unit_Name, 6, LEN(Unit_Name) - 5))), 0) + 1 FROM Laundry_Unit";
                SqlCommand getMaxUnitNumberCommand = new SqlCommand(getMaxUnitNumberSql, connect);

                // Check if the table is empty
                object result = getMaxUnitNumberCommand.ExecuteScalar();
                int maxUnitNumber = result == DBNull.Value ? 1 : Convert.ToInt32(result);

                // Construct the new Unit_Name
                string newUnitName = "Unit " + maxUnitNumber;


                string sql = "INSERT INTO Laundry_Unit (Unit_Name, Unit_Status) " +
                             "VALUES (@Unit_Name, @Unit_Status)";

                SqlCommand command = new SqlCommand(sql, connect);
                command.Parameters.AddWithValue("@Unit_Name", newUnitName);
                command.Parameters.AddWithValue("@Unit_Status", _status);
                command.ExecuteNonQuery();

                connect.Close();
                MessageBox.Show("Successfully saved with Unit_Name: " + newUnitName);
            }
            return true;
        }
        */


        // INVENTORY MODULE >>
        // Create Item in Inventory
        public bool Set_ItemDetails(string _itemName, string _categoryItem, decimal _itemPrice, int _qyt)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {

                    connect.Open();

                    string getMaxItemNumber = "SELECT ISNULL(MAX(CONVERT(INT, SUBSTRING(Item_Code, 4, LEN(Item_Code) - 3))), 0) + 1 FROM Item";
                    SqlCommand getmaxcommand = new SqlCommand(getMaxItemNumber, connect);

                    object result = getmaxcommand.ExecuteScalar();
                    int maxItemNumber = result == DBNull.Value ? 1 : Convert.ToInt32(result);

                    // Construct the new Item_Code
                    string newItemCode = "ITM" + maxItemNumber.ToString("0000");

                    // Insert the new item into the database
                    string insertQuery = "INSERT INTO Item (Item_Code, Item_Name, Category_Type, Price, Quantity, Date_Added, Archive) VALUES (@Item_Code, @Item_Name, @Category_Type, @Price, @Quantity, @Date_Added, @Archive)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connect);

                    insertCommand.Parameters.AddWithValue("@Item_Code", newItemCode);
                    insertCommand.Parameters.AddWithValue("@Item_Name", _itemName);
                    insertCommand.Parameters.AddWithValue("@Category_Type", _categoryItem);
                    insertCommand.Parameters.AddWithValue("@Price", _itemPrice);
                    insertCommand.Parameters.AddWithValue("@Quantity", _qyt);
                    insertCommand.Parameters.AddWithValue("@Date_Added", DateTime.Now);
                    insertCommand.Parameters.AddWithValue("@Archive", 0);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item added successfully");

                    }
                    else
                    {
                        MessageBox.Show("Error adding item");
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

        /*
        // Create Payment Transaction 
        public int Set_PaymentDetails(int _unitID, int _bookingID, int _userID, decimal _amount, string _paymentMethod)
        {
            int transactionID = -1; // Initialize with an invalid value

            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                // Insert payment details into Transactions table and retrieve Transaction_ID
                string insertCmdText = "INSERT INTO Transactions (Booking_ID, User_ID, Amount, Transaction_Date, Payment_Method) " +
                                       "VALUES (@BookingID, @UserID, @Amount, GETDATE(), @PaymentMethod); SELECT SCOPE_IDENTITY()";

                using (SqlCommand insertCmd = new SqlCommand(insertCmdText, connect))
                {
                    insertCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    insertCmd.Parameters.AddWithValue("@UserID", _userID);
                    insertCmd.Parameters.AddWithValue("@Amount", _amount);
                    insertCmd.Parameters.AddWithValue("@PaymentMethod", _paymentMethod);

                    // Execute the query and retrieve the new Transaction_ID
                    var result = insertCmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        transactionID = Convert.ToInt32(result);
                    }
                }

                // Update Booking_Status to 'Complete' in Laundry_Bookings table
                using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'Completed' WHERE Booking_ID = @BookingID", connect))
                {
                    updateBookingCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    updateBookingCmd.ExecuteNonQuery();
                }

                // Update Unit_Status 
                using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = @UnitID", connect))
                {
                    updateUnitCmd.Parameters.AddWithValue("@UnitID", _unitID);
                    updateUnitCmd.ExecuteNonQuery();
                }
            }

            return transactionID;
        }
        */

        public void ss()
        {
            /*
              // Add a condition to check the existing Booking_Status based on Unit_ID
              string checkBookingStatusCmdText = "SELECT Bookings_Status FROM Laundry_Bookings WHERE Unit_ID = @UnitID";
              using (SqlCommand checkBookingStatusCmd = new SqlCommand(checkBookingStatusCmdText, connect))
              {
                  checkBookingStatusCmd.Parameters.AddWithValue("@UnitID", _unitID);

                  var bookingStatusResult = checkBookingStatusCmd.ExecuteScalar();
                  if (bookingStatusResult != null && bookingStatusResult != DBNull.Value)
                  {
                      string currentBookingStatus = bookingStatusResult.ToString();

                      // Check if the current Booking_Status is "Reserved"
                      if (string.Equals(currentBookingStatus, "Reserved", StringComparison.OrdinalIgnoreCase))
                      {
                          // Update Booking_Status to 'In-Progress' in Laundry_Bookings table
                          using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Unit_ID = @UnitID", connect))
                          {
                              updateBookingCmd.Parameters.AddWithValue("@UnitID", _unitID);
                              updateBookingCmd.ExecuteNonQuery();
                          }
                      }
                      // else: Do nothing if the current Booking_Status is not "Reserved"
                  }
              }
        }

        public int Set_PaymentDetails(int _unitID, int _bookingID, int _userID, decimal _amount, string _paymentMethod)
        {
            int transactionID = -1; // Initialize with an invalid value

            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                // Find all records with Booking_Status as "Reserved" based on Unit_ID
                List<int> reservedBookingIDs = new List<int>();

                string findReservedBookingsCmdText = "SELECT Booking_ID FROM Laundry_Bookings WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                using (SqlCommand findReservedBookingsCmd = new SqlCommand(findReservedBookingsCmdText, connect))
                {
                    findReservedBookingsCmd.Parameters.AddWithValue("@UnitID", _unitID);

                    using (SqlDataReader reader = findReservedBookingsCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservedBookingIDs.Add(reader.GetInt32(0));
                        }
                    }
                }

                // Update Booking_Status to 'In-Progress' for each record
                foreach (int bookingID in reservedBookingIDs)
                {
                    using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Booking_ID = @BookingID", connect))
                    {
                        updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        updateBookingCmd.ExecuteNonQuery();
                    }
                }

                // Insert payment details into Transactions table and retrieve Transaction_ID
                string insertCmdText = "INSERT INTO Transactions (Booking_ID, User_ID, Amount, Transaction_Date, Payment_Method) " +
                                       "VALUES (@BookingID, @UserID, @Amount, GETDATE(), @PaymentMethod); SELECT SCOPE_IDENTITY()";

                using (SqlCommand insertCmd = new SqlCommand(insertCmdText, connect))
                {
                    insertCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    insertCmd.Parameters.AddWithValue("@UserID", _userID);
                    insertCmd.Parameters.AddWithValue("@Amount", _amount);
                    insertCmd.Parameters.AddWithValue("@PaymentMethod", _paymentMethod);

                    // Execute the query and retrieve the new Transaction_ID
                    var result = insertCmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        transactionID = Convert.ToInt32(result);
                    }
                }

                // Update Booking_Status to 'Completed' in Laundry_Bookings table
                using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'Completed' WHERE Booking_ID = @BookingID", connect))
                {
                    updateBookingCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    updateBookingCmd.ExecuteNonQuery();
                }

                // Update Unit_Status 
                using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = @UnitID", connect))
                {
                    updateUnitCmd.Parameters.AddWithValue("@UnitID", _unitID);
                    updateUnitCmd.ExecuteNonQuery();
                }
            }

            return transactionID;
              */

        }

        public int Set_PaymentDetails(int _unitID, int _bookingID, int _userID, decimal _amount, string _paymentMethod)
        {
            int transactionID = -1; // Initialize with an invalid value

            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                // Find all records with Booking_Status as "Reserved" based on Unit_ID
                List<int> reservedBookingIDs = new List<int>();

                string findReservedBookingsCmdText = "SELECT Booking_ID FROM Laundry_Bookings WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                using (SqlCommand findReservedBookingsCmd = new SqlCommand(findReservedBookingsCmdText, connect))
                {
                    findReservedBookingsCmd.Parameters.AddWithValue("@UnitID", _unitID);

                    using (SqlDataReader reader = findReservedBookingsCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservedBookingIDs.Add(reader.GetInt32(0));
                        }
                    }
                }

                /*
                // Update Booking_Status to 'In-Progress' for each record
                foreach (int bookingID in reservedBookingIDs)
                {
                    using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Booking_ID = @BookingID", connect))
                    {
                        updateBookingCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        updateBookingCmd.ExecuteNonQuery();
                    }
                }
                */

                // Insert payment details into Transactions table and retrieve Transaction_ID
                string insertCmdText = "INSERT INTO Transactions (Booking_ID, User_ID, Amount, Transaction_Date, Payment_Method) " +
                                       "VALUES (@BookingID, @UserID, @Amount, GETDATE(), @PaymentMethod); SELECT SCOPE_IDENTITY()";

                using (SqlCommand insertCmd = new SqlCommand(insertCmdText, connect))
                {
                    insertCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    insertCmd.Parameters.AddWithValue("@UserID", _userID);
                    insertCmd.Parameters.AddWithValue("@Amount", _amount);
                    insertCmd.Parameters.AddWithValue("@PaymentMethod", _paymentMethod);

                    // Execute the query and retrieve the new Transaction_ID
                    var result = insertCmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        transactionID = Convert.ToInt32(result);
                    }
                }

                // Update Booking_Status to 'Completed' in Laundry_Bookings table
                using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'Completed' WHERE Booking_ID = @BookingID", connect))
                {
                    updateBookingCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    updateBookingCmd.ExecuteNonQuery();
                }

                // Update Unit_Status based on whether there are reserved bookings
                int unitStatus = (reservedBookingIDs.Count > 0) ? 1 : 0;
                using (SqlCommand updateUnitCmd = new SqlCommand("UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID", connect))
                {
                    updateUnitCmd.Parameters.AddWithValue("@UnitID", _unitID);
                    updateUnitCmd.Parameters.AddWithValue("@UnitStatus", unitStatus);
                    updateUnitCmd.ExecuteNonQuery();
                }
            }

            return transactionID;
        }

        // Create aditional payment
        public bool Set_AdditionalPayment(int transactionID, List<Item_Data> additionalItems, double totalAmount)
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                // Iterate through additional items
                foreach (Item_Data item in additionalItems)
                {
                    // Insert into Transaction_Item table
                    string insertItemQuery = "INSERT INTO Transaction_Item (Transaction_ID, Item_ID, Item_Quantity, Amount) " +
                                             "VALUES (@TransactionID, @ItemID, @ItemQuantity, @Amount)";

                    using (SqlCommand itemCmd = new SqlCommand(insertItemQuery, connect))
                    {
                        itemCmd.Parameters.AddWithValue("@TransactionID", transactionID);
                        itemCmd.Parameters.AddWithValue("@ItemID", item.ItemId);
                        itemCmd.Parameters.AddWithValue("@ItemQuantity", item.Quantity);
                        itemCmd.Parameters.AddWithValue("@Amount", item.Amount);

                        itemCmd.ExecuteNonQuery();
                    }

                    // Subtract the quantity from the Item table
                    string updateItemQuery = "UPDATE Item SET Quantity = Quantity - @ItemQuantity WHERE Item_ID = @ItemID";

                    using (SqlCommand updateItemCmd = new SqlCommand(updateItemQuery, connect))
                    {
                        updateItemCmd.Parameters.AddWithValue("@ItemQuantity", item.Quantity);
                        updateItemCmd.Parameters.AddWithValue("@ItemID", item.ItemId);

                        updateItemCmd.ExecuteNonQuery();
                    }
                }
            }
            return true;
        }

        // Create user acount with permission or not
        public bool Set_CreateUser(string username, string password, byte _IsSuperUser, byte _services, byte _schedule, byte _customer, byte _payments, byte _user, byte _inventory, byte _settings)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Hash the password (you may want to use a secure hashing algorithm)
                    byte[] passwordHash = HashPassword(password);

                    // Insert the new user account into the User_Account table
                    string userSql = "INSERT INTO User_Account (User_Name, Super_User, Password_Hash, Date_Added, Archive) VALUES (@User_Name, @Super_User,  @Password_Hash,  @Date_Added, @Archive)";
                    using (SqlCommand userCommand = new SqlCommand(userSql, connect))
                    {
                        userCommand.Parameters.AddWithValue("@User_Name", username);
                        userCommand.Parameters.AddWithValue("@Super_User", _IsSuperUser);
                        userCommand.Parameters.AddWithValue("@Password_Hash", passwordHash);
                        userCommand.Parameters.AddWithValue("@Date_Added", DateTime.Now);
                        userCommand.Parameters.AddWithValue("@Archive", 0);
                        userCommand.ExecuteNonQuery();
                    }

                    // Get the User_ID of the newly created user
                    string getUserIdSql = "SELECT User_ID FROM User_Account WHERE User_Name = @User_Name";
                    int userId;
                    using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdSql, connect))
                    {
                        getUserIdCommand.Parameters.AddWithValue("@User_Name", username);
                        userId = (int)getUserIdCommand.ExecuteScalar();
                    }

                    // Insert record into User_Permissions with specified permissions
                    string permissionsSql = "INSERT INTO User_Permissions (User_ID, Available_Services, Schedule, Customer_Manage, Payments, User_Manage, Inventory, Settings) VALUES (@User_ID, @Available_Services, @Schedule, @Customer_Manage, @Payments, @User_Manage, @Inventory, @Settings)";
                    using (SqlCommand permissionsCommand = new SqlCommand(permissionsSql, connect))
                    {
                        permissionsCommand.Parameters.AddWithValue("@User_ID", userId);
                        permissionsCommand.Parameters.AddWithValue("@Available_Services", _services);
                        permissionsCommand.Parameters.AddWithValue("@Schedule", _schedule);
                        permissionsCommand.Parameters.AddWithValue("@Customer_Manage", _customer);
                        permissionsCommand.Parameters.AddWithValue("@Payments", _payments);
                        permissionsCommand.Parameters.AddWithValue("@User_Manage", _user);
                        permissionsCommand.Parameters.AddWithValue("@Inventory", _inventory);
                        permissionsCommand.Parameters.AddWithValue("@Settings", _settings);
                        permissionsCommand.ExecuteNonQuery();
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

        // reserved a unit
        public bool Set_ReservedUnit(int _unitID, int _customerID, string _services, decimal _weight, string _r_startTime, string _r_EndTime)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Assuming "Reserved" as the booking status for reserved units
                    string bookingStatus = "Reserved";

                    string sql = "INSERT INTO Laundry_Bookings (Unit_ID, Customer_ID, Services_Type, Weight, Start_Time, End_Time, Bookings_Status) " +
                                 "VALUES (@Unit_ID, @Customer_ID, @Services_Type, @Weight, @Start_Time, @End_Time,  @Bookings_Status)";
                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        command.Parameters.AddWithValue("@Unit_ID", _unitID);
                        command.Parameters.AddWithValue("@Customer_ID", _customerID);
                        command.Parameters.AddWithValue("@Services_Type", _services);
                        command.Parameters.AddWithValue("@Weight", _weight);
                        command.Parameters.AddWithValue("@Bookings_Status", bookingStatus);
                        command.Parameters.AddWithValue("@Start_Time", _r_startTime);
                        command.Parameters.AddWithValue("@End_Time", _r_EndTime);
                        command.ExecuteNonQuery();
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

        // insert customer information in database 
        public bool Set_CustomerInformation(byte typeCustomer, string customerName, string contactNumber, string emailAddress, string address)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string sql = "INSERT INTO Customers (Customer_Name, Contact_Number, Email_Address, Address, Customer_Type, Date_Added, Archive) " +
                                   "VALUES (@CustomerName, @ContactNumber, @EmailAddress, @Address, @CustomerType, @DateAdded, @Archive)";

                    using (SqlCommand cmd = new SqlCommand(sql, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);
                        cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@CustomerType", typeCustomer);
                        cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Archive", 0);
                        cmd.ExecuteNonQuery();
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


        public int Set_PendingDetails(int _unitID, int _bookingID, int _userID, decimal _amount, string _paymentMethod)
        {
            int transactionID = -1; // Initialize with an invalid value

            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                // Insert payment details into Transactions table and retrieve Transaction_ID
                string insertCmdText = "INSERT INTO Transactions (Booking_ID, User_ID, Amount, Transaction_Date, Payment_Method) " +
                                       "VALUES (@BookingID, @UserID, @Amount, GETDATE(), @PaymentMethod); SELECT SCOPE_IDENTITY()";

                using (SqlCommand insertCmd = new SqlCommand(insertCmdText, connect))
                {
                    insertCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    insertCmd.Parameters.AddWithValue("@UserID", _userID);
                    insertCmd.Parameters.AddWithValue("@Amount", _amount);
                    insertCmd.Parameters.AddWithValue("@PaymentMethod", _paymentMethod);

                    // Execute the query and retrieve the new Transaction_ID
                    var result = insertCmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        transactionID = Convert.ToInt32(result);
                    }
                }

                // Update Booking_Status to 'Completed' in Laundry_Bookings table
                using (SqlCommand updateBookingCmd = new SqlCommand("UPDATE Laundry_Bookings SET Bookings_Status = 'Completed' WHERE Booking_ID = @BookingID", connect))
                {
                    updateBookingCmd.Parameters.AddWithValue("@BookingID", _bookingID);
                    updateBookingCmd.ExecuteNonQuery();
                }
            }

            return transactionID;
        }

        // << ACTIVITY LOG >>
        // method to insert action of user in table activit log in database 
        public bool Set_ActivityLog(int userID, string userName, string activityType, string description)
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
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@LogDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UserName", userName);
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

        public bool Now4()
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
