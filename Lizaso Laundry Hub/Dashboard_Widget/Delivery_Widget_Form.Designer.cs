namespace Lizaso_Laundry_Hub.Dashboard_Widget
{
    partial class Delivery_Widget_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Delivery_Widget_Form));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label_LastBackUpInfo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grid_delivery_view = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ckCancel = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckInTransit = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckCompleted = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cancel = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_delivery_view)).BeginInit();
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
            // panel1
            // 
            this.panel1.Controls.Add(this.ckCompleted);
            this.panel1.Controls.Add(this.ckInTransit);
            this.panel1.Controls.Add(this.ckCancel);
            this.panel1.Controls.Add(this.Label_LastBackUpInfo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 45);
            this.panel1.TabIndex = 2;
            // 
            // Label_LastBackUpInfo
            // 
            this.Label_LastBackUpInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_LastBackUpInfo.AutoSize = true;
            this.Label_LastBackUpInfo.Font = new System.Drawing.Font("Poppins SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_LastBackUpInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(35)))), ((int)(((byte)(30)))));
            this.Label_LastBackUpInfo.Location = new System.Drawing.Point(262, 9);
            this.Label_LastBackUpInfo.Name = "Label_LastBackUpInfo";
            this.Label_LastBackUpInfo.Size = new System.Drawing.Size(22, 28);
            this.Label_LastBackUpInfo.TabIndex = 6;
            this.Label_LastBackUpInfo.Text = "0";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Poppins SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.label5.Location = new System.Drawing.Point(19, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 28);
            this.label5.TabIndex = 11;
            this.label5.Text = "Delivery Status Summary";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grid_delivery_view);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(782, 387);
            this.panel2.TabIndex = 3;
            // 
            // grid_delivery_view
            // 
            this.grid_delivery_view.AllowUserToAddRows = false;
            this.grid_delivery_view.AllowUserToDeleteRows = false;
            this.grid_delivery_view.AllowUserToResizeColumns = false;
            this.grid_delivery_view.AllowUserToResizeRows = false;
            this.grid_delivery_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid_delivery_view.ColumnHeadersHeight = 40;
            this.grid_delivery_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid_delivery_view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Cancel});
            this.grid_delivery_view.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grid_delivery_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid_delivery_view.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.grid_delivery_view.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.grid_delivery_view.Location = new System.Drawing.Point(0, 0);
            this.grid_delivery_view.Name = "grid_delivery_view";
            this.grid_delivery_view.ReadOnly = true;
            this.grid_delivery_view.RowHeadersVisible = false;
            this.grid_delivery_view.RowHeadersWidth = 50;
            this.grid_delivery_view.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid_delivery_view.RowTemplate.Height = 50;
            this.grid_delivery_view.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_delivery_view.Size = new System.Drawing.Size(782, 387);
            this.grid_delivery_view.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.grid_delivery_view.StateCommon.DataCell.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.grid_delivery_view.StateCommon.DataCell.Content.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.grid_delivery_view.StateCommon.DataCell.Content.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid_delivery_view.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.grid_delivery_view.StateCommon.HeaderColumn.Content.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.grid_delivery_view.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid_delivery_view.TabIndex = 9;
            this.grid_delivery_view.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_delivery_view_CellContentClick);
            this.grid_delivery_view.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grid_delivery_view_CellFormatting);
            this.grid_delivery_view.SelectionChanged += new System.EventHandler(this.grid_delivery_view_SelectionChanged);
            // 
            // ckCancel
            // 
            this.ckCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckCancel.Location = new System.Drawing.Point(674, 9);
            this.ckCancel.Name = "ckCancel";
            this.ckCancel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.ckCancel.Size = new System.Drawing.Size(105, 27);
            this.ckCancel.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.ckCancel.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.ckCancel.StateCommon.ShortText.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckCancel.TabIndex = 80;
            this.ckCancel.Values.Text = "Canceled";
            this.ckCancel.CheckedChanged += new System.EventHandler(this.ckCancel_CheckedChanged);
            // 
            // ckInTransit
            // 
            this.ckInTransit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckInTransit.Location = new System.Drawing.Point(445, 9);
            this.ckInTransit.Name = "ckInTransit";
            this.ckInTransit.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.ckInTransit.Size = new System.Drawing.Size(99, 27);
            this.ckInTransit.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.ckInTransit.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.ckInTransit.StateCommon.ShortText.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckInTransit.TabIndex = 81;
            this.ckInTransit.Values.Text = "In Transit";
            this.ckInTransit.CheckedChanged += new System.EventHandler(this.ckInTransit_CheckedChanged);
            // 
            // ckCompleted
            // 
            this.ckCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckCompleted.Location = new System.Drawing.Point(550, 9);
            this.ckCompleted.Name = "ckCompleted";
            this.ckCompleted.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.ckCompleted.Size = new System.Drawing.Size(118, 27);
            this.ckCompleted.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.ckCompleted.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.ckCompleted.StateCommon.ShortText.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckCompleted.TabIndex = 82;
            this.ckCompleted.Values.Text = "Completed";
            this.ckCompleted.CheckedChanged += new System.EventHandler(this.ckCompleted_CheckedChanged);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.FillWeight = 22.24427F;
            this.dataGridViewTextBoxColumn6.HeaderText = "#";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Delivery_ID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.FillWeight = 67.12963F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Transaction_ID";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn9.FillWeight = 127.5247F;
            this.dataGridViewTextBoxColumn9.HeaderText = "Customer Name";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 186.7603F;
            this.Column1.HeaderText = "Delivery Address";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 82.38619F;
            this.Column2.HeaderText = " Total Amount";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 48.21418F;
            this.Column3.HeaderText = " Status";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Cancel
            // 
            this.Cancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Cancel.HeaderText = " Cancel";
            this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
            this.Cancel.MinimumWidth = 50;
            this.Cancel.Name = "Cancel";
            this.Cancel.ReadOnly = true;
            this.Cancel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Cancel.Width = 80;
            // 
            // Delivery_Widget_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 442);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Delivery_Widget_Form";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Delivery_Widget_Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_delivery_view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label_LastBackUpInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_delivery_view;
        public ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckCancel;
        public ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckCompleted;
        public ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckInTransit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn Cancel;
    }
}