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
            this.btn_ClickDeliveryList = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grid_delivery_view = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cancel = new System.Windows.Forms.DataGridViewImageColumn();
            this.Complete = new System.Windows.Forms.DataGridViewImageColumn();
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
            this.panel1.Controls.Add(this.btn_ClickDeliveryList);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 45);
            this.panel1.TabIndex = 2;
            // 
            // btn_ClickDeliveryList
            // 
            this.btn_ClickDeliveryList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ClickDeliveryList.AutoSize = true;
            this.btn_ClickDeliveryList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClickDeliveryList.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ClickDeliveryList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(138)))), ((int)(((byte)(97)))));
            this.btn_ClickDeliveryList.Location = new System.Drawing.Point(550, 10);
            this.btn_ClickDeliveryList.Name = "btn_ClickDeliveryList";
            this.btn_ClickDeliveryList.Size = new System.Drawing.Size(213, 21);
            this.btn_ClickDeliveryList.TabIndex = 20;
            this.btn_ClickDeliveryList.Text = " Click to view all delivery list";
            this.btn_ClickDeliveryList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_ClickDeliveryList.Click += new System.EventHandler(this.btn_ClickDeliveryList_Click);
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
            this.panel2.Size = new System.Drawing.Size(766, 371);
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
            this.Cancel,
            this.Complete});
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
            this.grid_delivery_view.Size = new System.Drawing.Size(766, 371);
            this.grid_delivery_view.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.grid_delivery_view.StateCommon.DataCell.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.grid_delivery_view.StateCommon.DataCell.Content.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.grid_delivery_view.StateCommon.DataCell.Content.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid_delivery_view.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.DarkSlateGray;
            this.grid_delivery_view.StateCommon.HeaderColumn.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(110)))), ((int)(((byte)(121)))));
            this.grid_delivery_view.StateCommon.HeaderColumn.Back.ColorAngle = 45F;
            this.grid_delivery_view.StateCommon.HeaderColumn.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.HalfCut;
            this.grid_delivery_view.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.White;
            this.grid_delivery_view.StateCommon.HeaderColumn.Content.Color2 = System.Drawing.Color.White;
            this.grid_delivery_view.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid_delivery_view.TabIndex = 9;
            this.grid_delivery_view.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_delivery_view_CellContentClick);
            this.grid_delivery_view.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grid_delivery_view_CellFormatting);
            this.grid_delivery_view.SelectionChanged += new System.EventHandler(this.grid_delivery_view_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.FillWeight = 27.84934F;
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
            this.dataGridViewTextBoxColumn9.FillWeight = 134.2327F;
            this.dataGridViewTextBoxColumn9.HeaderText = "Customer Name";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 141.5388F;
            this.Column1.HeaderText = "Delivery Address";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 103.1457F;
            this.Column2.HeaderText = " Total Amount";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 60.36311F;
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
            // Complete
            // 
            this.Complete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Complete.HeaderText = " Complete";
            this.Complete.Image = ((System.Drawing.Image)(resources.GetObject("Complete.Image")));
            this.Complete.Name = "Complete";
            this.Complete.ReadOnly = true;
            // 
            // Delivery_Widget_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(776, 426);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid_delivery_view;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn Cancel;
        private System.Windows.Forms.DataGridViewImageColumn Complete;
        private System.Windows.Forms.Label btn_ClickDeliveryList;
    }
}