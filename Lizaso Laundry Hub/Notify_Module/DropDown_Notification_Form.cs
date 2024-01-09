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

namespace Lizaso_Laundry_Hub.Notify_Module
{
    public partial class DropDown_Notification_Form : KryptonForm
    {
        private Panel panel_upper_noti;
        private Get_Data_Class getData;
        private Account_Class account;

        public DropDown_Notification_Form(Panel panelUpperNoti)
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            account = new Account_Class();

            this.panel_upper_noti = panelUpperNoti;

            if (this.panel_upper_noti != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(850, 50); // Coordinates of the button
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

        public async Task DisplayNotificationLog()
        {
            //await 
        }

        public void LoadNotification()
        {
            try
            {
                notification_flow_panel.Controls.Clear();

                List<NotificationLog> notificationLogs = getData.GetNotificationLog(account.User_ID);

                foreach (var noti in notificationLogs)
                {
                    ucNotification_Control reservedNotify = new ucNotification_Control(noti);
                    notification_flow_panel.Controls.Add(reservedNotify);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Load_Unit: {ex.Message}");
            }
        }

        private void DropDown_Notification_Form_Load(object sender, EventArgs e)
        {
            LoadNotification();
        }
    }
}
