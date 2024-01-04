using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        private Backup_Data_Class backupData;
        //private Main_Form mainForm = new Main_Form(authenticatedUser);

        public DropDown_Form(Panel panelUpper)
        {
            InitializeComponent();
            backupData = new Backup_Data_Class();
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

    
        private async void btn_Logout_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayUIBackup();
                this.Dispose();
                // Introduce a 5-second delay
                await Task.Delay(2000);

                Application.OpenForms["Main_Form"].Dispose();

                Login_Form frm = new Login_Form();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisplayUIBackup()
        {
            backupData.BackupDatabaseEveryLogout();
           // mainForm.image_database_save.Visible = true;
           // mainForm.lbl_ShowAutoBackup.Visible = true;
        }



        private void DropDown_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
            /*
           DialogResult res;
           res = MessageBox.Show("Do you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

           if (res == DialogResult.Yes)
           {
               /
               this.Dispose();
               Application.OpenForms["Main_Form"].Dispose();
               Login_Form frm = new Login_Form();
               frm.Show();
           }
           else
           {
               backupData.BackupDatabaseEveryLogout();
               this.Dispose();
               Application.OpenForms["Main_Form"].Dispose();
               Login_Form frm = new Login_Form();
               frm.Show();

           }
           */
        }
    }
}
