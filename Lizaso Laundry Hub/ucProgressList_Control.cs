﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub
{
    public partial class ucProgressList_Control : UserControl
    {
        public In_Progress_Class pen { get; private set; }
        private Update_Data_Class updateData;
        private Get_Data_Class getData;

        private int bookingID, unitID;

        public ucProgressList_Control(In_Progress_Class pending)
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
            pen = pending;
            ShowPending();
        }

        public void ShowPending()
        {
            bookingID = pen.BookingID;
            unitID = pen.UnitID;
            int customerID = pen.CustomerID;

            Console.WriteLine(unitID + "  " + bookingID);

            lblUnitName.Text = pen.Unit_Name;
            lblCustomerName.Text = pen.Customer_Name;
            lblServiceType.Text = pen.ServiceType;
            lblTimeLeft.Text = pen.TimeLeft + " minutes";
            lblStatus.Text = pen.Status;
        }

        public void UpdateTimeLeft()
        {
            // Calculate the updated time left based on the current time
            DateTime endTime = getData.RetrieveEndTimeFromDatabase(pen.BookingID); // Pass the BookingID
            TimeSpan timeLeft = endTime - DateTime.Now;

            // Display the updated time left
            if (timeLeft.TotalMinutes > 1)
            {
                lblTimeLeft.Text = $"{(int)timeLeft.TotalMinutes} minutes";
            }
            else if (timeLeft.TotalSeconds > 0)
            {
                lblTimeLeft.Text = $"{(int)timeLeft.TotalSeconds} seconds";
            }
            else
            {
                //lblTimeLeft.Text = "0 minutes";
                lblTimeLeft.Text = "0 seconds";
            }

            pen.Status = timeLeft.TotalSeconds > 0 ? "In-Progress" : "Pending";
            lblStatus.Text = pen.Status;
        }

        public string TimeLeft
        {
            get { return lblTimeLeft.Text; }
        }

        public string Status
        {
            get { return lblStatus.Text; }
        }

        public int BookingID
        {
            get { return pen.BookingID; }
        }

        public int UnitID
        {
            get { return pen.UnitID; }
        }

        private void btnFinishNow_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to finish this booking?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                updateData.Update_BookingStatusToPending(bookingID, unitID);
               
            }
            /*
           Payment_Details_Form frm = new Payment_Details_Form(this.ParentForm as Services_Form);
           frm.CustomerID = pen.CustomerID;
           frm.UnitID = pen.UnitID;
           frm.BookingID = pen.BookingID;
           frm.txt_CustomerName.Text = pen.Customer_Name;
           frm.txt_ServiceType.Text = pen.ServiceType;
           frm.ShowDialog();
           */
        }

           /*
        public void UpdateTimeLeft()
        {
            // Calculate the updated time left based on the current time
            DateTime endTime = getData.RetrieveEndTimeFromDatabase(pen.BookingID); // Pass the BookingID
            TimeSpan timeLeft = endTime - DateTime.Now;

            // Display the updated time left
            lblTimeLeft.Text = timeLeft.TotalMinutes > 0 ? $"{(int)timeLeft.TotalMinutes} minutes" : "0 minutes";
            pen.Status = timeLeft.TotalMinutes > 0 ? "In-Progress" : "Pending";
            lblStatus.Text = pen.Status;
        }
        */
    }
}