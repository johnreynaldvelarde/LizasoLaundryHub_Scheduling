using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub.Notify_Module
{
    public partial class ucNotification_Control : UserControl
    {
        public NotificationLog Log { get; private set; }

        public ucNotification_Control(NotificationLog log)
        {
            InitializeComponent();
            Log = log;
        }

        public void ShowNotification()
        {
            int LogID = Log.LogID;

        }
    }
}
