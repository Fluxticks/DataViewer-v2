namespace DataViewer_v2
{
    partial class DataViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataViewer));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAdd = new System.Windows.Forms.TabPage();
            this.txtDuplicate = new System.Windows.Forms.Label();
            this.duplicateCheck = new System.Windows.Forms.CheckBox();
            this.isArchived = new System.Windows.Forms.CheckBox();
            this.isContacted = new System.Windows.Forms.CheckBox();
            this.btnCalDropDown_Add = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.btnSpreadSheet_Add = new System.Windows.Forms.Button();
            this.btnExit_Add = new System.Windows.Forms.Button();
            this.btnConfirm_Add = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txtEmail_Add = new System.Windows.Forms.TextBox();
            this.txtPhone_Add = new System.Windows.Forms.TextBox();
            this.txtYear_Add = new System.Windows.Forms.TextBox();
            this.txtDOB_Add = new System.Windows.Forms.TextBox();
            this.txtName_Add = new System.Windows.Forms.TextBox();
            this.lblExtras_Add = new System.Windows.Forms.Label();
            this.lblEmail_Add = new System.Windows.Forms.Label();
            this.lblPhone_Add = new System.Windows.Forms.Label();
            this.lblYear_Add = new System.Windows.Forms.Label();
            this.lblDOB_Add = new System.Windows.Forms.Label();
            this.lblName_Add = new System.Windows.Forms.Label();
            this.lblTitle_Add = new System.Windows.Forms.Label();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.txtExtras_Search = new System.Windows.Forms.RichTextBox();
            this.lblExtras_Search = new System.Windows.Forms.Label();
            this.enableArchived = new System.Windows.Forms.CheckBox();
            this.enableContacted = new System.Windows.Forms.CheckBox();
            this.txtName_Search = new System.Windows.Forms.TextBox();
            this.lblName_Search = new System.Windows.Forms.Label();
            this.lblYear_Search = new System.Windows.Forms.Label();
            this.txtListDisplay = new System.Windows.Forms.TextBox();
            this.listCheck = new System.Windows.Forms.CheckedListBox();
            this.btnSearch_Search = new System.Windows.Forms.Button();
            this.dataSearchView = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabAdd.SuspendLayout();
            this.tabSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSearchView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAdd);
            this.tabControl1.Controls.Add(this.tabSearch);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 500);
            this.tabControl1.TabIndex = 1;
            // 
            // tabAdd
            // 
            this.tabAdd.Controls.Add(this.txtDuplicate);
            this.tabAdd.Controls.Add(this.duplicateCheck);
            this.tabAdd.Controls.Add(this.isArchived);
            this.tabAdd.Controls.Add(this.isContacted);
            this.tabAdd.Controls.Add(this.btnCalDropDown_Add);
            this.tabAdd.Controls.Add(this.monthCalendar1);
            this.tabAdd.Controls.Add(this.btnSpreadSheet_Add);
            this.tabAdd.Controls.Add(this.btnExit_Add);
            this.tabAdd.Controls.Add(this.btnConfirm_Add);
            this.tabAdd.Controls.Add(this.richTextBox1);
            this.tabAdd.Controls.Add(this.txtEmail_Add);
            this.tabAdd.Controls.Add(this.txtPhone_Add);
            this.tabAdd.Controls.Add(this.txtYear_Add);
            this.tabAdd.Controls.Add(this.txtDOB_Add);
            this.tabAdd.Controls.Add(this.txtName_Add);
            this.tabAdd.Controls.Add(this.lblExtras_Add);
            this.tabAdd.Controls.Add(this.lblEmail_Add);
            this.tabAdd.Controls.Add(this.lblPhone_Add);
            this.tabAdd.Controls.Add(this.lblYear_Add);
            this.tabAdd.Controls.Add(this.lblDOB_Add);
            this.tabAdd.Controls.Add(this.lblName_Add);
            this.tabAdd.Controls.Add(this.lblTitle_Add);
            this.tabAdd.Location = new System.Drawing.Point(4, 22);
            this.tabAdd.Name = "tabAdd";
            this.tabAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdd.Size = new System.Drawing.Size(486, 474);
            this.tabAdd.TabIndex = 0;
            this.tabAdd.Text = "Add Child";
            this.tabAdd.UseVisualStyleBackColor = true;
            // 
            // txtDuplicate
            // 
            this.txtDuplicate.AutoSize = true;
            this.txtDuplicate.ForeColor = System.Drawing.Color.DarkOrange;
            this.txtDuplicate.Location = new System.Drawing.Point(252, 61);
            this.txtDuplicate.Name = "txtDuplicate";
            this.txtDuplicate.Size = new System.Drawing.Size(102, 13);
            this.txtDuplicate.TabIndex = 52;
            this.txtDuplicate.Text = "Duplicate Detected!";
            this.txtDuplicate.Visible = false;
            // 
            // duplicateCheck
            // 
            this.duplicateCheck.AutoSize = true;
            this.duplicateCheck.Checked = true;
            this.duplicateCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.duplicateCheck.Location = new System.Drawing.Point(235, 447);
            this.duplicateCheck.Name = "duplicateCheck";
            this.duplicateCheck.Size = new System.Drawing.Size(134, 17);
            this.duplicateCheck.TabIndex = 9;
            this.duplicateCheck.Text = "Check For Duplicates?";
            this.duplicateCheck.UseVisualStyleBackColor = true;
            // 
            // isArchived
            // 
            this.isArchived.AutoSize = true;
            this.isArchived.Location = new System.Drawing.Point(125, 283);
            this.isArchived.Name = "isArchived";
            this.isArchived.Size = new System.Drawing.Size(74, 17);
            this.isArchived.TabIndex = 6;
            this.isArchived.Text = "Archived?";
            this.isArchived.UseVisualStyleBackColor = true;
            // 
            // isContacted
            // 
            this.isContacted.AutoSize = true;
            this.isContacted.Location = new System.Drawing.Point(125, 260);
            this.isContacted.Name = "isContacted";
            this.isContacted.Size = new System.Drawing.Size(81, 17);
            this.isContacted.TabIndex = 5;
            this.isContacted.Text = "Contacted?";
            this.isContacted.UseVisualStyleBackColor = true;
            // 
            // btnCalDropDown_Add
            // 
            this.btnCalDropDown_Add.Location = new System.Drawing.Point(224, 99);
            this.btnCalDropDown_Add.Name = "btnCalDropDown_Add";
            this.btnCalDropDown_Add.Size = new System.Drawing.Size(22, 22);
            this.btnCalDropDown_Add.TabIndex = 17;
            this.btnCalDropDown_Add.Text = "v";
            this.btnCalDropDown_Add.UseVisualStyleBackColor = true;
            this.btnCalDropDown_Add.Click += new System.EventHandler(this.BtnCalDropDown_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(255, 99);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 16;
            this.monthCalendar1.Visible = false;
            // 
            // btnSpreadSheet_Add
            // 
            this.btnSpreadSheet_Add.Location = new System.Drawing.Point(375, 14);
            this.btnSpreadSheet_Add.Name = "btnSpreadSheet_Add";
            this.btnSpreadSheet_Add.Size = new System.Drawing.Size(75, 45);
            this.btnSpreadSheet_Add.TabIndex = 10;
            this.btnSpreadSheet_Add.Text = "Set Working Spreadsheet";
            this.btnSpreadSheet_Add.UseVisualStyleBackColor = true;
            this.btnSpreadSheet_Add.Click += new System.EventHandler(this.BtnSpreadSheet_Click);
            // 
            // btnExit_Add
            // 
            this.btnExit_Add.Location = new System.Drawing.Point(23, 443);
            this.btnExit_Add.Name = "btnExit_Add";
            this.btnExit_Add.Size = new System.Drawing.Size(75, 23);
            this.btnExit_Add.TabIndex = 50;
            this.btnExit_Add.TabStop = false;
            this.btnExit_Add.Text = "Exit";
            this.btnExit_Add.UseVisualStyleBackColor = true;
            this.btnExit_Add.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnConfirm_Add
            // 
            this.btnConfirm_Add.Location = new System.Drawing.Point(375, 443);
            this.btnConfirm_Add.Name = "btnConfirm_Add";
            this.btnConfirm_Add.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm_Add.TabIndex = 8;
            this.btnConfirm_Add.Text = "Confirm";
            this.btnConfirm_Add.UseVisualStyleBackColor = true;
            this.btnConfirm_Add.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(140, 320);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(310, 100);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // txtEmail_Add
            // 
            this.txtEmail_Add.Location = new System.Drawing.Point(125, 226);
            this.txtEmail_Add.Name = "txtEmail_Add";
            this.txtEmail_Add.Size = new System.Drawing.Size(121, 20);
            this.txtEmail_Add.TabIndex = 4;
            // 
            // txtPhone_Add
            // 
            this.txtPhone_Add.Location = new System.Drawing.Point(125, 184);
            this.txtPhone_Add.MaxLength = 13;
            this.txtPhone_Add.Name = "txtPhone_Add";
            this.txtPhone_Add.Size = new System.Drawing.Size(121, 20);
            this.txtPhone_Add.TabIndex = 3;
            // 
            // txtYear_Add
            // 
            this.txtYear_Add.Location = new System.Drawing.Point(125, 142);
            this.txtYear_Add.Name = "txtYear_Add";
            this.txtYear_Add.Size = new System.Drawing.Size(121, 20);
            this.txtYear_Add.TabIndex = 2;
            // 
            // txtDOB_Add
            // 
            this.txtDOB_Add.Location = new System.Drawing.Point(125, 100);
            this.txtDOB_Add.Name = "txtDOB_Add";
            this.txtDOB_Add.Size = new System.Drawing.Size(121, 20);
            this.txtDOB_Add.TabIndex = 1;
            // 
            // txtName_Add
            // 
            this.txtName_Add.Location = new System.Drawing.Point(125, 58);
            this.txtName_Add.MaxLength = 50;
            this.txtName_Add.Name = "txtName_Add";
            this.txtName_Add.Size = new System.Drawing.Size(121, 20);
            this.txtName_Add.TabIndex = 0;
            // 
            // lblExtras_Add
            // 
            this.lblExtras_Add.AutoSize = true;
            this.lblExtras_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblExtras_Add.Location = new System.Drawing.Point(28, 320);
            this.lblExtras_Add.Name = "lblExtras_Add";
            this.lblExtras_Add.Size = new System.Drawing.Size(91, 17);
            this.lblExtras_Add.TabIndex = 6;
            this.lblExtras_Add.Text = "More Details:";
            // 
            // lblEmail_Add
            // 
            this.lblEmail_Add.AutoSize = true;
            this.lblEmail_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblEmail_Add.Location = new System.Drawing.Point(73, 228);
            this.lblEmail_Add.Name = "lblEmail_Add";
            this.lblEmail_Add.Size = new System.Drawing.Size(46, 17);
            this.lblEmail_Add.TabIndex = 5;
            this.lblEmail_Add.Text = "Email:";
            // 
            // lblPhone_Add
            // 
            this.lblPhone_Add.AutoSize = true;
            this.lblPhone_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPhone_Add.Location = new System.Drawing.Point(9, 186);
            this.lblPhone_Add.Name = "lblPhone_Add";
            this.lblPhone_Add.Size = new System.Drawing.Size(106, 17);
            this.lblPhone_Add.TabIndex = 4;
            this.lblPhone_Add.Text = "Telephone No.:";
            // 
            // lblYear_Add
            // 
            this.lblYear_Add.AutoSize = true;
            this.lblYear_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblYear_Add.Location = new System.Drawing.Point(30, 144);
            this.lblYear_Add.Name = "lblYear_Add";
            this.lblYear_Add.Size = new System.Drawing.Size(89, 17);
            this.lblYear_Add.TabIndex = 3;
            this.lblYear_Add.Text = "School Year:";
            // 
            // lblDOB_Add
            // 
            this.lblDOB_Add.AutoSize = true;
            this.lblDOB_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDOB_Add.Location = new System.Drawing.Point(77, 102);
            this.lblDOB_Add.Name = "lblDOB_Add";
            this.lblDOB_Add.Size = new System.Drawing.Size(42, 17);
            this.lblDOB_Add.TabIndex = 2;
            this.lblDOB_Add.Text = "DOB:";
            // 
            // lblName_Add
            // 
            this.lblName_Add.AutoSize = true;
            this.lblName_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblName_Add.Location = new System.Drawing.Point(70, 60);
            this.lblName_Add.Name = "lblName_Add";
            this.lblName_Add.Size = new System.Drawing.Size(49, 17);
            this.lblName_Add.TabIndex = 1;
            this.lblName_Add.Text = "Name:";
            // 
            // lblTitle_Add
            // 
            this.lblTitle_Add.AutoSize = true;
            this.lblTitle_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblTitle_Add.Location = new System.Drawing.Point(18, 14);
            this.lblTitle_Add.Name = "lblTitle_Add";
            this.lblTitle_Add.Size = new System.Drawing.Size(112, 25);
            this.lblTitle_Add.TabIndex = 0;
            this.lblTitle_Add.Text = "Add To List";
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.txtExtras_Search);
            this.tabSearch.Controls.Add(this.lblExtras_Search);
            this.tabSearch.Controls.Add(this.enableArchived);
            this.tabSearch.Controls.Add(this.enableContacted);
            this.tabSearch.Controls.Add(this.txtName_Search);
            this.tabSearch.Controls.Add(this.lblName_Search);
            this.tabSearch.Controls.Add(this.lblYear_Search);
            this.tabSearch.Controls.Add(this.txtListDisplay);
            this.tabSearch.Controls.Add(this.listCheck);
            this.tabSearch.Controls.Add(this.btnSearch_Search);
            this.tabSearch.Controls.Add(this.dataSearchView);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearch.Size = new System.Drawing.Size(486, 474);
            this.tabSearch.TabIndex = 1;
            this.tabSearch.Text = "Search/Filter";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // txtExtras_Search
            // 
            this.txtExtras_Search.Location = new System.Drawing.Point(531, 6);
            this.txtExtras_Search.Name = "txtExtras_Search";
            this.txtExtras_Search.Size = new System.Drawing.Size(186, 88);
            this.txtExtras_Search.TabIndex = 10;
            this.txtExtras_Search.Text = "";
            // 
            // lblExtras_Search
            // 
            this.lblExtras_Search.AutoSize = true;
            this.lblExtras_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblExtras_Search.Location = new System.Drawing.Point(476, 9);
            this.lblExtras_Search.Name = "lblExtras_Search";
            this.lblExtras_Search.Size = new System.Drawing.Size(51, 17);
            this.lblExtras_Search.TabIndex = 9;
            this.lblExtras_Search.Text = "Extras:";
            // 
            // enableArchived
            // 
            this.enableArchived.AutoSize = true;
            this.enableArchived.Location = new System.Drawing.Point(63, 70);
            this.enableArchived.Name = "enableArchived";
            this.enableArchived.Size = new System.Drawing.Size(98, 17);
            this.enableArchived.TabIndex = 2;
            this.enableArchived.Text = "Show Archived";
            this.enableArchived.UseVisualStyleBackColor = true;
            // 
            // enableContacted
            // 
            this.enableContacted.AutoSize = true;
            this.enableContacted.Checked = true;
            this.enableContacted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableContacted.Location = new System.Drawing.Point(63, 47);
            this.enableContacted.Name = "enableContacted";
            this.enableContacted.Size = new System.Drawing.Size(105, 17);
            this.enableContacted.TabIndex = 1;
            this.enableContacted.Text = "Show Contacted";
            this.enableContacted.UseVisualStyleBackColor = true;
            // 
            // txtName_Search
            // 
            this.txtName_Search.Location = new System.Drawing.Point(63, 9);
            this.txtName_Search.MaxLength = 100;
            this.txtName_Search.Name = "txtName_Search";
            this.txtName_Search.Size = new System.Drawing.Size(88, 20);
            this.txtName_Search.TabIndex = 0;
            // 
            // lblName_Search
            // 
            this.lblName_Search.AutoSize = true;
            this.lblName_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblName_Search.Location = new System.Drawing.Point(8, 9);
            this.lblName_Search.Name = "lblName_Search";
            this.lblName_Search.Size = new System.Drawing.Size(49, 17);
            this.lblName_Search.TabIndex = 5;
            this.lblName_Search.Text = "Name:";
            // 
            // lblYear_Search
            // 
            this.lblYear_Search.AutoSize = true;
            this.lblYear_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblYear_Search.Location = new System.Drawing.Point(157, 9);
            this.lblYear_Search.Name = "lblYear_Search";
            this.lblYear_Search.Size = new System.Drawing.Size(93, 17);
            this.lblYear_Search.TabIndex = 4;
            this.lblYear_Search.Text = "Year Groups:";
            // 
            // txtListDisplay
            // 
            this.txtListDisplay.Location = new System.Drawing.Point(256, 6);
            this.txtListDisplay.Name = "txtListDisplay";
            this.txtListDisplay.Size = new System.Drawing.Size(210, 20);
            this.txtListDisplay.TabIndex = 3;
            this.txtListDisplay.Text = "Click to add year groups";
            // 
            // listCheck
            // 
            this.listCheck.CheckOnClick = true;
            this.listCheck.FormattingEnabled = true;
            this.listCheck.Location = new System.Drawing.Point(256, 30);
            this.listCheck.Name = "listCheck";
            this.listCheck.Size = new System.Drawing.Size(129, 64);
            this.listCheck.TabIndex = 4;
            this.listCheck.Visible = false;
            // 
            // btnSearch_Search
            // 
            this.btnSearch_Search.Location = new System.Drawing.Point(391, 70);
            this.btnSearch_Search.Name = "btnSearch_Search";
            this.btnSearch_Search.Size = new System.Drawing.Size(75, 23);
            this.btnSearch_Search.TabIndex = 5;
            this.btnSearch_Search.Text = "Search";
            this.btnSearch_Search.UseVisualStyleBackColor = true;
            this.btnSearch_Search.Click += new System.EventHandler(this.BtnSearch_Search_Click);
            // 
            // dataSearchView
            // 
            this.dataSearchView.AllowUserToAddRows = false;
            this.dataSearchView.AllowUserToDeleteRows = false;
            this.dataSearchView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataSearchView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataSearchView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataSearchView.Location = new System.Drawing.Point(0, 100);
            this.dataSearchView.MultiSelect = false;
            this.dataSearchView.Name = "dataSearchView";
            this.dataSearchView.ReadOnly = true;
            this.dataSearchView.Size = new System.Drawing.Size(470, 320);
            this.dataSearchView.TabIndex = 0;
            // 
            // DataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 499);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataViewer";
            this.Text = "Data Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabAdd.ResumeLayout(false);
            this.tabAdd.PerformLayout();
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSearchView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAdd;
        private System.Windows.Forms.CheckBox isArchived;
        private System.Windows.Forms.CheckBox isContacted;
        private System.Windows.Forms.Button btnCalDropDown_Add;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button btnSpreadSheet_Add;
        private System.Windows.Forms.Button btnExit_Add;
        private System.Windows.Forms.Button btnConfirm_Add;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtEmail_Add;
        private System.Windows.Forms.TextBox txtPhone_Add;
        private System.Windows.Forms.TextBox txtYear_Add;
        private System.Windows.Forms.TextBox txtDOB_Add;
        private System.Windows.Forms.TextBox txtName_Add;
        private System.Windows.Forms.Label lblExtras_Add;
        private System.Windows.Forms.Label lblEmail_Add;
        private System.Windows.Forms.Label lblPhone_Add;
        private System.Windows.Forms.Label lblYear_Add;
        private System.Windows.Forms.Label lblDOB_Add;
        private System.Windows.Forms.Label lblName_Add;
        private System.Windows.Forms.Label lblTitle_Add;
        private System.Windows.Forms.TabPage tabSearch;
        private System.Windows.Forms.CheckBox enableArchived;
        private System.Windows.Forms.CheckBox enableContacted;
        private System.Windows.Forms.TextBox txtName_Search;
        private System.Windows.Forms.Label lblName_Search;
        private System.Windows.Forms.Label lblYear_Search;
        private System.Windows.Forms.TextBox txtListDisplay;
        private System.Windows.Forms.CheckedListBox listCheck;
        private System.Windows.Forms.Button btnSearch_Search;
        private System.Windows.Forms.DataGridView dataSearchView;
        private System.Windows.Forms.CheckBox duplicateCheck;
        private System.Windows.Forms.Label txtDuplicate;
        private System.Windows.Forms.RichTextBox txtExtras_Search;
        private System.Windows.Forms.Label lblExtras_Search;
    }
}

