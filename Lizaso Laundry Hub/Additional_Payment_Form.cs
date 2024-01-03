using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;


namespace Lizaso_Laundry_Hub
{
    public partial class Additional_Payment_Form : KryptonForm
    {
        DB_Connection database = new DB_Connection();

        private Payment_Details_Form paymentDetailsForm;

        public double TotalAmount { get; private set; }

        public Additional_Payment_Form(Payment_Details_Form paymentDetailsForm)
        {
            InitializeComponent();
            Show_Item();
            this.paymentDetailsForm = paymentDetailsForm;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void Clear()
        {
            txt_EnterQyt.Clear();
            txt_EnterQyt.Focus();
        }

        public void Show_Item()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    string query = "SELECT Item_Name FROM Item";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string itemName = reader.GetString(0);
                                // Assuming cbSelectItem is the name of your ComboBox
                                cbSelectItem.Items.Add(itemName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void cbSelectItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selected_item = cbSelectItem.SelectedItem.ToString();

                using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                {
                    connect.Open();

                    // Assuming your database has a column named 'Item_Name' to match the selected item
                    string sql = "SELECT Item_ID, Item_Code, Quantity FROM Item WHERE Item_Name = @ItemName";

                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        command.Parameters.AddWithValue("@ItemName", selected_item);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the values from the reader
                                int itemID = reader.GetInt32(0);
                                string itemCode = reader.GetString(1);
                                int quantity = reader.GetInt32(2);

                                // Display the values in your controls (e.g., text boxes)
                                txt_Stock.Text = quantity.ToString();
                                // You can use itemID and itemCode as needed in your application

                                // For example, if you want to display Item_ID in a TextBox named txt_ItemID
                                // txt_ItemID.Text = itemID.ToString();
                            }
                            else
                            {
                                // Handle the case where the item is not found in the database
                                MessageBox.Show("Item not found in the database.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (cbSelectItem.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the available item.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_EnterQyt.Text))
            {
                MessageBox.Show("Please enter the quantity.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int stockQuantity = Convert.ToInt32(txt_Stock.Text);
                int putQuantity = Convert.ToInt32(txt_EnterQyt.Text);

                if (0 == putQuantity)
                {
                    MessageBox.Show("The quantity is zero");
                }
                else if (putQuantity <= stockQuantity)
                {

                    try
                    {
                        string selectedItemName = cbSelectItem.SelectedItem.ToString();
                        int enter_quantity = Convert.ToInt32(txt_EnterQyt.Text);

                        using (SqlConnection connect = new SqlConnection(database.MyConnection()))
                        {
                            connect.Open();

                            string query = "SELECT Item_ID, Item_Code, Item_Name, Quantity, Price FROM Item WHERE Item_Name = @ItemName";

                            using (SqlCommand command = new SqlCommand(query, connect))
                            {
                                command.Parameters.AddWithValue("@ItemName", selectedItemName);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int itemID = reader.GetInt32(0);
                                        string itemCode = reader.GetString(1);
                                        string itemName = reader.GetString(2);
                                        //int stockQuantity = reader.GetInt32(3);
                                        decimal price = reader.GetDecimal(4);

                                        // Calculate the total price by multiplying price and quantity
                                        decimal totalPrice = price * enter_quantity;

                                        bool itemExists = false;

                                        foreach (DataGridViewRow row in grid_item_selection.Rows)
                                        {
                                            // Assuming that the itemID is in the first column (index 0) of the DataGridView
                                            int existingItemID = Convert.ToInt32(row.Cells["id"].Value);

                                            if (existingItemID == itemID)
                                            {
                                                // Item already exists in the DataGridView
                                                itemExists = true;

                                                // If needed, you can update the existing row with the new quantity and total price
                                                int existingQuantity = Convert.ToInt32(row.Cells["qyt"].Value);
                                                decimal existingTotalPrice = Convert.ToDecimal(row.Cells["tlt_price"].Value);

                                                int newTotalQuantity = existingQuantity + enter_quantity;

                                                // Check if the new total quantity exceeds the stock limit
                                                if (newTotalQuantity > stockQuantity)
                                                {
                                                    MessageBox.Show("Adding this quantity will exceed the stock limit.", "Stock Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    return;
                                                }

                                                row.Cells["qyt"].Value = newTotalQuantity;
                                                row.Cells["tlt_price"].Value = existingTotalPrice + totalPrice;

                                                break; // Exit the loop since find the item
                                            }
                                        }

                                        // Add the data to the DataGridView if the item doesn't exist
                                        if (!itemExists)
                                        {
                                            // Check if the new total quantity exceeds the stock limit
                                            if (enter_quantity > stockQuantity)
                                            {
                                                MessageBox.Show("Adding this quantity will exceed the stock limit.", "Stock Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }

                                            grid_item_selection.Rows.Add(0, itemID, itemCode, itemName, enter_quantity, totalPrice);
                                        }
                                    }
                                }
                            }
                        }
                        Get_Total();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Quantity exceeds the stock limit.");
                    Clear();
                }
            }
        }

        
        public double Get_Total()
        {
            double total = 0;

            foreach (DataGridViewRow item in grid_item_selection.Rows)
            {
                if (item.Cells[5].Value != null && double.TryParse(item.Cells[5].Value.ToString(), out double cellValue))
                {
                    total += cellValue;
                }
            }
            lblAmount.Text = total.ToString();
            return total;  // Return the calculated total
        }


        private void txt_EnterQyt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '\u007F')
            {
                e.Handled = true;
            }
            else
            {
                string currentText = txt_EnterQyt.Text;

                if (currentText == "0" && e.KeyChar != '\b' && e.KeyChar != '\u007F')
                {
                    e.Handled = true;
                }
                else
                {
                    if (currentText.Length >= 3 && !char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void grid_item_selection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_item_selection.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (grid_item_selection.Rows.Count == 0)
            {
                MessageBox.Show("The grid is empty. Add items before proceeding.", "Empty Grid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                double total = Get_Total();  // Get the total from the method
                TotalAmount = total;         // Update TotalAmount

                this.Hide();

                paymentDetailsForm.UpdateAdditionalAmount(TotalAmount);
                paymentDetailsForm.DisableAdditonalPaymentButton();
                paymentDetailsForm.EnableViewDetails();
               
                // Pass the total amount to the CalculateTotalPayment method in Payment_Details_Form
                paymentDetailsForm.CalculateTotalPayment(TotalAmount, 0); // Pass 0 as additional amount
            }
        }

        public List<Item_Data> GetAdditionalItems()
        {
            List<Item_Data> additionalItems = new List<Item_Data>();

            foreach (DataGridViewRow row in grid_item_selection.Rows)
            {
                int itemId = Convert.ToInt32(row.Cells["id"].Value);
                int qty = Convert.ToInt32(row.Cells["qyt"].Value);
                double amount = Convert.ToDouble(row.Cells["tlt_price"].Value);

                Item_Data item = new Item_Data
                {
                    ItemId = itemId,
                    Quantity = qty,
                    Amount = amount
                };

                additionalItems.Add(item);
            }
            return additionalItems;
        }

        public void Clear_Purchase()
        {
            grid_item_selection.Rows.Clear();
            grid_item_selection.Refresh();
            lblAmount.Text = "0";
        }

        private void grid_item_selection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (paymentDetailsForm.btnViewDetails.Enabled == true)
            {
                MessageBox.Show("Unable to delete the saved additional payments.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string column_purchase = grid_item_selection.Columns[e.ColumnIndex].Name;
                if (column_purchase == "Remove")
                {
                    DataGridViewRow selectedRow = grid_item_selection.Rows[e.RowIndex];
                    grid_item_selection.Rows.Remove(selectedRow);
                    Get_Total();
                }

            }
           
        }
    }
}
