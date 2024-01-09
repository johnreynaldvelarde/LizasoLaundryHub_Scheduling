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
    public partial class ActivityLog_Widget_Form : KryptonForm
    {
        private Get_Data_Class getData;

        public ActivityLog_Widget_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        public void DisplayUserActivityLog()
        {
            getData.Get_AllUsersActivityLog(grid_activity_view);
        }

        private void ActivityLog_Widget_Form_Load(object sender, EventArgs e)
        {
            DisplayUserActivityLog();
        }
    }
}
