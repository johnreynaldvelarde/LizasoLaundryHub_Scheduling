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
    public partial class View_DeliveryList_Form : KryptonForm
    {
        private Get_Data_Class getData;

        public View_DeliveryList_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        private void View_DeliveryList_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
