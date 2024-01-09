using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;
using Lizaso_Laundry_Hub.Receipt_Module;

namespace Lizaso_Laundry_Hub
{
    public partial class Payment_Details_Form : KryptonForm
    {
        //private Services_Form frm;
        private Additional_Payment_Form additionalPaymentForm;
        private Account_Class account;
        private Insert_Data_Class insertData;
        private Get_Data_Class getData;
        private Payments_Form frm;
        private Receipt_Form receipt;

        public int UnitID;
        public int BookingID;
        public int CustomerID;

        public int userId;
        public string userName;

        public double TotalServicesPrice;
        private double TotalAmount;

        private int TransactionID;

        public Payment_Details_Form(Payments_Form payments)
        {
            InitializeComponent();
            //frm = services;
            frm = payments;
            account = new Account_Class();
            insertData = new Insert_Data_Class();
            getData = new Get_Data_Class();
            payments = new Payments_Form();
            receipt = new Receipt_Form();
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnPrintBills_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNumberLoad.Text) || txtNumberLoad.Text == "0")
            {
                MessageBox.Show("The number of load is empty or zero.");
            }
            else if (!rbCash.Checked && !rbCashDelivery.Checked && !rbOnlinePayment.Checked)
            {
                MessageBox.Show("Please select a payment method.");
            }
            else
            {
                string _customerName = txt_CustomerName.Text;
                string _serviceType = txt_ServiceType.Text;
                string paymentMethod = GetSelectedPaymentMethod();
                string totalPaymentText = lblTotalPayment.Text;
                totalPaymentText = totalPaymentText.Replace("PHP", "").Trim();
                decimal amount = decimal.Parse(totalPaymentText);

                if (btnViewDetails.Enabled == true)
                {
                    double totalAmount = additionalPaymentForm.TotalAmount;

                    List<Item_Data> additionalItems = additionalPaymentForm.GetAdditionalItems();

                    TransactionID = insertData.Set_PendingDetails(UnitID, BookingID, account.User_ID, amount, paymentMethod);

                    // check if SetPaymentDetails was successful before proceeding
                    if (TransactionID != -1)
                    {
                        // call the SetAdditionalPayment with the obtained Transaction_ID
                        bool success = insertData.Set_AdditionalPayment(TransactionID, additionalItems, totalAmount);

                        if (success)
                        {

                            MessageBox.Show("Transaction with additional payment added successfully");

                            if (ckFreeShipping.Checked)
                            {
                                insertData.Set_Delivery(TransactionID);
                            }

                            this.Dispose();
                            frm.DisplayInPendingList();

                            //receipt.Get_AdditonalPayment(account.User_Name, _customerName, _serviceType, additionalItems);
                            //receipt.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Error saving additional payment information");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error setting payment details");
                    }
                   
                }
                else
                {
                    //int transactionID = insertData.Set_PendingDetails(UnitID, BookingID, account.User_ID, amount, paymentMethod);
                    TransactionID = insertData.Set_PendingDetails(UnitID, BookingID, account.User_ID, amount, paymentMethod);

                    if (TransactionID != -1)
                    {
                        MessageBox.Show("Transaction payment added successfully");

                        if (ckFreeShipping.Checked)
                        {
                            insertData.Set_Delivery(TransactionID);
                        }

                        this.Dispose();
                        frm.DisplayInPendingList();

                        //receipt.GetPaymentDetails(account.User_Name, _customerName, _serviceType);
                        receipt.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error setting payment details");
                    }
                  
                }
            }
        }
      
        private string GetSelectedPaymentMethod()
        {
            //  return the selected payment method
            if (rbCash.Checked)
            {
                return "Cash";
            }
            else if (rbCashDelivery.Checked)
            {
                return "Cash on Delivery";
            }
            else if (rbOnlinePayment.Checked)
            {
                return "Online Payment";
            }
            else
            {
                return string.Empty;
            }
        }

