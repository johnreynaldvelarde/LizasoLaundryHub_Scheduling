﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        }

        // Section 1 openChildPanel
        private void openChildPanelSection1(Form childPanel)
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

        // Section 2 openChildPanel
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

        // Section 3 openChildPanel
        private void openChildPanelSection3(Form childPanel)
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

        // Section 4 openChildPanel
        private void openChildPanelSection4(Form childPanel)
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

        public void CheckDashboardPreferences()
        {
            string filePath = Path.Combine(@"C:\Lizaso Laundry Hub\System Settings", "Dashboard Preferences.txt");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Dashboard Preferences file not found. Please check the configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();

                            string[] parts = line.Split(':');

                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                bool value = bool.Parse(parts[1].Trim());

                                switch (key)
                                {
                                    //Section 1
                                    case "All Users Activity Log":
                                        if (value)
                                            openChildPanelSection1(new Dashboard_Widget.ActivityLog_Widget_Form());
                                        break;

                                    case "Customer List View":
                                        if (value)
                                            openChildPanelSection1(new Dashboard_Widget.CustomerList_Widget_Form());
                                        break;

                                    // Section 2
                                    case "Total Earnings":
                                        if (value)
                                            openChildPanelSection2(new Dashboard_Widget.Earnings_Widget_Form());
                                        break;

                                    case "Inventory Monitor":
                                        if (value)
                                            openChildPanelSection2(new Dashboard_Widget.Inventory_Widget_Form());
                                        break;

                                    // Section 3
                                    case "Calendar View":
                                        if (value)
                                            openChildPanelSection3(new Dashboard_Widget.Calendar_Widget_Form());
                                        break;

                                    case "Statistic View":
                                        if (value)
                                            openChildPanelSection3(new Dashboard_Widget.Stats_Widget_Form());
                                        break;

                                    // Section 4
                                    case "Delivery Summary":
                                        if (value)
                                            openChildPanelSection4(new Dashboard_Widget.Delivery_Widget_Form());
                                        break;

                                    case "Payment Pending List":
                                        if (value)
                                            openChildPanelSection4(new Dashboard_Widget.Pending_Widget_Form());
                                        break;

                                    default:

                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Dashboard_Form_Load(object sender, EventArgs e)
        {
            CheckDashboardPreferences();
        }
    }
}
