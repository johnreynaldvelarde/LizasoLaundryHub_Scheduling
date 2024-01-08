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
    public partial class Dashboard_Form : KryptonForm
    {
        private Form activeForm = null;

        public Dashboard_Form()
        {
            InitializeComponent();
            DisplayDeliverySummaryStatus();
        }

        public void DisplayCustomerRankings()
        {
            /*
            if ()
            {

            }
            else
            {

            }
            */
        }

        public void DisplayDeliverySummaryStatus()
        {
            openChildPanel(new Dashboard_Widget.Delivery_Widget_Form());
        }

        private void openChildPanel(Form childPanel)
        {
            if (activeForm != null)

            activeForm.Close();
            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            panel_downward.Controls.Add(childPanel);
            panel_downward.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }



    }
}
