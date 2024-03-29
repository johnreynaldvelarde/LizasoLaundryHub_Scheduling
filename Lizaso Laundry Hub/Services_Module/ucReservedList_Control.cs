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
    public partial class ucReservedList_Control : UserControl
    {
        public In_Reserved_Class reve { get; private set; }
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;


        private int bookingID, unitID;

        public ucReservedList_Control(In_Reserved_Class reserved)
        {
            InitializeComponent();
            updateData = new Update_Data_Class();
            activityLogger = new Activity_Log_Class();
            reve = reserved;
            ShowReserved();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel this reservation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                 updateData.Update_BookingReservationToCanceled(bookingID, unitID);
                 UserActivityLog(reve.Customer_Name);
            }
        }

        public void UserActivityLog(string customerName)
        {
            string activityType = "Canceled";
            string CancelDescription = $"{customerName}'s reserved laundry booking has been forcefully marked as canceled as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, CancelDescription);
        }

        public void ShowReserved()
        {
            bookingID = reve.BookingID;
            unitID = reve.UnitID;
            int customerID = reve.CustomerID;

            Console.WriteLine(unitID + "  " + bookingID);

            lblUnitName.Text = reve.Unit_Name;
            lblCustomerName.Text = reve.Customer_Name;
            lblServiceType.Text = reve.ServiceType;
            lblR_StartTime.Text = reve.ReservedStartTime;
            lblR_EndTime.Text = reve.ReservedEndTime;
            lblStatus.Text = reve.Status;
        }



    }
}
