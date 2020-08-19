using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataViewer_v2
{
    public partial class DataViewer : Form
    {
        private TextBox[] elements;
        private String workingFile = String.Empty;
        private DataTable data;
        private String[] headers = { "Hash","Date Added", "Name", "DOB", "SchoolYr", "Phone Number", "Email", "More Details", "Contacted", "Archived" };
        private String[] years = { "Reception", "Year 1", "Year 2", "Year 3", "Year 4", "Year 5", "Year 6", "Year 7", "Year 8", "Year 9", "Year 10", "Year 11", "Year 12", "Year 13", "Mature" };
        private Boolean firstEdit = true;
        private String rowHash;
        private String date;

        //Config Vars
        private string fileNameFormat;
        private int filesToKeep;



        public DataViewer()
        {
            InitializeComponent();
            this.txtName_Add.KeyPress += new KeyPressEventHandler(this.checkKeyPress);
            this.txtName_Search.KeyPress += new KeyPressEventHandler(this.checkKeyPress);
            this.txtPhone_Add.KeyPress += new KeyPressEventHandler(this.checkKeyPress);
            this.monthCalendar1.DateChanged += new DateRangeEventHandler(this.calendarChanged);
            this.txtListDisplay.MouseClick += new MouseEventHandler(this.openYearGroupPicker);
            this.dataSearchView.CellDoubleClick += new DataGridViewCellEventHandler(this.doubleClick);
            this.dataSearchView.DataSourceChanged += new EventHandler(this.updateHandler);
            this.listCheck.Items.AddRange(years);
            this.txtDOB_Add.LostFocus += new EventHandler(this.exitDOB);
            this.listCheck.ItemCheck += new ItemCheckEventHandler(this.updateSelectedYears);
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabChangeHandler);
            this.dataSearchView.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.orderingChange);
            this.txtName_Add.LostFocus += new EventHandler(this.removeSpaces);

            elements = new TextBox[] { txtName_Add, txtDOB_Add, txtYear_Add, txtPhone_Add, txtEmail_Add };
            foreach(TextBox item in elements)
            {
                item.Enter += new EventHandler(this.enterTextBox);
            }
            
        }

        #region Handlers
        private void checkKeyPress(object sender, KeyPressEventArgs e)
        {
            //Regex re = new Regex(@"^[a-zA-Z]+$");
            Regex re = null;

            TextBox box = (TextBox)sender;

            if(box == txtName_Add || box == txtName_Search)
            {
                re = new Regex("^[/|\\\\|'|\"|%|!|~|&|,|(|)|{|}|\\[|\\].|\\*|\\^|\\$|<|>|\\?|\\+|=|_|#|@|;|:|`]$");
            }
            else if(box == txtPhone_Add)
            {
                re = new Regex("[^0-9\b]$");
            }

            if (re.IsMatch(e.KeyChar.ToString()))
            {
                //MessageBox.Show("yes");
                e.Handled = true;
            }
        }
        private void calendarChanged(object sender, DateRangeEventArgs e)
        {
            MonthCalendar cal = (MonthCalendar)sender;
            String date = cal.SelectionStart.ToString("dd/MM/yyyy");
            txtDOB_Add.Text = date;
            updateYear();
        }
        private void enterTextBox(object sender, EventArgs e)
        {
            TextBox current = (TextBox)sender;
            current.BackColor = Color.White;
            String tmpsql = "";
            int counter = 0;
            if (duplicateCheck.Checked)
            {
                foreach (TextBox box in elements)
                {
                    if (!(box.Text.Trim().Equals("")))
                    {
                        if (tmpsql.Equals(""))
                        {
                            tmpsql = headers[counter + 2] + " = '" + box.Text.Trim() + "'";
                        }
                        else
                        {
                            tmpsql += " and '" + headers[counter + 2] + "' = '" + box.Text.Trim() + "'";
                        }
                    }
                    counter++;
                }
                if (!tmpsql.Equals(""))
                {
                    txtDuplicate.Visible = (data.Select(tmpsql).Length > 0) ? true : false;
                }
                else
                {
                    txtDuplicate.Visible = false;
                }
            }
            
        }
        private void openYearGroupPicker(object sender, MouseEventArgs e)
        {
            listCheck.Visible = !listCheck.Visible;
        }
        private void updateSelectedYears(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                String items = "";

                if (listCheck.CheckedItems.Count != 0)
                {
                    foreach (String item in listCheck.CheckedItems)
                    {
                        items += item + ", ";
                    }
                    items = items.Substring(0, items.LastIndexOf(","));

                }
                else
                {
                    items = "Click to add year groups";
                }
                txtListDisplay.Text = items;
            }));
        }
        private void doubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataViewer f2 = new DataViewer();
            var index = e.RowIndex;
            DataGridViewRow row = dataSearchView.Rows[index];
            String[] rowData = new String[headers.Length];
            for (int i = 0; i < headers.Length; i++)
            {
                rowData[i] = row.Cells[i].Value.ToString();
            }
            f2.editing(rowData, workingFile, data);
            f2.Show();
            f2.Disposed += new EventHandler(this.disposeHandler);
        } 
        private void disposeHandler(object sender, EventArgs e)
        {
            //dataSearchView.DataSource = updateData(true);
            updateData(true);
        }
        private void exitDOB(object sender, EventArgs e)
        {
            updateYear();
        }
        private void updateHandler(object sender, EventArgs e)
        {
            try
            {
                this.dataSearchView.Columns[headers[0]].Visible = false;
            }
            catch(Exception ex)
            {

            }
            
        }
        private void tabChangeHandler(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 1)
            {
               this.Size = new Size(511 + 475, 538);
               updateData(false);
            }
            else
            {
               this.Size = new Size(511, 538);
            }
            //470,320
            
            dataSearchView.Size = new Size(this.Size.Width - 41, dataSearchView.Height);
            dataSearchView.ScrollBars = ScrollBars.Both;
            //494, 500
            tabControl1.Size = new Size(this.Size.Width - 17, tabControl1.Height);
        }
        private void orderingChange(object sender, DataGridViewCellMouseEventArgs e) 
        {
            int counter = 0;

            foreach (DataGridViewRow row in dataSearchView.Rows)
            {
                if (counter % 2 != 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                counter++;
            }
        }
        private void removeSpaces(object sender, EventArgs e)
        {
            String text = txtName_Add.Text;
            text = text.Trim();
            while(text.Contains("  "))
            {
                text = text.Replace("  "," ");
            }
            txtName_Add.Text = text;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Delete && tabControl1.SelectedIndex == 1 && dataSearchView.SelectedRows.Count > 0) 
            {
                var res = MessageBox.Show("Are you sure you want to erase this entry?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(res == DialogResult.OK)
                {
                    DataGridViewSelectedRowCollection rows = dataSearchView.SelectedRows;
                    foreach(DataGridViewRow row in rows)
                    {
                        DataRow currentrow = (row.DataBoundItem as DataRowView).Row;
                        string hash = currentrow[0].ToString();
                        data.Rows.Remove(data.Rows.Find(hash));
                        updateData(true);
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Functions
        private void updateYear()
        {
            DateTime d;
            if (txtDOB_Add.Text.Trim().Equals(""))
            {
                txtDOB_Add.Text = "";

            }
            else
            {
                if (DateTime.TryParseExact(txtDOB_Add.Text, "d/M/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                {
                    txtDOB_Add.Text = d.ToString("dd/MM/yyyy");
                    txtYear_Add.Text = getYearGroup(d);
                }
                else if (DateTime.TryParseExact(txtDOB_Add.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                {
                    txtDOB_Add.Text = d.ToString("dd/MM/yyyy");
                    txtYear_Add.Text = getYearGroup(d);
                }
                else
                {
                    MessageBox.Show("Please ensure the date entered fits the format dd/MM/yy or dd/MM/yyyy", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private String getYearGroup(DateTime DOB)
        {
            DateTime today = DateTime.Today;

            int currentDay = today.Day;
            int currentMonth = today.Month;
            int currentYear = today.Year;

            int childDay = DOB.Day;
            int childMonth = DOB.Month;
            int childYear = DOB.Year;

            int age = currentYear - childYear;

            int currentTerm;
            int childTerm;

            String yearGroup;

            currentTerm = (currentMonth >= 9) ? 1 : ((currentMonth <= 3) ? 2 : 3);
            childTerm = (childMonth >= 9) ? 1 : ((childMonth <= 3) ? 2 : 3);
            currentYear += (currentTerm == 1) ? 1 : 0;

            if (currentMonth > childMonth)
            {
                age--;
            }
            else if (currentMonth == childMonth && currentDay > childDay)
            {
                age--;
            }

            int yrGr = (currentYear - childYear - ((childTerm == 1) ? 6 : 5));

            yearGroup = (yrGr < 1) ? "Reception" : (yrGr > 13) ? "Mature" : "Year " + yrGr;

            //yearGroup = "Year " + yrGr;

            return yearGroup;
        }
        private String HashFunction(string line)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(line);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string outhash = string.Empty;

            foreach (byte x in hash)
            {
                outhash += String.Format("{0:x2}", x);
            }

            return outhash;
        }
        private Boolean checkEmptyData()
        {
            Boolean completed = false;
            int counter = 0;
            int total = 0;
            foreach(TextBox box in elements)
            {
                if (box.Text.Trim().Equals(""))
                {
                    completed = false;
                    box.BackColor = Color.FromArgb(255, 51, 51);
                }
                else
                {
                    total += (int)Math.Pow(2, counter);
                }
                
                counter++;
            }

            
            
            if(total == 15 || total == 23 || total == 31)
            {
                completed = true;
            }

            return completed;
        }
        /*public void loadData()
        {
            string pathOnly = Path.GetDirectoryName(workingFile);
            string fileName = Path.GetFileName(workingFile);
            string sql = @"SELECT * FROM [" + fileName + "]";
            string oldeb = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly + ";Extended Properties=\"Text;HDR=Yes" + "\"";
            data = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(oldeb))
            using (OleDbCommand command = new OleDbCommand(sql, conn))
            using(OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                //data.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(data);
            }
            updateYearGroups();
            data.PrimaryKey = new DataColumn[] {data.Columns[headers[0]]};

        }*/
        public void loadData()
        {
            using (var reader = new StreamReader(workingFile))
            using (var csv = new CsvReader(reader))
            {
                
                using (var dr = new CsvDataReader(csv))
                {
                    data = new DataTable();
                    data.Columns.Add(headers[0], typeof(string));
                    data.Columns.Add(headers[1], typeof(DateTime));
                    data.Columns.Add(headers[2], typeof(string));
                    data.Columns.Add(headers[3], typeof(DateTime));
                    data.Columns.Add(headers[4], typeof(string));
                    data.Columns.Add(headers[5], typeof(string));
                    data.Columns.Add(headers[6], typeof(string));
                    data.Columns.Add(headers[7], typeof(string));
                    data.Columns.Add(headers[8], typeof(string));
                    data.Columns.Add(headers[9], typeof(string));

                    data.Load(dr);
                }
            }
            updateYearGroups();
            data.PrimaryKey = new DataColumn[] { data.Columns[headers[0]] };
        }
        private String generateSQL()
        {
            string sql = String.Empty;
            if (!txtName_Search.Text.Trim().Equals(""))
            {
                sql += " where "+headers[2]+" like '%" + txtName_Search.Text + "%'";
                if (listCheck.CheckedItems.Count != 0)
                {
                    sql += " and ";
                    foreach (string item in listCheck.CheckedItems)
                    {
                        sql += headers[4]+" = '" + item + "' or ";
                    }

                    sql = sql.Substring(0, sql.LastIndexOf("or"));
                }
            }
            else
            {
                if (listCheck.CheckedItems.Count != 0)
                {
                    sql += " where ";
                    foreach (string item in listCheck.CheckedItems)
                    {
                        sql += headers[4]+" = '" + item + "' or ";
                    }

                    sql = sql.Substring(0, sql.LastIndexOf("or"));
                }
            }

            if (sql.Contains("where"))
            {
                if (!enableContacted.Checked)
                {
                    sql += " and not " +headers[8]+ "= 'Yes'";
                }

            }
            else
            {
                if (!enableContacted.Checked)
                {
                    sql += " where not "+headers[8]+" = 'Yes'";
                }

            }

            if (sql.Contains("where"))
            {
                if (!enableArchived.Checked)
                {
                    sql += " and not " + headers[9] + " = 'Yes'";
                }

            }
            else
            {
                if (!enableArchived.Checked)
                {
                    sql += " where not " + headers[9] + " = 'Yes'";
                }

            }
            return sql;
        }
        public void loadCSV()
        {
            ArrayList files = new ArrayList();
            Regex re = new Regex("[0-9]{14,14}");
            if (!Directory.Exists(Application.StartupPath + "\\data"))
            {
                Console.WriteLine("Making directory");
                Directory.CreateDirectory(Application.StartupPath + "\\data");
                MessageBox.Show("Couldn't find data folder, please select a data set to copy from", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    String foundFile = ofd.FileName;
                    workingFile = Application.StartupPath + "\\data\\" + fileNameFormat + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
                    File.Copy(foundFile, workingFile);
                }
                else
                {
                    Application.Exit();
                }
            }
            foreach (string f in Directory.EnumerateFiles(Application.StartupPath + "\\data\\", "*.csv"))
            {
                string filename = Path.GetFileName(f);
                string dateformat = filename.Split('_')[1].Split('.')[0];
                string nameformat = filename.Split('_')[0];
                if (re.IsMatch(dateformat) && nameformat.Equals(fileNameFormat))
                {
                    files.Add(Convert.ToInt64(dateformat));
                }
            }
            files.Sort();
            if(files.Count != 0)
            {
                workingFile = Application.StartupPath + "\\data\\" + fileNameFormat + "_" + files[files.Count - 1].ToString() + ".csv";
            }
            else
            {
                var res = MessageBox.Show("No CSV files found in data directory! \n Use the Converter to convert an existing one or move an existing one into the directory.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                if (res == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            if(files.Count > filesToKeep)
            {
               
                for (int i = 0; i < files.Count - filesToKeep; i++)
                {
                    String f = Application.StartupPath + "\\data\\" + fileNameFormat + "_" +  files[i].ToString() +".csv";
                    File.Delete(f);
                    Console.WriteLine("Deleting " + f);
                }
            }
        }
        public void loadConfig()
        {
            String cfg = Application.StartupPath + "\\config.cfg";
            try
            {
                StreamReader sr = new StreamReader(cfg);
                String line = sr.ReadLine();
                filesToKeep = Convert.ToInt32(line.Split('=')[1]);
                line = sr.ReadLine();
                fileNameFormat = line.Split('=')[1];
                sr.Close();
                //MessageBox.Show("no:" + filesToKeep + "\nformat:" + fileNameFormat);
            }
            catch (Exception ex)
            {
                filesToKeep = 10;
                fileNameFormat = "Testdata2";
                Console.WriteLine(ex.Message);
            }
        }
        public void updateData(Boolean save)
        {
            if (firstEdit && save)
            {

                String tmp = Application.StartupPath + "\\data\\" + fileNameFormat + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
                File.Copy(workingFile, tmp);
                workingFile = tmp;
                firstEdit = false;
            }
            File.Create(workingFile).Close();
            writeHeaders(workingFile);
            for (int j = 0; j < data.Rows.Count; j++)
            {
                object[] items = data.Rows[j].ItemArray;
                String output = "";
                for (int i = 0; i < items.Length; i++)
                {
                    output += items[i].ToString() + ",";
                }
                output = output.Substring(0, output.LastIndexOf(",")) + Environment.NewLine;
                File.AppendAllText(workingFile, output);
            }
            DataTable dt = new DataTable();
            for(int i = 0; i < headers.Length; i++)
            {
                DataColumn cl = new DataColumn(headers[i]);
                dt.Columns.Add(cl);
            }
            String sql = generateSQL();
            if (sql.Contains("where"))
            {
                sql = sql.Substring(sql.IndexOf("where") + 5, sql.Length - sql.IndexOf("where") - 5).TrimStart();
            }
            DataRow[] foundRows = data.Select(sql);
            foreach (DataRow d in foundRows)
            {
                dt.Rows.Add(d.ItemArray);

            }
            dt.Columns[headers[0]].Convert(val => val.ToString());
            dt.Columns[headers[1]].Convert(val => DateTime.Parse(val.ToString()).ToString("dd/MM/yyyy"));
            dt.Columns[headers[2]].Convert(val => val.ToString());
            dt.Columns[headers[3]].Convert(val => DateTime.Parse(val.ToString()).ToString("dd/MM/yyyy"));
            dt.Columns[headers[4]].Convert(val => val.ToString());
            dt.Columns[headers[5]].Convert(val => val.ToString());
            dt.Columns[headers[6]].Convert(val => val.ToString());
            dt.Columns[headers[7]].Convert(val => val.ToString());
            dt.Columns[headers[8]].Convert(val => val.ToString());
            dt.Columns[headers[9]].Convert(val => val.ToString());

            dataSearchView.DataSource = dt;
            int counter = 0;
            
            foreach(DataGridViewRow row in dataSearchView.Rows)
            {
                if(counter%2 != 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                counter++;
            }
        }
        private void writeHeaders(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < headers.Length - 1; i++)
            {
                sw.Write(headers[i] + ",");
            }
            sw.Write(headers[headers.Length - 1] + "\n");
            sw.Close();
        }
        public void editing(String[] data, String path, DataTable table)
        {
            this.btnConfirm_Add.Click -= new EventHandler(this.BtnConfirm_Click);
            this.btnConfirm_Add.Click += new EventHandler(this.BtnConfirm_Edit);
            this.btnExit_Add.Click -= new EventHandler(this.BtnExit_Click);
            this.btnExit_Add.Click += new EventHandler(this.BtnExit_Edit);
            this.dataSearchView.DataSourceChanged -= new EventHandler(this.updateHandler);
            this.btnSpreadSheet_Add.Visible = false;
            this.tabControl1.TabPages.RemoveAt(1);
            this.lblTitle_Add.Text = "Editing Entry";
            this.btnExit_Add.Text = "Close";
            this.duplicateCheck.Checked = false;
            this.duplicateCheck.Visible = false;
            for(int i = 0; i< elements.Length; i++)
            {
                elements[i].Text = data[i + 2];
            }
            txtDOB_Add.Text = txtDOB_Add.Text.Split(' ')[0] ;
            richTextBox1.Text = data[7];
            isContacted.Checked = (data[8].Equals("Yes")) ? true : false;
            isArchived.Checked = (data[9].Equals("Yes")) ? true : false;
            rowHash = data[0];
            date = data[1].Split(' ')[0];
            workingFile = path;
            this.data = table;
            //MessageBox.Show(data[0] + "\n" + data[1] + "\n" + data[2] + "\n" + data[3] + "\n" + data[4] + "\n" + data[5] + "\n" + data[6] + "\n" + data[7] + "\n" + data[8] + "\n" + data[9]);
        }
        private void updateYearGroups()
        {
            DateTime[] DOBs = new DateTime[data.Rows.Count];
            for(int i = 0; i < DOBs.Length; i++)
            {
                DOBs[i] = data.Rows[i].Field<DateTime>(headers[3]);
            }
            string[] years = new string[DOBs.Length];
            for(int i = 0; i < years.Length; i++)
            {
                years[i] = getYearGroup(DOBs[i]);
            }

            for(int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i][headers[4]] = years[i];
            }

            for(int i = 0; i < data.Rows.Count; i++)
            {
                string line = string.Empty;
                for (int j = 1; j < data.Columns.Count; j++)
                {
                    line += data.Rows[i][j].ToString() + ",";
                }
                line = line.Substring(0, line.LastIndexOf(","));
                data.Rows[i][headers[0]] = HashFunction(line);
            }

        }
        
        #endregion

        #region Form Controls
        private void BtnCalDropDown_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = !monthCalendar1.Visible;
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (checkEmptyData())
            {
                Object[] export = new Object[headers.Length];

                export[1] = DateTime.Today.ToString("dd/MM/yyyy");
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i].Text.Trim().Equals(""))
                    {
                        export[i + 2] = null;
                    }
                    else
                    {
                        export[i + 2] = elements[i].Text;
                    }
                    
                }
                export[7] = richTextBox1.Text;
                export[8] = (isContacted.Checked) ? "Yes" : "No";
                export[9] = (isArchived.Checked) ? "Yes" : "No";
                string line = string.Empty;
                for (int i = 1; i < export.Length; i++)
                {
                    line += export[i] + ",";
                }
                line = line.Substring(0, line.LastIndexOf(","));
                export[0] = HashFunction(line);

                if (!workingFile.Equals(String.Empty))
                {
                    //File.AppendAllText(workingFile, csvline);
                    DataRow row = data.NewRow();
                    row.ItemArray = export;
                    data.Rows.Add(row);
                    //dataSearchView.DataSource = updateData(true);
                    updateData(true);
                    foreach(TextBox box in elements)
                    {
                        box.Text = "";
                        box.BackColor = Color.White;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a file first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("One or more fields was left blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnConfirm_Edit(object sender, EventArgs e)
        {
            String[] export = new String[headers.Length];
            
            export[1] = date;
            for(int i = 0; i < elements.Length; i++)
            {
                export[i + 2] = elements[i].Text;
            }
            export[7] = richTextBox1.Text;
            export[8] = (isContacted.Checked) ? "Yes" : "No";
            export[9] = (isArchived.Checked) ? "Yes" : "No";
            string line = string.Empty;
            for(int i = 1; i < export.Length; i++)
            {
                line += export[i] + ",";
            }
            line = line.Substring(0, line.LastIndexOf(","));
            export[0] = HashFunction(line);
            int index = data.Rows.IndexOf(data.Rows.Find(rowHash));
            DataRow row = data.NewRow();
            row.ItemArray = export;
            data.Rows.InsertAt(row, index);
            data.Rows.RemoveAt(index + 1);
            this.Dispose();
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void BtnExit_Edit(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to stop editing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            //dataSearchView.DataSource = updateData(false);
            
            this.CenterToScreen();
        }
        private void BtnSpreadSheet_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                workingFile = ofd.FileName;
            }
        }
        private void BtnSearch_Search_Click(object sender, EventArgs e)
        {
            //dataSearchView.DataSource = updateData(false);
            updateData(false);
        }
        #endregion


    }
}
