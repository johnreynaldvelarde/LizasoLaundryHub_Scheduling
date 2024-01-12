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
        private Customer_Form frm;

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
            if(btnSave.Text == "Update")
            {

            }
            else
            {
                Customer_Text_Status();
            }
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
                string customerName = txt_CustomerName.Text;
                string contactNumber = txt_ContactNumber.Text;
                string emailAddress = txt_EmailAddress.Text;
                string address = txt_Address.Text;

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
                        if (btnSave.Text == "Update")
                        {
                            updateData.Update_RegisterCustomer(registerCustomeID, customerName, contactNumber, emailAddress, address);
                            frm.DisplayRegisterAndGuestCustomer();
                            this.Dispose();
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
                            updateData.Update_GuestCustomer(guestCustomerID, customerName);
                            frm.DisplayRegisterAndGuestCustomer();
                            this.Dispose();
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
    }
}
