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
            DisplayHistoryLog();
            DisplayCalendarView();
            DisplayPendingPayment();
        }

        // for downward panel
        private void openChildPanelDownward(Form childPanel)
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

        // for upward panel
        private void openChildPanelUpward(Form childPanel)
        {
            if (activeForm != null)

                activeForm.Close();
            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            panelSection1.Controls.Add(childPanel);
            panelSection1.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }

        // for sideward panel
        private void openChildPanelSideward(Form childPanel)
        {
            if (activeForm != null)

                activeForm.Close();
            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            panelSection3.Controls.Add(childPanel);
            panelSection3.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }

        private void openChildPanelSection2(Form childPanel)
        {
            if (activeForm != null)

                activeForm.Close();
            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            panelSection2.Controls.Add(childPanel);
            panelSection2.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }

        public void DisplayPendingPayment()
        {
            openChildPanelSection2(new Dashboard_Widget.Pending_Widget_Form());
        }
        public void DisplayCalendarView()
        {
            openChildPanelSideward(new Dashboard_Widget.Calendar_Widget_Form());
        }

        public void DisplayHistoryLog()
        {
            openChildPanelUpward(new Dashboard_Widget.ActivityLog_Widget_Form());
        }

        public void DisplayDeliverySummaryStatus()
        {
            openChildPanelDownward(new Dashboard_Widget.Delivery_Widget_Form());
        }

      



    }
}
