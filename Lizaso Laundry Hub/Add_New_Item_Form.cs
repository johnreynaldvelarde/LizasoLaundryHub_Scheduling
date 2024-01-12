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
    public partial class Add_New_Item_Form : KryptonForm
    {
        private Inventory_Form frm;
        private Insert_Data_Class insertData;
        private Update_Data_Class updateData;
        private Activity_Log_Class activityLogger;
        public int setItemID;

        public Add_New_Item_Form(Inventory_Form inventory)
        {
            InitializeComponent();
            insertData = new Insert_Data_Class();
            updateData = new Update_Data_Class();
            activityLogger = new Activity_Log_Class();
            frm = inventory;
        }
        public void Clear()
        {
            if(btnSave.Text == "Update")
            {
                txt_ItemName.Clear();
                txt_Price.Clear();
                cb_Category.SelectedIndex = -1;
                txt_ItemName.Focus();
            }
            else
            {
                txt_ItemName.Clear();
                txt_Price.Clear();
                txt_Quantity.Clear();
                cb_Category.SelectedIndex = -1;
                txt_ItemName.Focus();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_ItemName.Text))
            {
                MessageBox.Show("Please enter the Item Name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_Quantity.Text))
            {
                MessageBox.Show("Please enter the Quantity.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(txt_Price.Text))
            {
                MessageBox.Show("Please enter the Price.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cb_Category.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(btnSave.Text == "Update")
                {
                    string itemCategory = cb_Category.Text;
                    decimal itemPrice = decimal.Parse(txt_Price.Text);
                    updateData.Update_InventoryItem(setItemID, txt_ItemName.Text, itemCategory, itemPrice);
                    UserActivityLogUpdate(txt_ItemName.Text);
                    frm.DisplayInventory();
                    this.Dispose();
                }
                else
                {
                    try
                    {
                        string _itemName = txt_ItemName.Text;
                        string _categoryItem = cb_Category.Text;
                        decimal _itemPrice = decimal.Parse(txt_Price.Text);
                        int _qyt = int.Parse(txt_Quantity.Text);

                        insertData.Set_ItemDetails(_itemName, _categoryItem, _itemPrice, _qyt);
                        UserActivityLogSave(_itemName);
                        this.Dispose();
                        frm.DisplayInventory();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        public bool UserActivityLogSave(string itemName)
        {
            string activityType = "New Created";
            string createdDescription = $"{itemName} has been successfully added to the system as a new item as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, createdDescription);

            return true;
        }
        public bool UserActivityLogUpdate(string itemName)
        {
            string activityType = "Updated";
            string updateDescription = $"{itemName}'s information has been updated as of {DateTime.Now}.";
            activityLogger.LogActivity(activityType, updateDescription);

            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
