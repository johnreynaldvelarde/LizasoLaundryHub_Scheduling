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
        private Get_Data_Class getData;

        public Inventory_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            
        }

        public void DisplayInventorySummary()
        {
            getData.Get_AllCountItemQytAndLoss(Label_Total, Label_Loss, Label_ItemName);
        }

        private void btn_ViewAllInventory_CheckedChanged(object sender, EventArgs e)
        {
            View_AllInventory_Form inventory = new View_AllInventory_Form();
            inventory.Show();
        }

        private void Inventory_Widget_Form_Load(object sender, EventArgs e)
        {
            DisplayInventorySummary();
        }
    }
}
