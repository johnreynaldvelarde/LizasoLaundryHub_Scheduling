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
    public partial class DropDown_Form : KryptonForm
    {
        private Panel panel_upper;
        
        public DropDown_Form(Panel panelUpper)
        {
            InitializeComponent();

            this.panel_upper = panelUpper;

            if (this.panel_upper != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(1160, 50); // Coordinates of the button
            }
            else
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(this.panel_upper.Right, this.panel_upper.Bottom);
            }

            this.Show();
        }

    
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                this.Dispose();
                Application.OpenForms["Main_Form"].Dispose();
                Login_Form frm = new Login_Form();
                frm.Show();
            }
            else
            {
                this.Dispose();
                Application.OpenForms["Main_Form"].Dispose();
                Login_Form frm = new Login_Form();
                frm.Show();
                //this.Show();
            }
        }

        private void DropDown_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
