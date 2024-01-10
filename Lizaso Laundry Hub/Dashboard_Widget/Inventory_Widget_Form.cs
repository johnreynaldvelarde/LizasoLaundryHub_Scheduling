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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    public partial class Inventory_Widget_Form : KryptonForm
    {
        public Inventory_Widget_Form()
        {
            InitializeComponent();
            DisplayListView();
        }

        public void DisplayInventoryQuantity()
        {

        }

        public void DisplayListView()
        {
            /*
            // Assuming you have a list of items with their quantities
            var itemList = new[]
            {
            new { ItemName = "Item1", Quantity = 10 },
            new { ItemName = "Item2", Quantity = 20 },
            new { ItemName = "Item3", Quantity = 15 },
            // Add more items as needed
        };

            foreach (var item in itemList)
            {
                // Create a new ListViewItem with the item name
                var listViewItem = new ListViewItem(item.ItemName);

                // Add the quantity as a sub-item
                listViewItem.SubItems.Add(item.Quantity.ToString());

                // Add the ListViewItem to the ListView
                listView1.Items.Add(listViewItem);
            }
            */
        }
    }
}
