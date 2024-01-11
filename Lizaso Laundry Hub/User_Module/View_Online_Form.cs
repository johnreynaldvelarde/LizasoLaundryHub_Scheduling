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

namespace Lizaso_Laundry_Hub.User_Module
{
    public partial class View_Online_Form : KryptonForm
    {
        private Get_Data_Class getData;
        private Account_Class account;

        public View_Online_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            account = new Account_Class();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void View_Online_Form_Load(object sender, EventArgs e)
        {
            getData.Get_UserOnlineorOffline(grid_user_view, account.User_ID);
        }

        private void grid_user_view_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in grid_user_view.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void View_Online_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
