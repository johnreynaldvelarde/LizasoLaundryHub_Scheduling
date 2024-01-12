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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lizaso_Laundry_Hub
{
    public partial class Select_Unit_Form : KryptonForm
    {
        private Services_Form frm;
        private Insert_Data_Class insertData;
        private Activity_Log_Class activityLogger;
        private DB_Connection database = new DB_Connection();

       
        public int unitID;
        public ucUnit_Control ucUnit { get; set; }

        public Select_Unit_Form(Services_Form services)
        {
            InitializeComponent();
            insertData = new Insert_Data_Class();
            activityLogger = new Activity_Log_Class();
            frm = services;
        }

        private async void btnProceed_Click(object sender, EventArgs e)
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
            else
            {
                try
                {
                    string _services = txt_Service.Text;
                    decimal weight = ParseWeight(cbWeight.SelectedItem.ToString());

                    string startTimeText = lblStartTime.Text;
                    string endTimeText = lblEndTime.Text;

                    DateTime currentDate = DateTime.Now.Date;

                    DateTime startTime = DateTime.Parse($"{currentDate.ToShortDateString()} {startTimeText}");
                    DateTime endTime = DateTime.Parse($"{currentDate.ToShortDateString()} {endTimeText}");

                    string sendStartTime = startTime.ToString("MM/dd/yyyy h:mm:ss tt");
                    string sendEndTime = endTime.ToString("MM/dd/yyyy h:mm:ss tt");


                    if (cbSelectCustomer.SelectedItem is ComboBoxItem selectedCustomerItem)
                    {
                        int customer_ID = selectedCustomerItem.CustomerID;
                        insertData.Set_LaundryBookings(unitID, customer_ID, _services, weight, sendStartTime, sendEndTime);
                    }
                    UserActivityLog();
                    this.Dispose();
                    frm.Load_Unit();
                    await frm.DisplayInProgress();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void UserActivityLog()
        {
            string activityType = "Selected";
            string UnitDescription = $"{txt_UnitName.Text} has been selected for a laundry booking as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, UnitDescription);
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

        public void DisplayAvailableSelectedTime()
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime;
            lblStartTime.Text = startTime.ToLongTimeString();

            if (rd2hourDefaultTime.Checked == true)
            {
                endTime = startTime.AddHours(2);
                lblEndTime.Text = endTime.ToLongTimeString();
            }
            else if (rd3hours.Checked == true)
            {
                endTime = startTime.AddHours(3);
                lblEndTime.Text = endTime.ToLongTimeString();
            }
            else if (rd1minutes.Checked == true)
            {
                endTime = startTime.AddMinutes(1);
                lblEndTime.Text = endTime.ToLongTimeString();
            }
            else if (rd30minutes.Checked == true)
            {
                endTime = startTime.AddMinutes(30);
                lblEndTime.Text = endTime.ToLongTimeString();
            }
            else
            {
                MessageBox.Show("No schedule selection");
            }

        }

        // method show the second is running
        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayAvailableSelectedTime();
        }

        private void Select_Unit_Form_Load(object sender, EventArgs e)
        {
            rd2hourDefaultTime.Checked = true;
            DisplayAvailableSelectedTime();
           
            cbSelectCustomer.Enabled = false;
           
            string[] listcustomer = new string[] { "Registered Customer", "Guest Customer" };

            string[] listweight = new string[] { "1 Kilogram (Minimum)", 
                                                 "2 Kilogram",
                                                 "3 Kilogram",
                                                 "4 Kilogram",
                                                 "5 Kilogram",
                                                 "6 Kilogram",
                                                 "7 Kilogram",
                                                 "8 Kilogram (Maximum)",
            };

            for (int i = 0; i < 2; i++)
            {
                cbCustomer_Type.Items.Add(listcustomer[i].ToString());
            }
            for (int j = 0; j < 8; j++)
            {
                cbWeight.Items.Add(listweight[j].ToString());
            }
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

                        ComboBoxItem item = new ComboBoxItem(customerID, customerName);

                        cbSelectCustomer.Items.Add(item);
                    }

                    reader.Close();
                    connect.Close();
                }
            }
        }

        public void Clear()
        {
            cbWeight.Items.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ucUnit != null)
            {
                ucUnit.ClearSelection();
            }
            this.Dispose();
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
