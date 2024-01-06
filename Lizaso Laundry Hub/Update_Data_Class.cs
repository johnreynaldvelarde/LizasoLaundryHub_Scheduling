﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub
{
    public class Update_Data_Class
    {
        private DB_Connection database = new DB_Connection();

        // << USER FORM >>
        // update user account
        public bool Update_User(int loggedInUserId, int userId, string username, string password, byte _IsSuperUser, byte _services, byte _schedule, byte _customer, byte _payments, byte _user, byte _inventory, byte _settings)
        {
            try
            {
              
                if (loggedInUserId == userId)
                {
                    MessageBox.Show("You are not authorized to update this user's account.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                bool isCurrentUserSuperUser = IsUserSuper(loggedInUserId);

                if (isCurrentUserSuperUser)
                {
                    MessageBox.Show("You are not authorized to update this user's account.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the provided userId exists in the User_Account table
                    string checkUserSql = "SELECT COUNT(*) FROM User_View WHERE User_ID = @User_ID";
                    using (SqlCommand checkUserCommand = new SqlCommand(checkUserSql, connect))
                    {
                        checkUserCommand.Parameters.AddWithValue("@User_ID", userId);
                        int userCount = (int)checkUserCommand.ExecuteScalar();

                        if (userCount == 0)
                        {
                            // User with the provided userId doesn't exist
                            MessageBox.Show("User with the specified ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // Hash the password (you may want to use a secure hashing algorithm)
                    byte[] passwordHash = HashPassword(password);

                    // Update the user account in the User_Account table
                    string userSql = "UPDATE User_Account SET User_Name = @User_Name, Super_User = @Super_User, Password_Hash = @Password_Hash WHERE User_ID = @User_ID";
                    using (SqlCommand userCommand = new SqlCommand(userSql, connect))
                    {
                        userCommand.Parameters.AddWithValue("@User_ID", userId);
                        userCommand.Parameters.AddWithValue("@User_Name", username);
                        userCommand.Parameters.AddWithValue("@Super_User", _IsSuperUser);
                        userCommand.Parameters.AddWithValue("@Password_Hash", passwordHash);
                        userCommand.ExecuteNonQuery();
                    }

                    // Update the permissions in the User_Permissions table
                    string permissionsSql = "UPDATE User_Permissions SET Available_Services = @Available_Services, Schedule = @Schedule, Customer_Manage = @Customer_Manage, Payments = @Payments, User_Manage = @User_Manage, Inventory = @Inventory, Settings = @Settings WHERE User_ID = @User_ID";
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
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool IsUserSuper(int userId)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Query the Super_User column for the specified user
                    string superUserSql = "SELECT Super_User FROM User_View WHERE User_ID = @User_ID";
                    using (SqlCommand superUserCommand = new SqlCommand(superUserSql, connect))
                    {
                        superUserCommand.Parameters.AddWithValue("@User_ID", userId);

                        // Execute the query and get the Super_User value
                        object result = superUserCommand.ExecuteScalar();

                        // Check if the result is not null and if Super_User is false
                        return result != null && !(bool)result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking if the user is a super user: {ex.Message}");
                return false;
            }
        }

        public bool Update_SuperUserToDeleted(int userID)
        {
            /*
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the quantity is 0 before updating the Archive field
                    string checkQuantitySql = "SELECT Quantity FROM Item WHERE Item_ID = @ItemID";
                    SqlCommand checkQuantityCommand = new SqlCommand(checkQuantitySql, connect);
                    checkQuantityCommand.Parameters.AddWithValue("@ItemID", itemID);

                    int quantity = (int)checkQuantityCommand.ExecuteScalar();

                    if (quantity == 0)
                    {
                        // Update the Archive field to 1 (indicating deleted)
                        string updateSql = "UPDATE Item SET Archive = 1 WHERE Item_ID = @ItemID";
                        SqlCommand updateCommand = new SqlCommand(updateSql, connect);
                        updateCommand.Parameters.AddWithValue("@ItemID", itemID);

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Item cannot be deleted as it still has quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            */
            return false;
        }





        // HashPassword function (replace with a secure hashing algorithm)
        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // << SCHEDULE FORM >>
        // method to cancel the booking
        public bool Update_BookingStatusToCanceled(int bookingID, int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Update Booking_Status to Canceled in Laundry_Bookings table
                    string updateBookingQuery = "UPDATE Laundry_Bookings SET Bookings_Status = 'Canceled' WHERE Booking_ID = @BookingID";
                    using (SqlCommand cmdUpdateBooking = new SqlCommand(updateBookingQuery, connect))
                    {
                        cmdUpdateBooking.Parameters.AddWithValue("@BookingID", bookingID);
                        cmdUpdateBooking.ExecuteNonQuery();
                    }

                    // Update Unit_Status to 0 in Laundry_Unit table
                    string updateUnitQuery = "UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = @UnitID";
                    using (SqlCommand cmdUpdateUnit = new SqlCommand(updateUnitQuery, connect))
                    {
                        cmdUpdateUnit.Parameters.AddWithValue("@UnitID", unitID);
                        cmdUpdateUnit.ExecuteNonQuery();
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

        // << SERVICES_FORM >>
        // method to finish button in ucProgressList_Control 
        public bool Update_BookingStatusToPending(int bookingID, int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the Unit_ID is found in Laundry_Bookings with Bookings_Status equal to 'Reserved'
                    string checkReservedStatusQuery = "SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                    using (SqlCommand cmdCheckReservedStatus = new SqlCommand(checkReservedStatusQuery, connect))
                    {
                        cmdCheckReservedStatus.Parameters.AddWithValue("@UnitID", unitID);
                        int reservedCount = (int)cmdCheckReservedStatus.ExecuteScalar();

                        // Update Bookings_Status to 'In-Progress' when 'Reserved' is found
                        if (reservedCount > 0)
                        {
                            string updateBookingStatusQuery = "UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                            using (SqlCommand cmdUpdateBookingStatus = new SqlCommand(updateBookingStatusQuery, connect))
                            {
                                cmdUpdateBookingStatus.Parameters.AddWithValue("@UnitID", unitID);
                                cmdUpdateBookingStatus.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Update Bookings_Status to 'Pending' when 'Reserved' is not found and the current status is not 'Completed' or 'Canceled'
                            string updateBookingStatusQuery = "UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Unit_ID = @UnitID AND Bookings_Status NOT IN ('Completed', 'Canceled')";
                            using (SqlCommand cmdUpdateBookingStatus = new SqlCommand(updateBookingStatusQuery, connect))
                            {
                                cmdUpdateBookingStatus.Parameters.AddWithValue("@UnitID", unitID);
                                cmdUpdateBookingStatus.ExecuteNonQuery();
                            }
                        }

                        // Update Booking_Status to 'Pending' in Laundry_Bookings table
                        string updateBookingtoPending = "UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID";
                        using (SqlCommand cmdUpdateBooking = new SqlCommand(updateBookingtoPending, connect))
                        {
                            cmdUpdateBooking.Parameters.AddWithValue("@BookingID", bookingID);
                            cmdUpdateBooking.ExecuteNonQuery();
                        }

                        // Update Unit_Status based on the condition
                        string updateUnitQuery = "UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID";
                        using (SqlCommand cmdUpdateUnit = new SqlCommand(updateUnitQuery, connect))
                        {
                            cmdUpdateUnit.Parameters.AddWithValue("@UnitID", unitID);
                            cmdUpdateUnit.Parameters.AddWithValue("@UnitStatus", (reservedCount > 0) ? 1 : 0);
                            cmdUpdateUnit.ExecuteNonQuery();
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

        public bool Update_BookingReservationToCanceled(int bookingID, int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Update Booking_Status to Canceled in Laundry_Bookings table
                    string updateBookingQuery = "UPDATE Laundry_Bookings SET Bookings_Status = 'Canceled' WHERE Booking_ID = @BookingID";
                    using (SqlCommand cmdUpdateBooking = new SqlCommand(updateBookingQuery, connect))
                    {
                        cmdUpdateBooking.Parameters.AddWithValue("@BookingID", bookingID);
                        cmdUpdateBooking.ExecuteNonQuery();
                    }

                    // Update Unit_Status to 0 in Laundry_Unit table
                    string updateUnitQuery = "UPDATE Laundry_Unit SET Unit_Status = 1 WHERE Unit_ID = @UnitID";
                    using (SqlCommand cmdUpdateUnit = new SqlCommand(updateUnitQuery, connect))
                    {
                        cmdUpdateUnit.Parameters.AddWithValue("@UnitID", unitID);
                        cmdUpdateUnit.ExecuteNonQuery();
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


        public bool Update_Unit(int unitID, string unitName, int unitStatus)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the unit is not Occupied before updating
                    string getStatusQuery = "SELECT Unit_Status FROM Laundry_Unit WHERE Unit_ID = @UnitID";
                    using (SqlCommand getStatusCommand = new SqlCommand(getStatusQuery, connect))
                    {
                        getStatusCommand.Parameters.AddWithValue("@UnitID", unitID);
                        int currentStatus = Convert.ToInt32(getStatusCommand.ExecuteScalar());

                        // Check if the unit is not Occupied
                        if (currentStatus != 1)
                        {
                            // Update the unit
                            string updateQuery = "UPDATE Laundry_Unit SET Unit_Name = @UnitName, Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connect))
                            {
                                updateCommand.Parameters.AddWithValue("@UnitID", unitID);
                                updateCommand.Parameters.AddWithValue("@UnitName", unitName);
                                updateCommand.Parameters.AddWithValue("@UnitStatus", unitStatus);

                                updateCommand.ExecuteNonQuery();
                                return true;
                            }
                        }
                        else
                        {
                            // Unit is Occupied, cannot update
                            MessageBox.Show("Cannot update an occupied unit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public bool Update_Delete_Unit(int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check the current status of the unit
                    string getStatusQuery = "SELECT Unit_Status FROM Laundry_Unit WHERE Unit_ID = @UnitID";
                    using (SqlCommand getStatusCommand = new SqlCommand(getStatusQuery, connect))
                    {
                        getStatusCommand.Parameters.AddWithValue("@UnitID", unitID);
                        int unitStatus = Convert.ToInt32(getStatusCommand.ExecuteScalar());

                        // Check if the unit can be deleted (Available or Not Available)
                        if (unitStatus == 0 || unitStatus == 3)
                        {
                            // Proceed with setting Archive to 1
                            string updateQuery = "UPDATE Laundry_Unit SET Archive = 1 WHERE Unit_ID = @UnitID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connect))
                            {
                                updateCommand.Parameters.AddWithValue("@UnitID", unitID);
                                updateCommand.ExecuteNonQuery();
                                return true;
                            }
                        }
                        else
                        {
                            // Unit is Occupied, cannot delete
                            MessageBox.Show("Cannot delete an occupied unit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public bool Update_Recycle_Unit(int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the unitID exists
                    string checkUnitQuery = "SELECT COUNT(*) FROM Laundry_Unit WHERE Unit_ID = @UnitID";
                    using (SqlCommand checkUnitCommand = new SqlCommand(checkUnitQuery, connect))
                    {
                        checkUnitCommand.Parameters.AddWithValue("@UnitID", unitID);
                        int count = Convert.ToInt32(checkUnitCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            // Update the Archive to 0
                            string updateQuery = "UPDATE Laundry_Unit SET Archive = 0 WHERE Unit_ID = @UnitID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connect))
                            {
                                updateCommand.Parameters.AddWithValue("@UnitID", unitID);
                                updateCommand.ExecuteNonQuery();
                                MessageBox.Show("Unit recycled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unit not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // << CUSTOMER_FORM / Registered Customer tab >>
        // update the Archive column to 1 in table Customers
        public bool Update_RegisterCustomerToDeleted(int registerCustomerID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string updateQuery = "UPDATE Customers SET Archive = 1 WHERE Customer_ID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", registerCustomerID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer successfully archived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Customer not found or update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // << CUSTOMER_FORM / Archive tab >>
        // update the Archive column to 1 in table Customers
        public bool Update_GuestCustomerToDeleted(int guestCustomerID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string updateQuery = "UPDATE Customers SET Archive = 1 WHERE Customer_ID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", guestCustomerID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer successfully archived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Customer not found or update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // << CUSTOMER_FORM / Archive tab >>
        // update the Archive column to 0 to recycle the customers
        public bool Update_CustomerRecycleArchive(int archiveID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string updateQuery = "UPDATE Customers SET Archive = 0 WHERE Customer_ID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", archiveID);
                        cmd.ExecuteNonQuery();

                        /*
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer successfully recycle.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Customer not found or update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        */
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }






        // << INVENTORY_FORM /  Items View tab >>
        // update the item information
        public bool Update_InventoryItem(int itemID, string itemName, string categoryItem, decimal itemPrice)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Update the specified fields for the item
                    string updateSql = "UPDATE Item SET Item_Name = @ItemName, Category_Type = @CategoryItem, Price = @ItemPrice WHERE Item_ID = @ItemID";
                    SqlCommand updateCommand = new SqlCommand(updateSql, connect);
                    updateCommand.Parameters.AddWithValue("@ItemID", itemID);
                    updateCommand.Parameters.AddWithValue("@ItemName", itemName);
                    updateCommand.Parameters.AddWithValue("@CategoryItem", categoryItem);
                    updateCommand.Parameters.AddWithValue("@ItemPrice", itemPrice);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Item not found or no changes made.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // << INVENTORY_FORM / Item View >>
        // Update and restock the item quantity
        public bool Update_ItemStock(int itemID, int itemQuantity)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Update the Quantity field for the item by adding the provided itemQuantity
                    string updateSql = "UPDATE Item SET Quantity = Quantity + @ItemQuantity WHERE Item_ID = @ItemID";
                    SqlCommand updateCommand = new SqlCommand(updateSql, connect);
                    updateCommand.Parameters.AddWithValue("@ItemID", itemID);
                    updateCommand.Parameters.AddWithValue("@ItemQuantity", itemQuantity);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item stock updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Item not found or no changes made.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        // << INVENTORY_FORM / Archive tab >>
        // update the item column archive to 1 to means its deleted item
        public bool Update_ItemToDeleted(int itemID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the quantity is 0 before updating the Archive field
                    string checkQuantitySql = "SELECT Quantity FROM Item WHERE Item_ID = @ItemID";
                    SqlCommand checkQuantityCommand = new SqlCommand(checkQuantitySql, connect);
                    checkQuantityCommand.Parameters.AddWithValue("@ItemID", itemID);

                    int quantity = (int)checkQuantityCommand.ExecuteScalar();

                    if (quantity == 0)
                    {
                        // Update the Archive field to 1 (indicating deleted)
                        string updateSql = "UPDATE Item SET Archive = 1 WHERE Item_ID = @ItemID";
                        SqlCommand updateCommand = new SqlCommand(updateSql, connect);
                        updateCommand.Parameters.AddWithValue("@ItemID", itemID);

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Item cannot be deleted as it still has quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }





        /*
        public bool Delete_Unit(int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check the current status of the unit
                    string getStatusQuery = "SELECT Unit_Status FROM Laundry_Unit WHERE Unit_ID = @UnitID";
                    using (SqlCommand getStatusCommand = new SqlCommand(getStatusQuery, connect))
                    {
                        getStatusCommand.Parameters.AddWithValue("@UnitID", unitID);
                        int unitStatus = Convert.ToInt32(getStatusCommand.ExecuteScalar());

                        // Check if the unit can be deleted (Available or Not Available)
                        if (unitStatus == 0 || unitStatus == 3)
                        {
                            // Proceed with deletion
                            string deleteQuery = "DELETE FROM Laundry_Unit WHERE Unit_ID = @UnitID";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connect))
                            {
                                deleteCommand.Parameters.AddWithValue("@UnitID", unitID);
                                deleteCommand.ExecuteNonQuery();
                                return true;
                            }
                        }
                        else
                        {
                            // Unit is Occupied, cannot delete
                            MessageBox.Show("Cannot delete an occupied unit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        */






        /*
        public bool Update_BookingStatusToPending(int bookingID, int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Update Booking_Status to Canceled in Laundry_Bookings table
                    string updateBookingtoPending = "UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID";
                    using (SqlCommand cmdUpdateBooking = new SqlCommand(updateBookingtoPending, connect))
                    {
                        cmdUpdateBooking.Parameters.AddWithValue("@BookingID", bookingID);
                        cmdUpdateBooking.ExecuteNonQuery();
                    }

                    // Update Unit_Status to 0 in Laundry_Unit table
                    string updateUnitQuery = "UPDATE Laundry_Unit SET Unit_Status = 0 WHERE Unit_ID = @UnitID";
                    using (SqlCommand cmdUpdateUnit = new SqlCommand(updateUnitQuery, connect))
                    {
                        cmdUpdateUnit.Parameters.AddWithValue("@UnitID", unitID);
                        cmdUpdateUnit.ExecuteNonQuery();
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
        
        public bool Update_BookingStatusToPending(int bookingID, int unitID)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Check if the Unit_ID is found in Laundry_Bookings with Bookings_Status equal to 'Reserved'
                    string checkReservedStatusQuery = "SELECT COUNT(*) FROM Laundry_Bookings WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                    using (SqlCommand cmdCheckReservedStatus = new SqlCommand(checkReservedStatusQuery, connect))
                    {
                        cmdCheckReservedStatus.Parameters.AddWithValue("@UnitID", unitID);
                        int reservedCount = (int)cmdCheckReservedStatus.ExecuteScalar();

                        // Update Bookings_Status to 'In-Progress' when 'Reserved' is found
                        if (reservedCount > 0)
                        {
                            string updateBookingStatusQuery = "UPDATE Laundry_Bookings SET Bookings_Status = 'In-Progress' WHERE Unit_ID = @UnitID AND Bookings_Status = 'Reserved'";
                            using (SqlCommand cmdUpdateBookingStatus = new SqlCommand(updateBookingStatusQuery, connect))
                            {
                                cmdUpdateBookingStatus.Parameters.AddWithValue("@UnitID", unitID);
                                cmdUpdateBookingStatus.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Update Bookings_Status to 'Pending' when 'Reserved' is not found
                            string updateBookingStatusQuery = "UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Unit_ID = @UnitID";
                            using (SqlCommand cmdUpdateBookingStatus = new SqlCommand(updateBookingStatusQuery, connect))
                            {
                                cmdUpdateBookingStatus.Parameters.AddWithValue("@UnitID", unitID);
                                cmdUpdateBookingStatus.ExecuteNonQuery();
                            }
                        }

                        // Update Booking_Status to 'Pending' in Laundry_Bookings table
                        string updateBookingtoPending = "UPDATE Laundry_Bookings SET Bookings_Status = 'Pending' WHERE Booking_ID = @BookingID";
                        using (SqlCommand cmdUpdateBooking = new SqlCommand(updateBookingtoPending, connect))
                        {
                            cmdUpdateBooking.Parameters.AddWithValue("@BookingID", bookingID);
                            cmdUpdateBooking.ExecuteNonQuery();
                        }

                        // Update Unit_Status based on the condition
                        string updateUnitQuery = "UPDATE Laundry_Unit SET Unit_Status = @UnitStatus WHERE Unit_ID = @UnitID";
                        using (SqlCommand cmdUpdateUnit = new SqlCommand(updateUnitQuery, connect))
                        {
                            cmdUpdateUnit.Parameters.AddWithValue("@UnitID", unitID);
                            cmdUpdateUnit.Parameters.AddWithValue("@UnitStatus", (reservedCount > 0) ? 1 : 0);
                            cmdUpdateUnit.ExecuteNonQuery();
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

        */


        public bool Delete_Unit2()
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

        // Back
    }
}
