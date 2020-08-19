namespace DataViewer_v2
{
    partial class Settings
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
            System.Windows.Forms.Label lblName_Setting;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.dataHeaders_Setting = new System.Windows.Forms.DataGridView();
            this.txtFileName_Setting = new System.Windows.Forms.TextBox();
            this.numFiles_Setting = new System.Windows.Forms.NumericUpDown();
            this.lblTitle_Setting = new System.Windows.Forms.Label();
            this.lblHeaders_Setting = new System.Windows.Forms.Label();
            this.lblFiles_Setting = new System.Windows.Forms.Label();
            this.btnBack_Setting = new System.Windows.Forms.Button();
            this.btnSave_Setting = new System.Windows.Forms.Button();
            this.colHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblName_Setting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataHeaders_Setting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFiles_Setting)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName_Setting
            // 
            lblName_Setting.AutoSize = true;
            lblName_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            lblName_Setting.Location = new System.Drawing.Point(103, 86);
            lblName_Setting.Name = "lblName_Setting";
            lblName_Setting.Size = new System.Drawing.Size(95, 17);
            lblName_Setting.TabIndex = 8;
            lblName_Setting.Text = "Name of File :";
            // 
            // dataHeaders_Setting
            // 
            this.dataHeaders_Setting.AllowDrop = true;
            this.dataHeaders_Setting.AllowUserToAddRows = false;
            this.dataHeaders_Setting.AllowUserToDeleteRows = false;
            this.dataHeaders_Setting.AllowUserToResizeRows = false;
            this.dataHeaders_Setting.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataHeaders_Setting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataHeaders_Setting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHeader,
            this.colIndex});
            this.dataHeaders_Setting.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataHeaders_Setting.Location = new System.Drawing.Point(161, 126);
            this.dataHeaders_Setting.Name = "dataHeaders_Setting";
            this.dataHeaders_Setting.Size = new System.Drawing.Size(302, 243);
            this.dataHeaders_Setting.TabIndex = 12;
            this.dataHeaders_Setting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataHeaders_Settings_EditingControlShowing);
            this.dataHeaders_Setting.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataHeaders_Setting_EndEdit);
            this.dataHeaders_Setting.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataHeaders_Settings_CellBeginEdit);
            // 
            // txtFileName_Setting
            // 
            this.txtFileName_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFileName_Setting.Location = new System.Drawing.Point(207, 85);
            this.txtFileName_Setting.Name = "txtFileName_Setting";
            this.txtFileName_Setting.Size = new System.Drawing.Size(100, 23);
            this.txtFileName_Setting.TabIndex = 11;
            this.txtFileName_Setting.Text = "gymnastics";
            // 
            // numFiles_Setting
            // 
            this.numFiles_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numFiles_Setting.Location = new System.Drawing.Point(207, 44);
            this.numFiles_Setting.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numFiles_Setting.Name = "numFiles_Setting";
            this.numFiles_Setting.Size = new System.Drawing.Size(100, 23);
            this.numFiles_Setting.TabIndex = 10;
            this.numFiles_Setting.ThousandsSeparator = true;
            this.numFiles_Setting.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblTitle_Setting
            // 
            this.lblTitle_Setting.AutoSize = true;
            this.lblTitle_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblTitle_Setting.Location = new System.Drawing.Point(12, 9);
            this.lblTitle_Setting.Name = "lblTitle_Setting";
            this.lblTitle_Setting.Size = new System.Drawing.Size(83, 25);
            this.lblTitle_Setting.TabIndex = 9;
            this.lblTitle_Setting.Text = "Settings";
            // 
            // lblHeaders_Setting
            // 
            this.lblHeaders_Setting.AutoSize = true;
            this.lblHeaders_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblHeaders_Setting.Location = new System.Drawing.Point(32, 126);
            this.lblHeaders_Setting.Name = "lblHeaders_Setting";
            this.lblHeaders_Setting.Size = new System.Drawing.Size(123, 17);
            this.lblHeaders_Setting.TabIndex = 7;
            this.lblHeaders_Setting.Text = "Order of Headers:";
            // 
            // lblFiles_Setting
            // 
            this.lblFiles_Setting.AutoSize = true;
            this.lblFiles_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFiles_Setting.Location = new System.Drawing.Point(32, 46);
            this.lblFiles_Setting.Name = "lblFiles_Setting";
            this.lblFiles_Setting.Size = new System.Drawing.Size(168, 17);
            this.lblFiles_Setting.TabIndex = 6;
            this.lblFiles_Setting.Text = "Number of Files to Keep :";
            // 
            // btnBack_Setting
            // 
            this.btnBack_Setting.Location = new System.Drawing.Point(12, 383);
            this.btnBack_Setting.Name = "btnBack_Setting";
            this.btnBack_Setting.Size = new System.Drawing.Size(75, 23);
            this.btnBack_Setting.TabIndex = 13;
            this.btnBack_Setting.Text = "Back";
            this.btnBack_Setting.UseVisualStyleBackColor = true;
            this.btnBack_Setting.Click += new System.EventHandler(this.btnBack_Setting_Click);
            // 
            // btnSave_Setting
            // 
            this.btnSave_Setting.Location = new System.Drawing.Point(388, 383);
            this.btnSave_Setting.Name = "btnSave_Setting";
            this.btnSave_Setting.Size = new System.Drawing.Size(75, 23);
            this.btnSave_Setting.TabIndex = 14;
            this.btnSave_Setting.Text = "Save";
            this.btnSave_Setting.UseVisualStyleBackColor = true;
            this.btnSave_Setting.Click += new System.EventHandler(this.btnSave_Setting_Click);
            // 
            // colHeader
            // 
            this.colHeader.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHeader.HeaderText = "Header";
            this.colHeader.Name = "colHeader";
            this.colHeader.ReadOnly = true;
            // 
            // colIndex
            // 
            this.colIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colIndex.HeaderText = "Index";
            this.colIndex.Name = "colIndex";
            this.colIndex.Width = 58;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 418);
            this.Controls.Add(this.btnSave_Setting);
            this.Controls.Add(this.btnBack_Setting);
            this.Controls.Add(this.dataHeaders_Setting);
            this.Controls.Add(this.txtFileName_Setting);
            this.Controls.Add(this.numFiles_Setting);
            this.Controls.Add(this.lblTitle_Setting);
            this.Controls.Add(this.lblHeaders_Setting);
            this.Controls.Add(lblName_Setting);
            this.Controls.Add(this.lblFiles_Setting);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.dataHeaders_Setting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFiles_Setting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataHeaders_Setting;
        private System.Windows.Forms.TextBox txtFileName_Setting;
        private System.Windows.Forms.NumericUpDown numFiles_Setting;
        private System.Windows.Forms.Label lblTitle_Setting;
        private System.Windows.Forms.Label lblHeaders_Setting;
        private System.Windows.Forms.Label lblFiles_Setting;
        private System.Windows.Forms.Button btnBack_Setting;
        private System.Windows.Forms.Button btnSave_Setting;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
    }
}