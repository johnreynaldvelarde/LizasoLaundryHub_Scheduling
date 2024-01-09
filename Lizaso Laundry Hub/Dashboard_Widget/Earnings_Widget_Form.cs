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
    public partial class Earnings_Widget_Form : KryptonForm
    {
        private Get_Data_Class getData;

        public Earnings_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        public void DisplayTotalEarnings()
        {
            getData.Get_Transaction_Amount(lblTotalEarnings, lblDaily, lblWeekly, lblMonthly);
        }

        private void Earnings_Widget_Form_Load(object sender, EventArgs e)
        {
            DisplayTotalEarnings();
        }
    }
}
