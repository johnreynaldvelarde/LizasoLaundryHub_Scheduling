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
    public partial class Customer_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private int getRegisterCustomerID, getGuestCustomerID, getArchiveID;
        private string getRegisterCustomerName, getGuestCustomerName, getRegisterEmailAddress, getRegisterPhoneNumber, getRegisterAddress;

        public Customer_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
        }

        public void DisplayRegisterAndGuestCustomer()
        {
            if (tab_Customer.SelectedTab == tabPage1)
            {
                getData.Get_RegisteredCustomer(grid_register_customer);

            }
            else if (tab_Customer.SelectedTab == tabPage2)
            {
                getData.Get_GuestsCustomer(grid_guest_customer);

            }
            else if (tab_Customer.SelectedTab == tabPage3)
            {
                getData.Get_CustomerDeleted(grid_customer_archive);
            }
            else
            {

            }
        }

        private void Customer_Form_Load(object sender, EventArgs e)
        {
            DisplayRegisterAndGuestCustomer();
        }

        private void tab_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayRegisterAndGuestCustomer();
        }

        private void btn_AddNewCustomer_Click(object sender, EventArgs e)
        {
            Add_Customer_Form frm = new Add_Customer_Form(this);
            frm.ShowDialog();
        }

        // register customer
        private void grid_register_customer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_customer = grid_register_customer.Columns[e.ColumnIndex].Name;

            if (column_customer == "Edit")
            {
                Add_Customer_Form register = new Add_Customer_Form(this);
                register.lbtitleCustomer.Text = "Update customer information";
                register.btnSave.Text = "Update";
                register.cbTypeCustomer.SelectedIndex = 0;
                register.registerCustomeID = getRegisterCustomerID;
                register.txt_CustomerName.Text = getRegisterCustomerName;
                register.txt_ContactNumber.Text = getRegisterPhoneNumber;
                register.txt_EmailAddress.Text = getRegisterEmailAddress;
                register.txt_Address.Text = getRegisterAddress;

                register.ShowDialog();
            }
            else if (column_customer == "Delete")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this registered customer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_RegisterCustomerToDeleted(getRegisterCustomerID);
                    DisplayRegisterAndGuestCustomer();
                }
            }
        }
        // register customer
        private void grid_register_customer_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_register_customer.CurrentRow != null)
            {
                int i = grid_register_customer.CurrentRow.Index;

                if (int.TryParse(grid_register_customer[1, i].Value.ToString(), out int selectCustomerID))
                {
                    getRegisterCustomerID = selectCustomerID;
                    getRegisterCustomerName = grid_register_customer[2, i].Value.ToString();
                    getRegisterEmailAddress = grid_register_customer[3, i].Value.ToString();
                    getRegisterPhoneNumber = grid_register_customer[4, i].Value.ToString();
                    getRegisterAddress = grid_register_customer[5, i].Value.ToString();
                }
            }
        }


        // guest customer
        private void grid_guest_customer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_guestcustomer = grid_guest_customer.Columns[e.ColumnIndex].Name;

            if (column_guestcustomer == "Edit2")
            {
                Add_Customer_Form guest = new Add_Customer_Form(this);
                guest.lbtitleCustomer.Text = "Update customer information";
                guest.btnSave.Text = "Update";
                guest.cbTypeCustomer.SelectedIndex = 1;
                guest.guestCustomerID = getGuestCustomerID;
                guest.txt_CustomerName.Text = getGuestCustomerName;
                guest.ShowDialog();
            }
            else if (column_guestcustomer == "Delete2")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this guest customer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_GuestCustomerToDeleted(getGuestCustomerID);
                    DisplayRegisterAndGuestCustomer();
                }
            }
        }

        // guest customer
        private void grid_guest_customer_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_guest_customer.CurrentRow != null)
            {
                int i = grid_guest_customer.CurrentRow.Index;

                if (int.TryParse(grid_guest_customer[1, i].Value.ToString(), out int selectGuestCustomerID))
                {
                    getGuestCustomerID = selectGuestCustomerID;
                    getGuestCustomerName = grid_guest_customer[2, i].Value.ToString();
                  
                }
            }
        }

        // custoemr archive
        private void grid_customer_archive_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_archive = grid_customer_archive.Columns[e.ColumnIndex].Name;

            if (column_archive == "Recycle")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to recycle this customer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_CustomerRecycleArchive(getArchiveID);
                    DisplayRegisterAndGuestCustomer();
                }
            }

        }

        // customer archive
        private void grid_customer_archive_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_customer_archive.CurrentRow != null)
            {
                int archive = grid_customer_archive.CurrentRow.Index;

                if (int.TryParse(grid_customer_archive[1, archive].Value.ToString(), out int selectArchiveID))
                {
                    getArchiveID = selectArchiveID;

                }
            }
        }

    }
}
