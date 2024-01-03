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
//using Lizaso_Laundry_Hub.Notify_Module;

namespace Lizaso_Laundry_Hub
{
    public partial class Add_Unit_Form : KryptonForm
    {
        private Settings_Module.LaundryUnit_Configuration_Form frm;

        private Services_Form frm2;
        private Account_Class account;
        private Insert_Data_Class insertData;
        private Update_Data_Class updateData;

        public int getunitID, getStatus;
        protected string activityType = "Add";
        protected string description = "Added a new laundry unit to the system.";

        public Add_Unit_Form(Settings_Module.LaundryUnit_Configuration_Form unit)
        {
            InitializeComponent();
            account = new Account_Class();
            insertData = new Insert_Data_Class();
            updateData = new Update_Data_Class();
            //sideNotificationForm = new Side_Notification_Form();
            frm = unit;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Notify_Module.Side_Notification_Form notify = new Notify_Module.Side_Notification_Form();

            if (cbStatus.SelectedIndex == -1)
            {
                notify.colorStatus = 1;
                notify.messageSent = "Please select a unit status";
                notify.Show();
            }
            else
            {
                if (btnSave.Text == "Update")
                {
                    int unitStatusIndex = cbStatus.SelectedIndex;

                    if (unitStatusIndex >= 0)
                    {
                        // Assuming unitStatus is an integer representing the selected index
                        int unitStatus = unitStatusIndex;

                        bool updateSuccessful = updateData.Update_Unit(getunitID, txt_UnitName.Text, unitStatus);

                        if (updateSuccessful)
                        {
                            notify.colorStatus = 0;
                            notify.messageSent = "Unit updated successfully. ";
                            notify.Show();
                            this.Close();
                            frm.DisplayUnit();
                           
                        }
                        else
                        {
                            // Update failed or was canceled
                            MessageBox.Show("Failed to update unit. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           
                        }
                    }
                    else
                    {
                        // Handle the case where no item is selected
                        MessageBox.Show("No unit status selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    int _status = cbStatus.SelectedIndex;

                    insertData.Set_Unit(_status);
                    insertData.Set_ActivityLog(account.User_ID, account.User_Name, activityType, description);
                    this.Dispose();
                    frm.DisplayUnit();
                }
                
                //frm2.Load_Unit();
            }
        }
        /*
        // Method to show success message using Side_Notification_Form
        private void ShowSuccessNotification(string message)
        {
            // Create an instance of Side_Notification_Form if not created yet
            if (sideNotificationForm == null)
            {
                sideNotificationForm = new Side_Notification_Form();
            }

            // Set the label text to the success message
            sideNotificationForm.label_show_string.Text = message;

            // Show the Side_Notification_Form
            sideNotificationForm.Show();
        }

        // Method to show error message using Side_Notification_Form
        private void ShowErrorNotification(string message)
        {
            // Create an instance of Side_Notification_Form if not created yet
            if (sideNotificationForm == null)
            {
                sideNotificationForm = new Side_Notification_Form();
            }

            // Set the label text to the error message
            sideNotificationForm.label_show_string.Text = message;

            // Set the label color to indicate an error (optional)
            sideNotificationForm.label_show_string.ForeColor = Color.Red;

            // Show the Side_Notification_Form
            sideNotificationForm.Show();
        }

        */
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Add_Unit_Form_Load(object sender, EventArgs e)
        {
           
            /*
            string[] ListofStatus = new string[] { "Available",
                                                   "Occupied",
                                                   "Reserved",
                                                   "Not Available" };
            */

            string[] ListofStatus = new string[] { "Available",
                                                   "Occupied",
                                                   "Not Available" };

            for (int i = 0; i < 3; i++)
            {
                cbStatus.Items.Add(ListofStatus[i].ToString());
            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = cbStatus.SelectedItem.ToString();

            switch (selectedStatus)
            {
                case "Available":
                    image_unit.Image = Properties.Resources.Available;

                    break;
                case "Occupied":
                    image_unit.Image = Properties.Resources.Occupied;
                    break;

                case "Reserved":
                    image_unit.Image = Properties.Resources.Washing_Reserved;
                    break;
                case "Not Available":
                    MessageBox.Show("No image yet");
                    break;
                default:


                    break;
            }
        }
    }
}
