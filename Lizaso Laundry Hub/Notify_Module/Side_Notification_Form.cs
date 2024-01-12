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
    public partial class Side_Notification_Form : KryptonForm
    {
        public static Side_Notification_Form Instance { get; private set; }
        public int colorStatus;
        public string messageSent;

        public Side_Notification_Form()
        {   
            InitializeComponent();
            this.Opacity = 0;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            int formWidth = this.Width;
            int formHeight = this.Height;

            int x = screenWidth - formWidth;
            int y = screenHeight - formHeight;

            this.Location = new Point(x, y);

        }

        public void ColorBlueWarning()
        {
            lbl_Title.Text = "Information";
            lbl_Title.ForeColor = Color.FromArgb(8, 131, 186);
            lbl_show_string.ForeColor = Color.FromArgb(8, 131, 186);
            btnClose.Image = Properties.Resources.BlueClose;
            image_sign.Image = Properties.Resources.Information;
            lbl_show_string.Text = messageSent;
            kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = Color.FromArgb(8, 131, 186);
            kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = Color.FromArgb(8, 131, 186);
        }

        public void ColorGreenSuccess()
        {
            lbl_Title.Text = "Sucess";
            lbl_Title.ForeColor = Color.FromArgb(79, 179, 139);
            lbl_show_string.ForeColor = Color.FromArgb(79, 179, 139);
            btnClose.Image = Properties.Resources.GreenClose;
            image_sign.Image = Properties.Resources.Sucess;
            lbl_show_string.Text = messageSent;
            kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = Color.FromArgb(8, 131, 186);
            kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = Color.FromArgb(8, 131, 186);
        }

        public void ColorRedError()
        {

        }

        public void ColorYellowWarning()
        {

        }

        private void Side_Notification_Form_Load(object sender, EventArgs e)
        {
            if (colorStatus == 0)
            {
                ColorGreenSuccess();
            }
            else if (colorStatus == 1)
            {
                ColorBlueWarning();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Side_Notification_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.05; // Adjust the increment as needed for smoothness
            }
            else
            {
                ((Timer)sender).Stop();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
    }
}
