namespace Lizaso_Laundry_Hub
{
    partial class Settings_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings_Form));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.settings_main_panel_dock = new System.Windows.Forms.Panel();
            this.side_panel = new System.Windows.Forms.Panel();
            this.btn_DataTimeConfig = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_BackUpConfig = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btn_LaundryUnit_Config = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btn_DashboardPreferences = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.side_panel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 12;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1160, 50);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1160, 3);
            this.panel3.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Poppins SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.label6.Location = new System.Drawing.Point(14, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(256, 37);
            this.label6.TabIndex = 1;
            this.label6.Text = "Settings Configuration";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 520);
            this.panel1.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.settings_main_panel_dock);
            this.panel4.Controls.Add(this.side_panel);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1160, 520);
            this.panel4.TabIndex = 9;
            // 
            // settings_main_panel_dock
            // 
            this.settings_main_panel_dock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.settings_main_panel_dock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settings_main_panel_dock.Location = new System.Drawing.Point(330, 80);
            this.settings_main_panel_dock.Name = "settings_main_panel_dock";
            this.settings_main_panel_dock.Padding = new System.Windows.Forms.Padding(10);
            this.settings_main_panel_dock.Size = new System.Drawing.Size(830, 440);
            this.settings_main_panel_dock.TabIndex = 4;
            // 
            // side_panel
            // 
            this.side_panel.Controls.Add(this.btn_DashboardPreferences);
            this.side_panel.Controls.Add(this.btn_DataTimeConfig);
            this.side_panel.Controls.Add(this.btn_BackUpConfig);
            this.side_panel.Controls.Add(this.btn_LaundryUnit_Config);
            this.side_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.side_panel.Location = new System.Drawing.Point(0, 80);
            this.side_panel.Name = "side_panel";
            this.side_panel.Size = new System.Drawing.Size(330, 440);
            this.side_panel.TabIndex = 3;
            // 
            // btn_DataTimeConfig
            // 
            this.btn_DataTimeConfig.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_DataTimeConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DataTimeConfig.Location = new System.Drawing.Point(12, 167);
            this.btn_DataTimeConfig.Name = "btn_DataTimeConfig";
            this.btn_DataTimeConfig.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_DataTimeConfig.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_DataTimeConfig.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_DataTimeConfig.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_DataTimeConfig.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_DataTimeConfig.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_DataTimeConfig.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DataTimeConfig.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_DataTimeConfig.OverrideDefault.Border.Rounding = 5;
            this.btn_DataTimeConfig.OverrideDefault.Border.Width = 1;
            this.btn_DataTimeConfig.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_DataTimeConfig.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_DataTimeConfig.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_DataTimeConfig.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_DataTimeConfig.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DataTimeConfig.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_DataTimeConfig.Size = new System.Drawing.Size(303, 60);
            this.btn_DataTimeConfig.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_DataTimeConfig.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_DataTimeConfig.StateCommon.Back.ColorAngle = 45F;
            this.btn_DataTimeConfig.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_DataTimeConfig.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_DataTimeConfig.StateCommon.Border.ColorAngle = 45F;
            this.btn_DataTimeConfig.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DataTimeConfig.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_DataTimeConfig.StateCommon.Border.Rounding = 5;
            this.btn_DataTimeConfig.StateCommon.Border.Width = 1;
            this.btn_DataTimeConfig.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_DataTimeConfig.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_DataTimeConfig.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DataTimeConfig.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_DataTimeConfig.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(70)))));
            this.btn_DataTimeConfig.StatePressed.Back.ColorAngle = 135F;
            this.btn_DataTimeConfig.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(100)))), ((int)(((byte)(80)))));
            this.btn_DataTimeConfig.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_DataTimeConfig.StatePressed.Border.ColorAngle = 135F;
            this.btn_DataTimeConfig.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DataTimeConfig.StatePressed.Border.Rounding = 5;
            this.btn_DataTimeConfig.StatePressed.Border.Width = 1;
            this.btn_DataTimeConfig.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_DataTimeConfig.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_DataTimeConfig.StateTracking.Back.ColorAngle = 45F;
            this.btn_DataTimeConfig.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_DataTimeConfig.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_DataTimeConfig.StateTracking.Border.ColorAngle = 45F;
            this.btn_DataTimeConfig.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DataTimeConfig.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_DataTimeConfig.StateTracking.Border.Rounding = 5;
            this.btn_DataTimeConfig.StateTracking.Border.Width = 1;
            this.btn_DataTimeConfig.TabIndex = 23;
            this.btn_DataTimeConfig.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_DataTimeConfig.Values.Image")));
            this.btn_DataTimeConfig.Values.Text = "Date and Time Configuration";
            this.btn_DataTimeConfig.Click += new System.EventHandler(this.btn_DataTimeConfig_Click);
            // 
            // btn_BackUpConfig
            // 
            this.btn_BackUpConfig.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_BackUpConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BackUpConfig.Location = new System.Drawing.Point(12, 85);
            this.btn_BackUpConfig.Name = "btn_BackUpConfig";
            this.btn_BackUpConfig.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_BackUpConfig.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_BackUpConfig.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_BackUpConfig.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_BackUpConfig.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_BackUpConfig.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_BackUpConfig.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_BackUpConfig.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_BackUpConfig.OverrideDefault.Border.Rounding = 5;
            this.btn_BackUpConfig.OverrideDefault.Border.Width = 1;
            this.btn_BackUpConfig.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_BackUpConfig.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_BackUpConfig.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_BackUpConfig.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_BackUpConfig.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_BackUpConfig.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_BackUpConfig.Size = new System.Drawing.Size(303, 60);
            this.btn_BackUpConfig.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_BackUpConfig.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_BackUpConfig.StateCommon.Back.ColorAngle = 45F;
            this.btn_BackUpConfig.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_BackUpConfig.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_BackUpConfig.StateCommon.Border.ColorAngle = 45F;
            this.btn_BackUpConfig.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_BackUpConfig.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_BackUpConfig.StateCommon.Border.Rounding = 5;
            this.btn_BackUpConfig.StateCommon.Border.Width = 1;
            this.btn_BackUpConfig.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_BackUpConfig.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_BackUpConfig.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BackUpConfig.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_BackUpConfig.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(70)))));
            this.btn_BackUpConfig.StatePressed.Back.ColorAngle = 135F;
            this.btn_BackUpConfig.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(100)))), ((int)(((byte)(80)))));
            this.btn_BackUpConfig.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_BackUpConfig.StatePressed.Border.ColorAngle = 135F;
            this.btn_BackUpConfig.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_BackUpConfig.StatePressed.Border.Rounding = 5;
            this.btn_BackUpConfig.StatePressed.Border.Width = 1;
            this.btn_BackUpConfig.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_BackUpConfig.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_BackUpConfig.StateTracking.Back.ColorAngle = 45F;
            this.btn_BackUpConfig.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_BackUpConfig.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_BackUpConfig.StateTracking.Border.ColorAngle = 45F;
            this.btn_BackUpConfig.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_BackUpConfig.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_BackUpConfig.StateTracking.Border.Rounding = 5;
            this.btn_BackUpConfig.StateTracking.Border.Width = 1;
            this.btn_BackUpConfig.TabIndex = 22;
            this.btn_BackUpConfig.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_BackUpConfig.Values.Image")));
            this.btn_BackUpConfig.Values.Text = "Backup and Restore Configuration";
            this.btn_BackUpConfig.Click += new System.EventHandler(this.btn_BackUpConfig_Click);
            // 
            // btn_LaundryUnit_Config
            // 
            this.btn_LaundryUnit_Config.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_LaundryUnit_Config.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LaundryUnit_Config.Location = new System.Drawing.Point(12, 6);
            this.btn_LaundryUnit_Config.Name = "btn_LaundryUnit_Config";
            this.btn_LaundryUnit_Config.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_LaundryUnit_Config.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_LaundryUnit_Config.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_LaundryUnit_Config.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_LaundryUnit_Config.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_LaundryUnit_Config.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_LaundryUnit_Config.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_LaundryUnit_Config.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_LaundryUnit_Config.OverrideDefault.Border.Rounding = 5;
            this.btn_LaundryUnit_Config.OverrideDefault.Border.Width = 1;
            this.btn_LaundryUnit_Config.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_LaundryUnit_Config.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_LaundryUnit_Config.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_LaundryUnit_Config.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_LaundryUnit_Config.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_LaundryUnit_Config.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_LaundryUnit_Config.Size = new System.Drawing.Size(303, 60);
            this.btn_LaundryUnit_Config.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_LaundryUnit_Config.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_LaundryUnit_Config.StateCommon.Back.ColorAngle = 45F;
            this.btn_LaundryUnit_Config.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_LaundryUnit_Config.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_LaundryUnit_Config.StateCommon.Border.ColorAngle = 45F;
            this.btn_LaundryUnit_Config.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_LaundryUnit_Config.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_LaundryUnit_Config.StateCommon.Border.Rounding = 5;
            this.btn_LaundryUnit_Config.StateCommon.Border.Width = 1;
            this.btn_LaundryUnit_Config.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_LaundryUnit_Config.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_LaundryUnit_Config.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LaundryUnit_Config.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_LaundryUnit_Config.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(70)))));
            this.btn_LaundryUnit_Config.StatePressed.Back.ColorAngle = 135F;
            this.btn_LaundryUnit_Config.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(100)))), ((int)(((byte)(80)))));
            this.btn_LaundryUnit_Config.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_LaundryUnit_Config.StatePressed.Border.ColorAngle = 135F;
            this.btn_LaundryUnit_Config.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_LaundryUnit_Config.StatePressed.Border.Rounding = 5;
            this.btn_LaundryUnit_Config.StatePressed.Border.Width = 1;
            this.btn_LaundryUnit_Config.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_LaundryUnit_Config.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_LaundryUnit_Config.StateTracking.Back.ColorAngle = 45F;
            this.btn_LaundryUnit_Config.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_LaundryUnit_Config.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_LaundryUnit_Config.StateTracking.Border.ColorAngle = 45F;
            this.btn_LaundryUnit_Config.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_LaundryUnit_Config.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_LaundryUnit_Config.StateTracking.Border.Rounding = 5;
            this.btn_LaundryUnit_Config.StateTracking.Border.Width = 1;
            this.btn_LaundryUnit_Config.TabIndex = 21;
            this.btn_LaundryUnit_Config.Values.Image = ((System.Drawing.Image)(resources.GetObject("btn_LaundryUnit_Config.Values.Image")));
            this.btn_LaundryUnit_Config.Values.Text = "Laundry Unit Configuration";
            this.btn_LaundryUnit_Config.Click += new System.EventHandler(this.btn_LaundryUnit_Config_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblTitle);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1160, 80);
            this.panel5.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Poppins SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.lblTitle.Location = new System.Drawing.Point(618, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(226, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Super User Account";
            // 
            // btn_DashboardPreferences
            // 
            this.btn_DashboardPreferences.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_DashboardPreferences.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DashboardPreferences.Location = new System.Drawing.Point(12, 249);
            this.btn_DashboardPreferences.Name = "btn_DashboardPreferences";
            this.btn_DashboardPreferences.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_DashboardPreferences.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_DashboardPreferences.OverrideDefault.Back.ColorAngle = 45F;
            this.btn_DashboardPreferences.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_DashboardPreferences.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_DashboardPreferences.OverrideDefault.Border.ColorAngle = 45F;
            this.btn_DashboardPreferences.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DashboardPreferences.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_DashboardPreferences.OverrideDefault.Border.Rounding = 5;
            this.btn_DashboardPreferences.OverrideDefault.Border.Width = 1;
            this.btn_DashboardPreferences.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn_DashboardPreferences.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this.btn_DashboardPreferences.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_DashboardPreferences.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btn_DashboardPreferences.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DashboardPreferences.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.btn_DashboardPreferences.Size = new System.Drawing.Size(303, 60);
            this.btn_DashboardPreferences.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_DashboardPreferences.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_DashboardPreferences.StateCommon.Back.ColorAngle = 45F;
            this.btn_DashboardPreferences.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_DashboardPreferences.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_DashboardPreferences.StateCommon.Border.ColorAngle = 45F;
            this.btn_DashboardPreferences.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DashboardPreferences.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_DashboardPreferences.StateCommon.Border.Rounding = 5;
            this.btn_DashboardPreferences.StateCommon.Border.Width = 1;
            this.btn_DashboardPreferences.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btn_DashboardPreferences.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.btn_DashboardPreferences.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DashboardPreferences.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(140)))), ((int)(((byte)(110)))));
            this.btn_DashboardPreferences.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(100)))), ((int)(((byte)(70)))));
            this.btn_DashboardPreferences.StatePressed.Back.ColorAngle = 135F;
            this.btn_DashboardPreferences.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(100)))), ((int)(((byte)(80)))));
            this.btn_DashboardPreferences.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_DashboardPreferences.StatePressed.Border.ColorAngle = 135F;
            this.btn_DashboardPreferences.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DashboardPreferences.StatePressed.Border.Rounding = 5;
            this.btn_DashboardPreferences.StatePressed.Border.Width = 1;
            this.btn_DashboardPreferences.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_DashboardPreferences.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_DashboardPreferences.StateTracking.Back.ColorAngle = 45F;
            this.btn_DashboardPreferences.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(90)))));
            this.btn_DashboardPreferences.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btn_DashboardPreferences.StateTracking.Border.ColorAngle = 45F;
            this.btn_DashboardPreferences.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btn_DashboardPreferences.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btn_DashboardPreferences.StateTracking.Border.Rounding = 5;
            this.btn_DashboardPreferences.StateTracking.Border.Width = 1;
            this.btn_DashboardPreferences.TabIndex = 24;
            this.btn_DashboardPreferences.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton1.Values.Image")));
            this.btn_DashboardPreferences.Values.Text = "Dashboard Preferences";
            // 
            // Settings_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Settings_Form";
            this.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Settings_Form_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.side_panel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel side_panel;
        private System.Windows.Forms.Panel settings_main_panel_dock;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_LaundryUnit_Config;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_DataTimeConfig;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_BackUpConfig;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btn_DashboardPreferences;
    }
}