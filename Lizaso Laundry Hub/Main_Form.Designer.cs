namespace Lizaso_Laundry_Hub
{
    partial class Main_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblUpperTime = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_upper = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.Count_Pending_Timer = new System.Windows.Forms.Timer(this.components);
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btn_Settings = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Inventory = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_UserMange = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_CustomerManage = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Schedule = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_PendingPayments = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_AvailableServices = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_Dashboard = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.main_panelDock = new System.Windows.Forms.Panel();
            this.btnNotification = new System.Windows.Forms.Button();
            this.btnDrop = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.panel_upper)).BeginInit();
            this.panel_upper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblUpperTime
            // 
            this.lblUpperTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblUpperTime.AutoSize = true;
            this.lblUpperTime.BackColor = System.Drawing.Color.Transparent;
            this.lblUpperTime.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpperTime.ForeColor = System.Drawing.Color.White;
            this.lblUpperTime.Location = new System.Drawing.Point(543, 12);
            this.lblUpperTime.Name = "lblUpperTime";
            this.lblUpperTime.Size = new System.Drawing.Size(82, 28);
            this.lblUpperTime.TabIndex = 3;
            this.lblUpperTime.Text = "00:00:00";
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(1013, 12);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUserName.Size = new System.Drawing.Size(99, 28);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "User Name";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(56, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 28);
            this.label1.TabIndex = 8;
            this.label1.Text = "Lizaso Laundry Hub";
            // 
            // panel_upper
            // 
            this.panel_upper.Controls.Add(this.btnNotification);
            this.panel_upper.Controls.Add(this.btnDrop);
            this.panel_upper.Controls.Add(this.pictureBox1);
            this.panel_upper.Controls.Add(this.label1);
            this.panel_upper.Controls.Add(this.lblUserName);
            this.panel_upper.Controls.Add(this.lblUpperTime);
            this.panel_upper.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_upper.Location = new System.Drawing.Point(0, 0);
            this.panel_upper.Name = "panel_upper";
            this.panel_upper.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderCustom1;
            this.panel_upper.Size = new System.Drawing.Size(1200, 50);
            this.panel_upper.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.panel_upper.StateCommon.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.panel_upper.StateCommon.ColorAngle = 0F;
            this.panel_upper.TabIndex = 0;
            // 
            // Count_Pending_Timer
            // 
            this.Count_Pending_Timer.Enabled = true;
            this.Count_Pending_Timer.Tick += new System.EventHandler(this.Count_Pending_Timer_Tick);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btn_Settings);
            this.kryptonPanel1.Controls.Add(this.btn_Inventory);
            this.kryptonPanel1.Controls.Add(this.btn_UserMange);
            this.kryptonPanel1.Controls.Add(this.btn_CustomerManage);
            this.kryptonPanel1.Controls.Add(this.btn_Schedule);
            this.kryptonPanel1.Controls.Add(this.btn_PendingPayments);
            this.kryptonPanel1.Controls.Add(this.btn_AvailableServices);
            this.kryptonPanel1.Controls.Add(this.btn_Dashboard);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 525);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1200, 75);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.kryptonPanel1.StateCommon.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.kryptonPanel1.TabIndex = 5;
            // 
            // btn_Settings
            // 
            this.btn_Settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Settings.Location = new System.Drawing.Point(1054, 8);
            this.btn_Settings.Name = "btn_Settings";
            this.btn_Settings.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Settings.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Settings.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_Settings.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Settings.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Settings.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_Settings.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Settings.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Settings.OverrideDefault.Border.Rounding = 5;
            this.btn_Settings.OverrideDefault.Border.Width = 1;
            this.btn_Settings.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Settings.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Settings.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_Settings.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Settings.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Settings.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_Settings.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Settings.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_Settings.Size = new System.Drawing.Size(134, 60);
            this.btn_Settings.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Settings.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Settings.StateCommon.Back.ColorAngle = 45F;
            this.btn_Settings.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Settings.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Settings.StateCommon.Border.ColorAngle = 45F;
            this.btn_Settings.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Settings.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Settings.StateCommon.Border.Rounding = 5;
            this.btn_Settings.StateCommon.Border.Width = 1;
            this.btn_Settings.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_Settings.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_Settings.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Settings.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Settings.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Settings.StatePressed.Back.ColorAngle = 135F;
            this.btn_Settings.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Settings.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Settings.StatePressed.Border.ColorAngle = 135F;
            this.btn_Settings.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Settings.StatePressed.Border.Rounding = 5;
            this.btn_Settings.StatePressed.Border.Width = 1;
            this.btn_Settings.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Settings.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_Settings.StateTracking.Back.ColorAngle = 45F;
            this.btn_Settings.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Settings.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Settings.StateTracking.Border.ColorAngle = 45F;
            this.btn_Settings.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Settings.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Settings.StateTracking.Border.Rounding = 5;
            this.btn_Settings.StateTracking.Border.Width = 1;
            this.btn_Settings.TabIndex = 31;
            this.btn_Settings.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Settings.Values.Image")));
            this.btn_Settings.Values.Text = "Settings";
            this.btn_Settings.Click += new System.EventHandler(this.btn_Settings_Click);
            // 
            // btn_Inventory
            // 
            this.btn_Inventory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Inventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Inventory.Location = new System.Drawing.Point(907, 8);
            this.btn_Inventory.Name = "btn_Inventory";
            this.btn_Inventory.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Inventory.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Inventory.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_Inventory.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Inventory.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Inventory.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_Inventory.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Inventory.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Inventory.OverrideDefault.Border.Rounding = 5;
            this.btn_Inventory.OverrideDefault.Border.Width = 1;
            this.btn_Inventory.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Inventory.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Inventory.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_Inventory.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Inventory.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Inventory.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_Inventory.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Inventory.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_Inventory.Size = new System.Drawing.Size(134, 60);
            this.btn_Inventory.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Inventory.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Inventory.StateCommon.Back.ColorAngle = 45F;
            this.btn_Inventory.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Inventory.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Inventory.StateCommon.Border.ColorAngle = 45F;
            this.btn_Inventory.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Inventory.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Inventory.StateCommon.Border.Rounding = 5;
            this.btn_Inventory.StateCommon.Border.Width = 1;
            this.btn_Inventory.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_Inventory.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_Inventory.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Inventory.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Inventory.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Inventory.StatePressed.Back.ColorAngle = 135F;
            this.btn_Inventory.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Inventory.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Inventory.StatePressed.Border.ColorAngle = 135F;
            this.btn_Inventory.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Inventory.StatePressed.Border.Rounding = 5;
            this.btn_Inventory.StatePressed.Border.Width = 1;
            this.btn_Inventory.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Inventory.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_Inventory.StateTracking.Back.ColorAngle = 45F;
            this.btn_Inventory.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Inventory.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Inventory.StateTracking.Border.ColorAngle = 45F;
            this.btn_Inventory.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Inventory.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Inventory.StateTracking.Border.Rounding = 5;
            this.btn_Inventory.StateTracking.Border.Width = 1;
            this.btn_Inventory.TabIndex = 30;
            this.btn_Inventory.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Inventory.Values.Image")));
            this.btn_Inventory.Values.Text = "Inventory";
            this.btn_Inventory.Click += new System.EventHandler(this.btn_Inventory_Click_1);
            // 
            // btn_UserMange
            // 
            this.btn_UserMange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_UserMange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_UserMange.Location = new System.Drawing.Point(757, 8);
            this.btn_UserMange.Name = "btn_UserMange";
            this.btn_UserMange.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_UserMange.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_UserMange.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_UserMange.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_UserMange.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_UserMange.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_UserMange.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_UserMange.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_UserMange.OverrideDefault.Border.Rounding = 5;
            this.btn_UserMange.OverrideDefault.Border.Width = 1;
            this.btn_UserMange.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_UserMange.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_UserMange.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_UserMange.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_UserMange.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_UserMange.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_UserMange.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_UserMange.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_UserMange.Size = new System.Drawing.Size(134, 60);
            this.btn_UserMange.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_UserMange.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_UserMange.StateCommon.Back.ColorAngle = 45F;
            this.btn_UserMange.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_UserMange.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_UserMange.StateCommon.Border.ColorAngle = 45F;
            this.btn_UserMange.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_UserMange.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_UserMange.StateCommon.Border.Rounding = 5;
            this.btn_UserMange.StateCommon.Border.Width = 1;
            this.btn_UserMange.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_UserMange.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_UserMange.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UserMange.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_UserMange.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_UserMange.StatePressed.Back.ColorAngle = 135F;
            this.btn_UserMange.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_UserMange.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_UserMange.StatePressed.Border.ColorAngle = 135F;
            this.btn_UserMange.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_UserMange.StatePressed.Border.Rounding = 5;
            this.btn_UserMange.StatePressed.Border.Width = 1;
            this.btn_UserMange.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_UserMange.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_UserMange.StateTracking.Back.ColorAngle = 45F;
            this.btn_UserMange.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_UserMange.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_UserMange.StateTracking.Border.ColorAngle = 45F;
            this.btn_UserMange.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_UserMange.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_UserMange.StateTracking.Border.Rounding = 5;
            this.btn_UserMange.StateTracking.Border.Width = 1;
            this.btn_UserMange.TabIndex = 29;
            this.btn_UserMange.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_UserMange.Values.Image")));
            this.btn_UserMange.Values.Text = "Manage\r\n  Users";
            this.btn_UserMange.Click += new System.EventHandler(this.btn_UserMange_Click);
            // 
            // btn_CustomerManage
            // 
            this.btn_CustomerManage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_CustomerManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_CustomerManage.Location = new System.Drawing.Point(608, 8);
            this.btn_CustomerManage.Name = "btn_CustomerManage";
            this.btn_CustomerManage.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_CustomerManage.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_CustomerManage.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_CustomerManage.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_CustomerManage.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_CustomerManage.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_CustomerManage.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_CustomerManage.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_CustomerManage.OverrideDefault.Border.Rounding = 5;
            this.btn_CustomerManage.OverrideDefault.Border.Width = 1;
            this.btn_CustomerManage.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_CustomerManage.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_CustomerManage.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_CustomerManage.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_CustomerManage.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_CustomerManage.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_CustomerManage.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_CustomerManage.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_CustomerManage.Size = new System.Drawing.Size(134, 60);
            this.btn_CustomerManage.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_CustomerManage.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_CustomerManage.StateCommon.Back.ColorAngle = 45F;
            this.btn_CustomerManage.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_CustomerManage.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_CustomerManage.StateCommon.Border.ColorAngle = 45F;
            this.btn_CustomerManage.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_CustomerManage.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_CustomerManage.StateCommon.Border.Rounding = 5;
            this.btn_CustomerManage.StateCommon.Border.Width = 1;
            this.btn_CustomerManage.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_CustomerManage.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_CustomerManage.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CustomerManage.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_CustomerManage.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_CustomerManage.StatePressed.Back.ColorAngle = 135F;
            this.btn_CustomerManage.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_CustomerManage.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_CustomerManage.StatePressed.Border.ColorAngle = 135F;
            this.btn_CustomerManage.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_CustomerManage.StatePressed.Border.Rounding = 5;
            this.btn_CustomerManage.StatePressed.Border.Width = 1;
            this.btn_CustomerManage.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_CustomerManage.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_CustomerManage.StateTracking.Back.ColorAngle = 45F;
            this.btn_CustomerManage.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_CustomerManage.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_CustomerManage.StateTracking.Border.ColorAngle = 45F;
            this.btn_CustomerManage.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_CustomerManage.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_CustomerManage.StateTracking.Border.Rounding = 5;
            this.btn_CustomerManage.StateTracking.Border.Width = 1;
            this.btn_CustomerManage.TabIndex = 28;
            this.btn_CustomerManage.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_CustomerManage.Values.Image")));
            this.btn_CustomerManage.Values.Text = "Customer\r\n Manage";
            this.btn_CustomerManage.Click += new System.EventHandler(this.btn_CustomerManage_Click);
            // 
            // btn_Schedule
            // 
            this.btn_Schedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Schedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Schedule.Location = new System.Drawing.Point(459, 8);
            this.btn_Schedule.Name = "btn_Schedule";
            this.btn_Schedule.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Schedule.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Schedule.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_Schedule.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Schedule.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Schedule.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_Schedule.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Schedule.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Schedule.OverrideDefault.Border.Rounding = 5;
            this.btn_Schedule.OverrideDefault.Border.Width = 1;
            this.btn_Schedule.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Schedule.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Schedule.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_Schedule.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Schedule.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Schedule.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_Schedule.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Schedule.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_Schedule.Size = new System.Drawing.Size(134, 60);
            this.btn_Schedule.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Schedule.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Schedule.StateCommon.Back.ColorAngle = 45F;
            this.btn_Schedule.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Schedule.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Schedule.StateCommon.Border.ColorAngle = 45F;
            this.btn_Schedule.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Schedule.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Schedule.StateCommon.Border.Rounding = 5;
            this.btn_Schedule.StateCommon.Border.Width = 1;
            this.btn_Schedule.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_Schedule.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_Schedule.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Schedule.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Schedule.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Schedule.StatePressed.Back.ColorAngle = 135F;
            this.btn_Schedule.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Schedule.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Schedule.StatePressed.Border.ColorAngle = 135F;
            this.btn_Schedule.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Schedule.StatePressed.Border.Rounding = 5;
            this.btn_Schedule.StatePressed.Border.Width = 1;
            this.btn_Schedule.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Schedule.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_Schedule.StateTracking.Back.ColorAngle = 45F;
            this.btn_Schedule.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Schedule.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Schedule.StateTracking.Border.ColorAngle = 45F;
            this.btn_Schedule.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Schedule.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Schedule.StateTracking.Border.Rounding = 5;
            this.btn_Schedule.StateTracking.Border.Width = 1;
            this.btn_Schedule.TabIndex = 27;
            this.btn_Schedule.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Schedule.Values.Image")));
            this.btn_Schedule.Values.Text = "Schedule";
            this.btn_Schedule.Click += new System.EventHandler(this.btn_Schedule_Click);
            // 
            // btn_PendingPayments
            // 
            this.btn_PendingPayments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_PendingPayments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_PendingPayments.Location = new System.Drawing.Point(310, 8);
            this.btn_PendingPayments.Name = "btn_PendingPayments";
            this.btn_PendingPayments.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_PendingPayments.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_PendingPayments.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_PendingPayments.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_PendingPayments.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_PendingPayments.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_PendingPayments.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_PendingPayments.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_PendingPayments.OverrideDefault.Border.Rounding = 5;
            this.btn_PendingPayments.OverrideDefault.Border.Width = 1;
            this.btn_PendingPayments.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_PendingPayments.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_PendingPayments.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_PendingPayments.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_PendingPayments.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_PendingPayments.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_PendingPayments.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_PendingPayments.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_PendingPayments.Size = new System.Drawing.Size(134, 60);
            this.btn_PendingPayments.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_PendingPayments.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_PendingPayments.StateCommon.Back.ColorAngle = 45F;
            this.btn_PendingPayments.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_PendingPayments.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_PendingPayments.StateCommon.Border.ColorAngle = 45F;
            this.btn_PendingPayments.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_PendingPayments.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_PendingPayments.StateCommon.Border.Rounding = 5;
            this.btn_PendingPayments.StateCommon.Border.Width = 1;
            this.btn_PendingPayments.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_PendingPayments.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_PendingPayments.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PendingPayments.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_PendingPayments.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_PendingPayments.StatePressed.Back.ColorAngle = 135F;
            this.btn_PendingPayments.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_PendingPayments.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_PendingPayments.StatePressed.Border.ColorAngle = 135F;
            this.btn_PendingPayments.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_PendingPayments.StatePressed.Border.Rounding = 5;
            this.btn_PendingPayments.StatePressed.Border.Width = 1;
            this.btn_PendingPayments.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_PendingPayments.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_PendingPayments.StateTracking.Back.ColorAngle = 45F;
            this.btn_PendingPayments.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_PendingPayments.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_PendingPayments.StateTracking.Border.ColorAngle = 45F;
            this.btn_PendingPayments.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_PendingPayments.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_PendingPayments.StateTracking.Border.Rounding = 5;
            this.btn_PendingPayments.StateTracking.Border.Width = 1;
            this.btn_PendingPayments.TabIndex = 26;
            this.btn_PendingPayments.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_PendingPayments.Values.Image")));
            this.btn_PendingPayments.Values.Text = "   Pending\r\n Payments";
            this.btn_PendingPayments.Click += new System.EventHandler(this.btn_PendingPayments_Click);
            // 
            // btn_AvailableServices
            // 
            this.btn_AvailableServices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_AvailableServices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AvailableServices.Location = new System.Drawing.Point(161, 8);
            this.btn_AvailableServices.Name = "btn_AvailableServices";
            this.btn_AvailableServices.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_AvailableServices.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_AvailableServices.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_AvailableServices.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_AvailableServices.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_AvailableServices.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_AvailableServices.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_AvailableServices.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_AvailableServices.OverrideDefault.Border.Rounding = 5;
            this.btn_AvailableServices.OverrideDefault.Border.Width = 1;
            this.btn_AvailableServices.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_AvailableServices.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_AvailableServices.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_AvailableServices.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_AvailableServices.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_AvailableServices.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_AvailableServices.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_AvailableServices.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_AvailableServices.Size = new System.Drawing.Size(134, 60);
            this.btn_AvailableServices.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_AvailableServices.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_AvailableServices.StateCommon.Back.ColorAngle = 45F;
            this.btn_AvailableServices.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_AvailableServices.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_AvailableServices.StateCommon.Border.ColorAngle = 45F;
            this.btn_AvailableServices.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_AvailableServices.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_AvailableServices.StateCommon.Border.Rounding = 5;
            this.btn_AvailableServices.StateCommon.Border.Width = 1;
            this.btn_AvailableServices.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_AvailableServices.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_AvailableServices.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AvailableServices.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_AvailableServices.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_AvailableServices.StatePressed.Back.ColorAngle = 135F;
            this.btn_AvailableServices.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_AvailableServices.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_AvailableServices.StatePressed.Border.ColorAngle = 135F;
            this.btn_AvailableServices.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_AvailableServices.StatePressed.Border.Rounding = 5;
            this.btn_AvailableServices.StatePressed.Border.Width = 1;
            this.btn_AvailableServices.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_AvailableServices.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_AvailableServices.StateTracking.Back.ColorAngle = 45F;
            this.btn_AvailableServices.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_AvailableServices.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_AvailableServices.StateTracking.Border.ColorAngle = 45F;
            this.btn_AvailableServices.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_AvailableServices.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_AvailableServices.StateTracking.Border.Rounding = 5;
            this.btn_AvailableServices.StateTracking.Border.Width = 1;
            this.btn_AvailableServices.TabIndex = 25;
            this.btn_AvailableServices.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_AvailableServices.Values.Image")));
            this.btn_AvailableServices.Values.Text = "Available \r\n Services";
            this.btn_AvailableServices.Click += new System.EventHandler(this.btn_AvailableServices_Click);
            // 
            // btn_Dashboard
            // 
            this.btn_Dashboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Dashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Dashboard.Location = new System.Drawing.Point(12, 8);
            this.btn_Dashboard.Name = "btn_Dashboard";
            this.btn_Dashboard.OverrideDefault.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Dashboard.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Dashboard.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_Dashboard.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Dashboard.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Dashboard.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_Dashboard.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Dashboard.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Dashboard.OverrideDefault.Border.Rounding = 5;
            this.btn_Dashboard.OverrideDefault.Border.Width = 1;
            this.btn_Dashboard.OverrideFocus.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Dashboard.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Dashboard.OverrideFocus.Back.ColorAngle = 45F;
            this.btn_Dashboard.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Dashboard.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this.btn_Dashboard.OverrideFocus.Border.ColorAngle = 45F;
            this.btn_Dashboard.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Dashboard.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_Dashboard.Size = new System.Drawing.Size(134, 60);
            this.btn_Dashboard.StateCommon.Back.Color1 = System.Drawing.Color.SteelBlue;
            this.btn_Dashboard.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Dashboard.StateCommon.Back.ColorAngle = 45F;
            this.btn_Dashboard.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Dashboard.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Dashboard.StateCommon.Border.ColorAngle = 45F;
            this.btn_Dashboard.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Dashboard.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Dashboard.StateCommon.Border.Rounding = 5;
            this.btn_Dashboard.StateCommon.Border.Width = 1;
            this.btn_Dashboard.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_Dashboard.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_Dashboard.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Dashboard.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Dashboard.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Dashboard.StatePressed.Back.ColorAngle = 135F;
            this.btn_Dashboard.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_Dashboard.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(70)))), ((int)(((byte)(40)))));
            this.btn_Dashboard.StatePressed.Border.ColorAngle = 135F;
            this.btn_Dashboard.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Dashboard.StatePressed.Border.Rounding = 5;
            this.btn_Dashboard.StatePressed.Border.Width = 1;
            this.btn_Dashboard.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_Dashboard.StateTracking.Back.Color2 = System.Drawing.Color.SteelBlue;
            this.btn_Dashboard.StateTracking.Back.ColorAngle = 45F;
            this.btn_Dashboard.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.btn_Dashboard.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.btn_Dashboard.StateTracking.Border.ColorAngle = 45F;
            this.btn_Dashboard.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_Dashboard.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_Dashboard.StateTracking.Border.Rounding = 5;
            this.btn_Dashboard.StateTracking.Border.Width = 1;
            this.btn_Dashboard.TabIndex = 24;
            this.btn_Dashboard.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_Dashboard.Values.Image")));
            this.btn_Dashboard.Values.Text = "Dashboard";
            this.btn_Dashboard.Click += new System.EventHandler(this.btn_Dashboard_Click);
            // 
            // main_panelDock
            // 
            this.main_panelDock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.main_panelDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panelDock.Location = new System.Drawing.Point(0, 50);
            this.main_panelDock.Name = "main_panelDock";
            this.main_panelDock.Padding = new System.Windows.Forms.Padding(10);
            this.main_panelDock.Size = new System.Drawing.Size(1200, 475);
            this.main_panelDock.TabIndex = 2;
            // 
            // btnNotification
            // 
            this.btnNotification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotification.BackColor = System.Drawing.Color.Transparent;
            this.btnNotification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotification.FlatAppearance.BorderSize = 0;
            this.btnNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotification.Image = global::Lizaso_Laundry_Hub.Properties.Resources.BellWithRed;
            this.btnNotification.Location = new System.Drawing.Point(1118, 0);
            this.btnNotification.Name = "btnNotification";
            this.btnNotification.Size = new System.Drawing.Size(33, 50);
            this.btnNotification.TabIndex = 11;
            this.btnNotification.UseVisualStyleBackColor = false;
            this.btnNotification.Click += new System.EventHandler(this.btnNotification_Click);
            // 
            // btnDrop
            // 
            this.btnDrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDrop.BackColor = System.Drawing.Color.Transparent;
            this.btnDrop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDrop.FlatAppearance.BorderSize = 0;
            this.btnDrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrop.Image = ((System.Drawing.Image)(resources.GetObject("btnDrop.Image")));
            this.btnDrop.Location = new System.Drawing.Point(1157, 0);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(31, 50);
            this.btnDrop.TabIndex = 10;
            this.btnDrop.UseVisualStyleBackColor = false;
            this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Lizaso_Laundry_Hub.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(240)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.ControlBox = false;
            this.Controls.Add(this.main_panelDock);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.panel_upper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main_Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel_upper)).EndInit();
            this.panel_upper.ResumeLayout(false);
            this.panel_upper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblUpperTime;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel_upper;
        private System.Windows.Forms.Button btnNotification;
        private System.Windows.Forms.Button btnDrop;
        private System.Windows.Forms.Timer Count_Pending_Timer;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.Panel main_panelDock;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Dashboard;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Schedule;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_PendingPayments;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_AvailableServices;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_CustomerManage;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_UserMange;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Inventory;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_Settings;
    }
}