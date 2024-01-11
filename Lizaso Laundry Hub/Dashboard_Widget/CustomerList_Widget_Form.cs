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

namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    public partial class CustomerList_Widget_Form : KryptonForm
    {
        private Get_Data_Class getData;
        public CustomerList_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        public void DisplayCustomerList()
        {
            getData.Get_AllCustomerNameandItsCustomerType(grid_allcustomer_view);
        }

        private void CustomerList_Widget_Form_Load(object sender, EventArgs e)
        {
            DisplayCustomerList();
        }

        private void grid_customer_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
    }
}
