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

namespace Lizaso_Laundry_Hub
{
    public partial class Add_Unit_Form : KryptonForm
    {
        private Settings_Module.LaundryUnit_Configuration_Form frm;

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
                    int unitStatusIndex = (cbStatus.SelectedIndex == 0) ? 0 : 2;

                    if (unitStatusIndex >= 0)
                    {
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
                            MessageBox.Show("Failed to update unit. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show("No unit status selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    int _status = (cbStatus.SelectedIndex == 0) ? 0 : 2;

                    insertData.Set_Unit(_status);
                    insertData.Set_ActivityLog(account.User_ID, account.User_Name, activityType, description);
                    this.Dispose();
                    frm.DisplayUnit();
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Add_Unit_Form_Load(object sender, EventArgs e)
        {

            string[] ListofStatus = new string[] { "Available",
                                                   "Not Available" };

            for (int i = 0; i < 2; i++)
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

                case "Not Available":
                    image_unit.Image = Properties.Resources.Not_Available;
                    break;
                default:
                    break;
            }
        }
    }
}
