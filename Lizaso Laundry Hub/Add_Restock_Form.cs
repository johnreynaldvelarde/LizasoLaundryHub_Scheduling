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
    public partial class Add_Restock_Form : KryptonForm
    {
        private Inventory_Form frm;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;
        public int getItemID;
        public string getitemName;

        public Add_Restock_Form(Inventory_Form inventory)
        {
            InitializeComponent();
            updateData = new Update_Data_Class();
            activityLogger = new Activity_Log_Class();
            frm = inventory;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_RestockItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_QytRestock.Text) || txt_QytRestock.Text == "0")
            {
                MessageBox.Show("Please enter a valid Quantity.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int itemQuantity = int.Parse(txt_QytRestock.Text);
                updateData.Update_ItemStock(getItemID, itemQuantity);
                UserActivityLog(getitemName);
                frm.DisplayInventory();
                this.Dispose();
            }
        }

        public void UserActivityLog(string itemName)
        {
            string activityType = "Restock";
            string restockDescription = $"{itemName} has been restocked as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, restockDescription);
        }

        private void txt_QytRestock_KeyPress(object sender, KeyPressEventArgs e)
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
                string currentText = txt_QytRestock.Text;

                if (currentText == "0")
                {
                    e.Handled = true;
                }
                else
                {
                    if (currentText.Length >= 3 || (currentText.Length == 1 && currentText[0] != '0'))
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
    }
}
