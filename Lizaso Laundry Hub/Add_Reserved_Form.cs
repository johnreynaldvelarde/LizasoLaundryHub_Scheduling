using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using static Lizaso_Laundry_Hub.Get_Data_Class;
using static Lizaso_Laundry_Hub.Select_Unit_Form;

namespace Lizaso_Laundry_Hub
{
    public partial class Add_Reserved_Form : KryptonForm
    {
        private DB_Connection database = new DB_Connection();
        
        private Insert_Data_Class insertData;
        private Get_Data_Class getData;
        Services_Form frm;

        public Add_Reserved_Form(Services_Form services)
        {
            InitializeComponent();
            insertData = new Insert_Data_Class();
            getData = new Get_Data_Class();
            frm = services;
        }

        public bool DisplayClosestUnitTime()
        {
            bool unitFound = getData.Get_ClosestUnit(lblUnitID, lblUnitName, lblReservedStartTime);

            if (!unitFound)
            {
                MessageBox.Show("No available units found in Laundry_Bookings.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {

            }
            return unitFound;
        }

        public void DisplayAvailableSelectedTime()
        {
            if (DateTime.TryParse(lblReservedStartTime.Text, out DateTime startTime))
            {
                DateTime endTime;

                if (rd2hourDefaultTime.Checked == true)
                {
                    endTime = startTime.AddHours(2);
                    lblReservedEndTime.Text = endTime.ToLongTimeString();
                }
                else if (rd3hours.Checked == true)
                {
                    endTime = startTime.AddHours(3);
                    lblReservedEndTime.Text = endTime.ToLongTimeString();
                }
                else if (rd1minutes.Checked == true)
                {
                    endTime = startTime.AddMinutes(1);
                    lblReservedEndTime.Text = endTime.ToLongTimeString();
                }
                else if (rd30minutes.Checked == true)
                {
                    endTime = startTime.AddMinutes(30);
                    lblReservedEndTime.Text = endTime.ToLongTimeString();
                }
                else
                {
                    MessageBox.Show("No schedule selection");
                }
            }
            else
            {
                this.Dispose();
            }
        }

        private void Add_Reserved_Form_Load(object sender, EventArgs e)
        {
            rd2hourDefaultTime.Checked = true;
            if (getData.IsAnyUnitAvailable())
            {
                this.Dispose();
                MessageBox.Show("There are available units left. Further actions cannot proceed.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DisplayClosestUnitTime();
                DisplayAvailableSelectedTime();
            }

            cbSelectCustomer.Enabled = false;
            string[] listcustomer = new string[] { "Registered Customer", "Guest Customer" };

            string[] listservicetype = new string[] { "Wash", "Wash/Dry", "Wash/Dry/Fold" };

            string[] listweight = new string[] { "1 Kilogram (Minimum)",
                                                 "2 Kilogram",
                                                 "3 Kilogram",
                                                 "4 Kilogram",
                                                 "5 Kilogram",
                                                 "6 Kilogram",
                                                 "7 Kilogram",
                                                 "8 Kilogram (Maximum)",
            };

            for (int i = 0; i < 8; i++)
            {
                cbWeight.Items.Add(listweight[i].ToString());
            }

            for (int j = 0; j < 3; j++)
            {
                cbService_Type.Items.Add(listservicetype[j].ToString());
            }

            for (int n = 0; n < 2; n++)
            {
                cbCustomer_Type.Items.Add(listcustomer[n].ToString());
            }
        }
        
        private decimal ParseWeight(string weightString)
        {
            string numericPart = new string(weightString.TakeWhile(char.IsDigit).ToArray());

            if (decimal.TryParse(numericPart, out decimal weight))
            {
                return weight;
            }
            return 0;
        }

        private void cbCustomer_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCustomerType = cbCustomer_Type.SelectedItem.ToString();

            switch (selectedCustomerType)
            {
                case "Registered Customer":
                case "Guest Customer":
                    cbSelectCustomer.Enabled = true;
                    break;

                default:
                    cbSelectCustomer.Enabled = false;
                    cbSelectCustomer.Items.Clear();
                    return;
            }

            cbSelectCustomer.Items.Clear();

            using (SqlConnection connect = new SqlConnection(database.MyConnection()))
            {
                string query = "SELECT Customer_ID, Customer_Name FROM Customers WHERE Customer_Type = @CustomerType";
                using (SqlCommand command = new SqlCommand(query, connect))
                {
                    command.Parameters.AddWithValue("@CustomerType", selectedCustomerType == "Registered Customer" ? 0 : 1);
                    connect.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int customerID = reader.GetInt32(0);
                        string customerName = reader["Customer_Name"].ToString();

                        // ComboBoxItem to store both Customer_ID and Customer_Name
                        ComboBoxItem item = new ComboBoxItem(customerID, customerName);

                        cbSelectCustomer.Items.Add(item);
                    }

                    reader.Close();
                    connect.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnReserved_Click(object sender, EventArgs e)
        {
            if (cbCustomer_Type.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer type");
            }
            else if (cbSelectCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer");
            }
            else if (cbWeight.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a weight(kg)");
            }
            else if (cbService_Type.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a services");
            }
            else
            {
                string _services = cbService_Type.Text;
                decimal weight = ParseWeight(cbWeight.SelectedItem.ToString());

                DateTime startTime = DateTime.ParseExact(lblReservedStartTime.Text, "h:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime endTime = DateTime.ParseExact(lblReservedEndTime.Text, "h:mm:ss tt", CultureInfo.InvariantCulture);

                // Format the DateTime objects into the desired format
                string formattedStartTime = startTime.ToString("MM/dd/yyyy h:mm:ss tt");
                string formattedEndTime = endTime.ToString("MM/dd/yyyy h:mm:ss tt");

                if (cbSelectCustomer.SelectedItem is ComboBoxItem selectedCustomerItem)
                {
                    int customer_ID = selectedCustomerItem.CustomerID;
                    int r_unitID = int.Parse(lblUnitID.Text);

                    bool success = insertData.Set_ReservedUnit(r_unitID, customer_ID, _services, weight, formattedStartTime, formattedEndTime);
                    
                    if (success)
                    {
                        // The reservation was successful, you can add additional logic here
                        Console.WriteLine("Reservation successful");
                        this.Dispose();
                        frm.Load_Unit();
                        frm.Load_Reserved();
                    }
                    else
                    {
                        // There was an issue with the reservation, handle accordingly
                        Console.WriteLine("Reservation failed");
                    }
                }

            }
        }

        private void rd2hourDefaultTime_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAvailableSelectedTime();
        }

        private void rd3hours_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAvailableSelectedTime();
        }

        private void rd1minutes_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAvailableSelectedTime();
        }

        private void rd30minutes_CheckedChanged(object sender, EventArgs e)
        {
            DisplayAvailableSelectedTime();
        }

        // Helper class to store Customer_ID and Customer_Name in ComboBox
        public class ComboBoxItem
        {
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }

            public ComboBoxItem(int id, string name)
            {
                CustomerID = id;
                CustomerName = name;
            }

            public override string ToString()
            {
                return CustomerName;
            }
        }
    }
}
