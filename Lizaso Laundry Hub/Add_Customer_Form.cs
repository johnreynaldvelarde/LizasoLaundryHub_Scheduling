using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Lizaso_Laundry_Hub
{
    public partial class Add_Customer_Form : KryptonForm
    {
        DB_Connection database = new DB_Connection();
        Customer_Form frm;

        private Insert_Data_Class insertData;
        private Update_Data_Class updateData;

        public int registerCustomeID;
        public int guestCustomerID;

        public Add_Customer_Form(Customer_Form customer)
        {
            InitializeComponent();
            insertData = new Insert_Data_Class();
            updateData = new Update_Data_Class();
            frm = customer;
        }

       
        private void Add_Customer_Form_Load(object sender, EventArgs e)
        {
            Customer_Text_Status();
            /*
            string[] listtypecustomer = new string[] { "Registered Customer", "Guest Customer" };

            for (int i = 0; i < 2; i++)
            {
                cbTypeCustomer.Items.Add(listtypecustomer[i].ToString());
            }
            */
        }

        private void cbTypeCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTypeCustomer = cbTypeCustomer.SelectedItem.ToString();

            switch (selectedTypeCustomer)
            {
                case "Registered Customer":
                    
                    Clear();
                    txt_CustomerName.Enabled = true;
                    txt_ContactNumber.Enabled = true;
                    txt_EmailAddress.Enabled = true;
                    txt_Address.Enabled = true;
                    
                    lblCustomerName.Enabled = true;
                    lblContactNumber.Enabled = true;
                    lblEmailAddress.Enabled = true;
                    lblAddress.Enabled = true;
                    

                    break;

                case "Guest Customer":
                    
                    Clear();
                    txt_CustomerName.Enabled = true;
                    txt_ContactNumber.Enabled = false;
                    txt_EmailAddress.Enabled = false;
                    txt_Address.Enabled = false;

                    lblCustomerName.Enabled = true;
                    lblContactNumber.Enabled = false;
                    lblEmailAddress.Enabled = false;
                    lblAddress.Enabled = false;

                    break;

                default:


                    break;

            }
        }

        public void Customer_Text_Status()
        {
            txt_CustomerName.Enabled = false;
            txt_ContactNumber.Enabled = false;
            txt_EmailAddress.Enabled = false;
            txt_Address.Enabled = false;

            lblCustomerName.Enabled = false;
            lblContactNumber.Enabled = false;
            lblEmailAddress.Enabled = false;
            lblAddress.Enabled = false;
        }

        public void Clear()
        {
            if(btnSave.Text == "Update" && cbTypeCustomer.Text == "Guest Customer")
            {
                //txt_ContactNumber.PasswordChar = '*';
                //txt_EmailAddress.PasswordChar = '*';
                //txt_Address.PasswordChar = '*';
            }
            else
            {
                
                txt_ContactNumber.Clear();
                txt_EmailAddress.Clear();
                txt_Address.Clear();
                txt_CustomerName.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (cbTypeCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer type", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string customerType = cbTypeCustomer.SelectedItem.ToString();

                if (customerType == "Registered Customer")
                {
                    if (String.IsNullOrEmpty(txt_CustomerName.Text))
                    {
                        MessageBox.Show("Please enter the customer.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (String.IsNullOrEmpty(txt_ContactNumber.Text))
                    {
                        MessageBox.Show("Please enter the contact number.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (String.IsNullOrEmpty(txt_EmailAddress.Text))
                    {
                        MessageBox.Show("Please enter the email address.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (String.IsNullOrEmpty(txt_Address.Text))
                    {
                        MessageBox.Show("Please enter the address.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if(btnSave.Text == "Update")
                        {
                            //updateData.Update_RegisteredCustomer();
                        }
                        else
                        {
                            byte customerTypes = (byte)cbTypeCustomer.SelectedIndex;
                            bool check = insertData.Set_CustomerInformation(customerTypes, txt_CustomerName.Text, txt_ContactNumber.Text, txt_EmailAddress.Text, txt_Address.Text);

                            if (check)
                            {
                                MessageBox.Show("Registered customer information saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frm.DisplayRegisterAndGuestCustomer();
                                this.Dispose();

                            }
                            else
                            {
                                MessageBox.Show("Failed to save customer information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                  
                    }

                }
                else if (customerType == "Guest Customer")
                {
                    if (String.IsNullOrEmpty(txt_CustomerName.Text))
                    {
                        MessageBox.Show("Please enter the customer.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (btnSave.Text == "Update")
                        {
                            //updateData.Update_GuestsCustomer();
                        }
                        else
                        {
                            byte customerTypes = (byte)cbTypeCustomer.SelectedIndex;
                            bool check = insertData.Set_CustomerInformation(customerTypes, txt_CustomerName.Text, txt_ContactNumber.Text, txt_EmailAddress.Text, txt_Address.Text);

                            if (check)
                            {
                                MessageBox.Show("Guest customer information saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frm.DisplayRegisterAndGuestCustomer();
                                this.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("Failed to save customer information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                      
                    }
                }
            }
        } 



        public void SetCustomerInformation()
        {
            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                connect.Open();

                byte customerType = (byte)cbTypeCustomer.SelectedIndex;

                string sql = "INSERT INTO Customers (Customer_Name, Contact_Number, Email_Address, Address, Customer_Type, Date_Added, Archive) " +
                               "VALUES (@CustomerName, @ContactNumber, @EmailAddress, @Address, @CustomerType, @DateAdded, @Archive)";

                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@CustomerName", txt_CustomerName.Text);
                    cmd.Parameters.AddWithValue("@ContactNumber", txt_ContactNumber.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", txt_EmailAddress.Text);
                    cmd.Parameters.AddWithValue("@Address", txt_Address.Text);
                    cmd.Parameters.AddWithValue("@CustomerType", customerType);
                    cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Archive", 0);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Customer information saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        //frm.Show_Register_Customer();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save customer information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
