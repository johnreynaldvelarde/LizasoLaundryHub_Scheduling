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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Lizaso_Laundry_Hub
{
    public partial class Login_Form : KryptonForm
    {
        private Insert_Data_Class insertData;
        private Get_Data_Class getData;
        private Backup_Data_Class backupData;

        private string default_user = "Type your username";
        private string default_pass = "Type your password";
        private bool isDefaultPassShown = true;

        public Login_Form()
        {
            InitializeComponent();
            insertData = new Insert_Data_Class();
            getData = new Get_Data_Class();
            backupData = new Backup_Data_Class();
            
        }

        private void txt_username_Enter(object sender, EventArgs e)
        {
            if (txt_username.Text == default_user)
            {
                txt_username.Text = string.Empty;
            }
        }

        private void txt_password_Enter(object sender, EventArgs e)
        {
            if (isDefaultPassShown)
            {
                txt_password.Text = string.Empty;
                isDefaultPassShown = false; 
                txt_password.PasswordChar = '\0'; 
            }
        }

        private void txt_password_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_password.Text))
            {
                HidePassword();
            }
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            if (!isDefaultPassShown)
            {
                UpdatePasswordVisibility();
            }
        }

        private void Login_Form_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_username.Text))
            {
                txt_username.Text = default_user;

            }
            if (string.IsNullOrWhiteSpace(txt_password.Text))
            {
                txt_password.Text = default_pass;
            }

            txt_username.Enabled = false;
            txt_username.Enabled = true;

            txt_password.Enabled = false;
            txt_password.Enabled = true;
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            insertData.Automatic_Create_Super_User();
            backupData.CreateLizasoLaundryHubFolder();

            txt_username.Text = default_user;
            txt_password.Text = default_pass;
            HidePassword();
            panelMessage.Visible = false;
            txt_username.Text = default_user;
            txt_password.Text = default_pass;
        }

        private void HidePassword()
        {
            txt_password.PasswordChar = '\0'; 
            txt_password.Text = default_pass;
            isDefaultPassShown = true;
        }

        private void ShowPassword()
        {
            txt_password.PasswordChar = '\0'; 
            txt_password.Text = "";
            isDefaultPassShown = false;
        }

        private void txt_password_GotFocus(object sender, EventArgs e)
        {
            if (isDefaultPassShown)
            {
                ShowPassword();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_username.Text) || txt_username.Text == default_user)
            {
                ShowErrorMessage("Please enter a valid username.");
            }
            else if (String.IsNullOrEmpty(txt_password.Text) || txt_password.Text == default_pass)
            {
                ShowErrorMessage("Please enter a valid password.");
            }
            else
            {
                Account_Class authenticatedUser = getData.AuthenticateUser(txt_username.Text, txt_password.Text);

                if (authenticatedUser != null)
                {
                    HideErrorMessage();
                    HidePassword();

                    if (getData.IsSuperUser(authenticatedUser.User_ID))
                    {
                        Main_Form mainForm = new Main_Form(authenticatedUser);
                        mainForm.Show();
                    }
                    else
                    {
                        Regular_User_Form regularUserForm = new Regular_User_Form(authenticatedUser);
                        regularUserForm.Show();
                    }

                    this.Hide();
                }
                else
                {
                    ShowErrorMessage("Invalid username or password.");
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = message;
            panelMessage.Visible = true;
        }

        private void HideErrorMessage()
        {
            panelMessage.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCloseMessage_Click(object sender, EventArgs e)
        {
            HideErrorMessage();
        }

        private void btnCloseMessage_MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.MouseOverBackColor = button.BackColor;
        }

        private void btnCloseMessage_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
        }

        private void btnCloseMessage_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.MouseDownBackColor = button.BackColor;
        }

        private void btnCloseMessage_MouseUp(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.FlatAppearance.MouseDownBackColor = SystemColors.Control;
        }

        private void passhide_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePasswordVisibility();
        }

        private void UpdatePasswordVisibility()
        {
            if (passhide.Checked)
            {
                txt_password.PasswordChar = '\0';
            }
            else
            {
                txt_password.PasswordChar = '*';
            }
        }
    }
}
