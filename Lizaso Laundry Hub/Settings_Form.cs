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
    public partial class Settings_Form : KryptonForm
    {
        private Form activeForm = null;
        private Services_Form servicesForm;

        public Settings_Form()
        {
            InitializeComponent();
        }

        private void openChildPanel(Form childPanel)
        {
            if (activeForm != null)
                activeForm.Close();

            if (childPanel is Services_Form)
            {
                if (servicesForm == null || servicesForm.IsDisposed)
                {
                    servicesForm = (Services_Form)childPanel;
                    servicesForm.FormClosed += (s, args) => servicesForm = null; 
                }
                else
                {
                    servicesForm.BringToFront();
                    return; 
                }
            }

            childPanel.TopLevel = false;
            childPanel.FormBorderStyle = FormBorderStyle.FixedSingle;
            childPanel.Dock = DockStyle.Fill;
            settings_main_panel_dock.Controls.Add(childPanel);
            settings_main_panel_dock.Tag = childPanel;
            childPanel.BringToFront();
            childPanel.Show();
        }

        private void btn_LaundryUnit_Config_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Laundry Unit Configuration";
            openChildPanel(new Settings_Module.LaundryUnit_Configuration_Form());
        }

        private void btn_DashboardPreferences_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Dashboard Preferences";
            openChildPanel(new Settings_Module.Dashboard_Preferences());
        }

        private void btn_BackUpConfig_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Backup and Restore Configuration";
            openChildPanel(new Settings_Module.Backup_Restore_Form());
        }

        private void btn_DataTimeConfig_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Date and Time Configuration";
            openChildPanel(new Settings_Module.DateTime_Configuration());
        }

        private void Settings_Form_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Laundry Unit Configuration";
            openChildPanel(new Settings_Module.LaundryUnit_Configuration_Form());
        }
    }
}
