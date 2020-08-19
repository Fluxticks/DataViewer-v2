namespace DataViewer_v2
{
    partial class Start
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.btnStart_Start = new System.Windows.Forms.Button();
            this.btnSettings_Start = new System.Windows.Forms.Button();
            this.lblTitle_Start = new System.Windows.Forms.Label();
            this.lblVersion_Start = new System.Windows.Forms.Label();
            this.picIcon_Start = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon_Start)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart_Start
            // 
            this.btnStart_Start.Location = new System.Drawing.Point(80, 86);
            this.btnStart_Start.Name = "btnStart_Start";
            this.btnStart_Start.Size = new System.Drawing.Size(75, 23);
            this.btnStart_Start.TabIndex = 0;
            this.btnStart_Start.Text = "Start";
            this.btnStart_Start.UseVisualStyleBackColor = true;
            this.btnStart_Start.Click += new System.EventHandler(this.btnStart_Start_Click);
            // 
            // btnSettings_Start
            // 
            this.btnSettings_Start.Location = new System.Drawing.Point(195, 87);
            this.btnSettings_Start.Name = "btnSettings_Start";
            this.btnSettings_Start.Size = new System.Drawing.Size(75, 23);
            this.btnSettings_Start.TabIndex = 1;
            this.btnSettings_Start.Text = "Settings";
            this.btnSettings_Start.UseVisualStyleBackColor = true;
            this.btnSettings_Start.Click += new System.EventHandler(this.btnSettings_Start_Click);
            // 
            // lblTitle_Start
            // 
            this.lblTitle_Start.AutoSize = true;
            this.lblTitle_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle_Start.Location = new System.Drawing.Point(17, 5);
            this.lblTitle_Start.Name = "lblTitle_Start";
            this.lblTitle_Start.Size = new System.Drawing.Size(316, 66);
            this.lblTitle_Start.TabIndex = 2;
            this.lblTitle_Start.Text = "Grantham Gymnastics \r\nWaiting List Viewer";
            this.lblTitle_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion_Start
            // 
            this.lblVersion_Start.AutoSize = true;
            this.lblVersion_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblVersion_Start.Location = new System.Drawing.Point(135, 241);
            this.lblVersion_Start.Name = "lblVersion_Start";
            this.lblVersion_Start.Size = new System.Drawing.Size(23, 17);
            this.lblVersion_Start.TabIndex = 4;
            this.lblVersion_Start.Text = "v. ";
            this.lblVersion_Start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picIcon_Start
            // 
            this.picIcon_Start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picIcon_Start.Image = global::DataViewer_v2.Properties.Resources.granthamgymnasticsclub;
            this.picIcon_Start.Location = new System.Drawing.Point(125, 128);
            this.picIcon_Start.Name = "picIcon_Start";
            this.picIcon_Start.Size = new System.Drawing.Size(100, 100);
            this.picIcon_Start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon_Start.TabIndex = 3;
            this.picIcon_Start.TabStop = false;
            this.picIcon_Start.WaitOnLoad = true;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 271);
            this.Controls.Add(this.lblVersion_Start);
            this.Controls.Add(this.picIcon_Start);
            this.Controls.Add(this.lblTitle_Start);
            this.Controls.Add(this.btnSettings_Start);
            this.Controls.Add(this.btnStart_Start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start";
            ((System.ComponentModel.ISupportInitialize)(this.picIcon_Start)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart_Start;
        private System.Windows.Forms.Button btnSettings_Start;
        private System.Windows.Forms.Label lblTitle_Start;
        private System.Windows.Forms.PictureBox picIcon_Start;
        private System.Windows.Forms.Label lblVersion_Start;
    }
}