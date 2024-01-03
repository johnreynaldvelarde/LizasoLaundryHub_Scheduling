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
        public int setItemID;

        public Add_New_Item_Form(Inventory_Form inventory)
        {
            InitializeComponent();
            insertData = new Insert_Data_Class();
            updateData = new Update_Data_Class();
            frm = inventory;
        }
        public void Clear()
        {
            txt_ItemName.Clear();
            txt_Price.Clear();
            txt_Quantity.Clear();
            cb_Category.SelectedIndex = -1;
            txt_ItemName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Add_New_Item_Form_Load(object sender, EventArgs e)
        {
            /*
            string[] ListofStatus = new string[] { "Liquid",
                                                   "Detergent",
                                                   "Donwy" };
            for (int i = 0; i < 3; i++)
            {
                cb_Category.Items.Add(ListofStatus[i].ToString());
            }
            */
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Notify_Module.Side_Notification_Form notify = new Notify_Module.Side_Notification_Form();

            if (String.IsNullOrEmpty(txt_ItemName.Text))
            {
                //MessageBox.Show("Please enter the Item Name.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                notify.colorStatus = 1;
                notify.lbl_Title.Text = "Missing Information";
                notify.messageSent = "Please enter the Item Name.";
                notify.Show();
            }
            else if (String.IsNullOrEmpty(txt_Quantity.Text))
            {
                //MessageBox.Show("Please enter the Quantity.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                notify.colorStatus = 1;
                notify.lbl_Title.Text = "Missing Information";
                notify.messageSent = "Please enter the Quantity.";
                notify.Show();
            }
            else if (String.IsNullOrEmpty(txt_Price.Text))
            {
                //MessageBox.Show("Please enter the Price.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                notify.colorStatus = 1;
                notify.lbl_Title.Text = "Missing Information";
                notify.messageSent = "Please enter the Price.";
                notify.Show();
            }
            else if (cb_Category.SelectedIndex == -1)
            {
                //MessageBox.Show("Please select a category.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                notify.colorStatus = 1;
                notify.lbl_Title.Text = "Missing Information";
                notify.messageSent = "Please select a category.";
                notify.Show();
            }
            else
            {
                if(btnSave.Text == "Update")
                {
                    string itemCategory = cb_Category.Text;
                    decimal itemPrice = decimal.Parse(txt_Price.Text);
                    updateData.Update_InventoryItem(setItemID, txt_ItemName.Text, itemCategory, itemPrice);
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
