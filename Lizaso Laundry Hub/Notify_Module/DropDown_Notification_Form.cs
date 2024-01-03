﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Lizaso_Laundry_Hub.Notify_Module
{
    public partial class DropDown_Notification_Form : KryptonForm
    {
        private Panel panel_upper_noti;

        public DropDown_Notification_Form(Panel panelUpperNoti)
        {
            InitializeComponent();

            this.panel_upper_noti = panelUpperNoti;

            if (this.panel_upper_noti != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(900, 50); // Coordinates of the button
            }
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(this.panel_upper_noti.Right, this.panel_upper_noti.Bottom);
            }

            this.Show();
        }

        private void DropDown_Notification_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
