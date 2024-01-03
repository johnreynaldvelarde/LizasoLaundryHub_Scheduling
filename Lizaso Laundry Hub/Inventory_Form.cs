using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Lizaso_Laundry_Hub
{
    public partial class Inventory_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Update_Data_Class updateData;
        private int getItemID, getCategory;
        private string getItemName, getItemCode, getQuantity, getPrice;
       
        public Inventory_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            updateData = new Update_Data_Class();
        }

        public void DisplayInventory()
        {
            if (tab_Inventory.SelectedTab == tabPage1)
            {
                getData.Get_InventoryItem(grid_item_inventory);

            }
            if (tab_Inventory.SelectedTab == tabPage2)
            {
                getData.Get_InventoryDeleted(grid_inventory_archive);

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add_New_Item_Form frm = new Add_New_Item_Form(this);
            frm.ShowDialog();
        }

        private void Inventory_Form_Load(object sender, EventArgs e)
        {
            DisplayInventory();
        }

        private void tab_Inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayInventory();
        }

        private void grid_item_inventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string column_inventory = grid_item_inventory.Columns[e.ColumnIndex].Name;

            if (column_inventory == "ReStock")
            {
                Add_Restock_Form restock = new Add_Restock_Form(this);
                restock.getItemID = getItemID;
                restock.txt_RemaningStock.Text = getQuantity;
                restock.txt_QytRestock.Focus();
                restock.ShowDialog();
            }
            else if (column_inventory == "Edit")
            {
                decimal price;
                Add_New_Item_Form item = new Add_New_Item_Form(this);
                item.btnSave.Text = "Update";
                item.lblItemTitle.Text = "Update the item information";
                item.setItemID = getItemID;
                item.txt_ItemCode.Text = getItemCode;
                item.txt_ItemName.Text = getItemName;
                item.cb_Category.SelectedIndex = getCategory;
                item.txt_Quantity.Enabled = false;
                item.txt_Quantity.Text = getQuantity;
                if (decimal.TryParse(getPrice, out price))
                {
                    item.txt_Price.Text = price.ToString("0");
                }
                else
                {
                    item.txt_Price.Text = "Invalid Price";
                }
                item.ShowDialog();
            }
            else if (column_inventory == "Delete")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    updateData.Update_ItemToDeleted(getItemID);
                    DisplayInventory();
                }
            }
        }

        private void grid_item_inventory_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_item_inventory.CurrentRow != null)
            {
                int i = grid_item_inventory.CurrentRow.Index;

                if (int.TryParse(grid_item_inventory[1, i].Value.ToString(), out int selectItemID))
                {
                    getItemID = selectItemID;
                    getItemCode = grid_item_inventory[2, i].Value.ToString();
                    getItemName = grid_item_inventory[3, i].Value.ToString();
                    getCategory = ConvertItemCategory(grid_item_inventory[4, i].Value.ToString());
                    getQuantity = grid_item_inventory[5, i].Value.ToString();
                    getPrice = grid_item_inventory[6, i].Value.ToString();
                }
            }
        }

        private int ConvertItemCategory(string itemCategory)
        {
            switch (itemCategory)
            {
                case "Liquid":
                    return 0;
                case "Detergent":
                    return 1;
                default:
                    // Handle unknown status or return a default value
                    return -1;
            }
        }
    }
}