        public void CalculateTotalPayment(double additionalPayment, double additionalAmount)
        {
            if (string.IsNullOrEmpty(txtNumberLoad.Text))
            {
                lblTotalPayment.Text = $"PHP {lblAdditionalPayment.Text}";
            }
            else
            {
                // Get the current day of the week
                DayOfWeek currentDay = DateTime.Now.DayOfWeek;

                // Parse the number of loads
                if (int.TryParse(txtNumberLoad.Text, out int numberOfLoads))
                {
                    // Get the selected service type
                    string serviceType = txt_ServiceType.Text.Trim().ToLower();

                    // Set default prices
                    int washPrice = 0;
                    int dryPrice = 0;
                    int foldPrice = 30; // Default fold price

                    // Check if it's a promo day (Monday, Tuesday, or Wednesday)
                    if (currentDay == DayOfWeek.Monday || currentDay == DayOfWeek.Tuesday || currentDay == DayOfWeek.Wednesday)
                    {
                        washPrice = 55;
                        dryPrice = 55;
                    }
                    else // It's a regular day
                    {
                        washPrice = 65;
                        dryPrice = 60;
                    }

                    // Calculate the total payment based on the service type and number of loads
                    double totalPayment = 0;

                    switch (serviceType)
                    {
                        case "wash":
                            totalPayment = washPrice * numberOfLoads;
                            break;
                        case "wash/dry":
                            totalPayment = (washPrice + dryPrice) * numberOfLoads;
                            break;
                        case "wash/dry/fold":
                            totalPayment = (washPrice + dryPrice) * numberOfLoads + foldPrice;
                            break;
                        default:
                            MessageBox.Show("Invalid service type. Please enter 'Wash,' 'Wash/Dry,' or 'Wash/Dry/Fold'.");
                            return;
                    }

                    TotalServicesPrice = totalPayment;
                    // Incorporate the additionalPayment and additionalAmount into the total payment
                    totalPayment += (int)additionalPayment + (int)additionalAmount; // If TotalAmount is int
                    
                    // Display the total payment in lblTotalPayment
                    lblTotalPayment.Text = $"PHP {totalPayment}";
                }
                else
                {
                    // Clear lblTotalPayment and set it back to 0 if the entered value is not a valid number
                    lblTotalPayment.Text = $"PHP {0}";
                }
            }
        }

        private void txtNumberLoad_TextChanged(object sender, EventArgs e)
        {
            string getAdditionalAmount = lblAdditionalPayment.Text;

            if (string.IsNullOrEmpty(txtNumberLoad.Text))
            {
               
                lblTotalPayment.Text = $"PHP {getAdditionalAmount}";
                lblServicePrice.Text = "0";
            }
            else
            {
                if (int.TryParse(txtNumberLoad.Text, out int numberOfLoads))
                {
                    // Call the CalculateTotalPayment method with the current TotalAmount and 0 as additional amount
                    CalculateTotalPayment(TotalAmount, double.Parse(getAdditionalAmount));
                    lblServicePrice.Text = TotalServicesPrice.ToString();
                }
                else
                {
                    lblTotalPayment.Text = $"PHP {0}";
                }
            }
        }

        private void txtNumberLoad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '\u007F')
            {
                e.Handled = true;
            }
            else if (e.KeyChar == '\b')
            {

            }
            else
            {
                string currentText = txtNumberLoad.Text;

                if (currentText == "0")
                {
                    e.Handled = true;
                }
                else
                {
                    if (currentText.Length >= 2 || (currentText.Length == 1 && currentText[0] != '0'))
                    {
                        int value;
                        bool isNumeric = int.TryParse(currentText + e.KeyChar, out value);
                        if (!isNumeric || value > 100)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void btnAdditonalPayment_Click(object sender, EventArgs e)
        {
            if (additionalPaymentForm == null || additionalPaymentForm.IsDisposed)
            {
                additionalPaymentForm = new Additional_Payment_Form(this);
            }
            additionalPaymentForm.Show(); 
            additionalPaymentForm.Focus();
        }
      
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (additionalPaymentForm == null || additionalPaymentForm.IsDisposed)
            {
                additionalPaymentForm = new Additional_Payment_Form(this);
            }
            additionalPaymentForm.Show(); 
            additionalPaymentForm.Focus();
        }

        public void UpdateAdditionalAmount(double totalAmount)
        {
            lblAdditionalPayment.Text = totalAmount.ToString();
        }

        public void DisableAdditonalPaymentButton()
        {
            btnAdditonalPayment.Enabled = false;
        }

        public void EnableViewDetails()
        {
            btnViewDetails.Enabled = true;
        }

        public void Get_InfoResereved()
        {

        }

        private void Payment_Details_Form_Load(object sender, EventArgs e)
        {
            getData.Get_CustomerType(CustomerID, ckFreeShipping);
        }
    }
}
