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
        private Dictionary<string, TextBox> elements = new Dictionary<string, TextBox>();
        private string workingFile = string.Empty;
        private DataTable data;
        private Dictionary<string, int> headers = new Dictionary<string, int>();
        private string[] header_names = { "Hash", "Date Added", "Name", "DOB", "School Year", "Phone Number", "Email", "More Details", "Contacted", "Archived" };
        private string[] years = { "Reception", "Year 1", "Year 2", "Year 3", "Year 4", "Year 5", "Year 6", "Year 7", "Year 8", "Year 9", "Year 10", "Year 11", "Year 12", "Year 13", "Mature" };
        private bool firstEdit = true;
        private string rowHash;
        private string date;
        private string data_folder = "\\data";

        //Config Vars
        private string fileNameFormat = Properties.Settings.Default.file_name;
        private int filesToKeep = Properties.Settings.Default.file_amount;



        public DataViewer()
        {
            InitializeComponent();
            this.txtName_Add.KeyPress += new KeyPressEventHandler(this.checkKeyPress);
            this.txtName_Search.KeyPress += new KeyPressEventHandler(this.checkKeyPress);
            this.txtPhone_Add.KeyPress += new KeyPressEventHandler(this.checkKeyPress);
            this.txtExtras_Search.KeyPress += new KeyPressEventHandler(this.richKeyPressCheck);
            this.richTextBox1.KeyPress += new KeyPressEventHandler(this.richKeyPressCheck);
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

        }

        public void postLoad() {
            //TextBox[] tmp_elements = new TextBox[] { txtName_Add, txtDOB_Add, txtYear_Add, txtPhone_Add, txtEmail_Add };
            elements.Add("name_index",txtName_Add);
            elements.Add("dob_index", txtDOB_Add);
            elements.Add("year_index", txtYear_Add);
            elements.Add("phone_index", txtPhone_Add);
            elements.Add("email_index", txtEmail_Add);

            headers.Add("hash_index", (int)Properties.Settings.Default["hash_index"]);
            headers.Add("added_index", (int)Properties.Settings.Default["added_index"]);
            headers.Add("name_index", (int)Properties.Settings.Default["name_index"]);
            headers.Add("dob_index", (int)Properties.Settings.Default["dob_index"]);
            headers.Add("year_index", (int)Properties.Settings.Default["year_index"]);
            headers.Add("phone_index", (int)Properties.Settings.Default["phone_index"]);
            headers.Add("email_index", (int)Properties.Settings.Default["email_index"]);
            headers.Add("details_index", (int)Properties.Settings.Default["details_index"]);
            headers.Add("contact_index", (int)Properties.Settings.Default["contact_index"]);
            headers.Add("archive_index", (int)Properties.Settings.Default["archive_index"]);

            foreach (var item in elements) {
                item.Value.Enter += new EventHandler(enterTextBox);
            }
        }

        #region Handlers
        private void checkKeyPress(object sender, KeyPressEventArgs e)
        {
            Regex re = null;

            TextBox box = (TextBox)sender;

            if (box == txtName_Add || box == txtName_Search)
            {
                re = new Regex("^[/|\\\\|'|\"|%|!|~|&|,|(|)|{|}|\\[|\\].|\\*|\\^|\\$|<|>|\\?|\\+|=|_|#|@|;|:|`]$");
            }
            else if (box == txtPhone_Add)
            {
                re = new Regex("[^0-9\b]$");
            }

            if (re.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void richKeyPressCheck(object sender, KeyPressEventArgs e)
        {
            Regex re = null;

            RichTextBox box = (RichTextBox)sender;
            re = new Regex("^[/|\\\\|'|\"|%|!|~|&|,|(|)|{|}|\\[|\\].|\\*|\\^|\\$|<|>|\\?|\\+|=|_|#|@|;|:|`]$");

            if (re.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void calendarChanged(object sender, DateRangeEventArgs e)
        {
            MonthCalendar cal = (MonthCalendar)sender;
            string date = cal.SelectionStart.ToString("dd/MM/yyyy");
            txtDOB_Add.Text = date;
            updateYear();
        }

        private void enterTextBox(object sender, EventArgs e)
        {
            TextBox current = (TextBox)sender;
            current.BackColor = Color.White;
            string tmpsql = "";
            if (duplicateCheck.Checked)
            {
                /* foreach(TextBox box in elements)
                {
                    if (!(box.Text.Trim().Equals("")))
                    {
                        if (tmpsql.Equals(""))
                        {
                            tmpsql = "[" + header_names[counter + 2] + "] = '" + box.Text.Trim() + "'";
                        }
                        else
                        {
                            tmpsql += " and [" + header_names[counter + 2] + "] = '" + box.Text.Trim() + "'";
                        }
                    }
                    counter++;
                }*/
                foreach(var item in elements)
                {
                    if (!(item.Value.Text.Trim().Equals("")))
                    {
                        if (tmpsql.Equals(""))
                        {
                            tmpsql = "[" + header_names[(int)Properties.Settings.Default[item.Key]] + "] = '" + item.Value.Text.Trim() + "'";
                        }
                        else 
                        {
                            tmpsql += " and [" + header_names[(int)Properties.Settings.Default[item.Key]] + "] = '" + item.Value.Text.Trim() + "'";
                        }
                    }
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
                string items = "";

                if (listCheck.CheckedItems.Count != 0)
                {
                    foreach (string item in listCheck.CheckedItems)
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
            if (index > -1)
            {
                DataGridViewRow row = dataSearchView.Rows[index];
                string[] rowData = new string[header_names.Length];

                foreach (var item in headers) {
                    rowData[item.Value] = row.Cells[item.Value].Value.ToString();
                }

                /*for (int i = 0; i < headers.Count; i++)
                {
                    rowData[i] = row.Cells[i].Value.ToString();
                }*/
                f2.postLoad();
                f2.editing(rowData, workingFile, data);
                f2.Show();
                f2.Disposed += new EventHandler(this.disposeHandler);
            }

        }
        private void disposeHandler(object sender, EventArgs e)
        {
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
                this.dataSearchView.Columns[header_names[Properties.Settings.Default.hash_index]].Visible = false;
            }
            catch (Exception)
            {

            }

        }
        private void tabChangeHandler(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
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
            string text = txtName_Add.Text;
            text = text.Trim();
            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }
            txtName_Add.Text = text;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete && tabControl1.SelectedIndex == 1 && dataSearchView.SelectedRows.Count > 0)
            {
                var res = MessageBox.Show("Are you sure you want to erase this entry?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (res == DialogResult.OK)
                {
                    DataGridViewSelectedRowCollection rows = dataSearchView.SelectedRows;
                    foreach (DataGridViewRow row in rows)
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
        //TODO: TEST THIS
        private void updateYear()
        {
            DateTime d;
            if (!txtDOB_Add.Text.Trim().Equals(""))
            {
                try
                {
                    d = parseDateTime(txtDOB_Add.Text);
                    txtDOB_Add.Text = d.ToString("dd/MM/yyyy");
                    txtYear_Add.Text = getYearGroup(d);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to convert the given date (" + txtDOB_Add.Text + ") to a valid date format!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string getYearGroup(DateTime DOB)
        {
            DateTime today = DateTime.Today;

            int currentDay = today.Day;
            int currentMonth = today.Month;
            int currentYear = today.Year;

            int childDay = DOB.Day;
            int childMonth = DOB.Month;
            int childYear = DOB.Year;

            //int age = currentYear - childYear;

            int currentTerm;
            int childTerm;

            string yearGroup;

            currentTerm = (currentMonth >= 9) ? 1 : ((currentMonth <= 3) ? 2 : 3);
            childTerm = (childMonth >= 9) ? 1 : ((childMonth <= 3) ? 2 : 3);
            currentYear += (currentTerm == 1) ? 1 : 0;

            /*if (currentMonth > childMonth)
            {
                age--;
            }
            else if (currentMonth == childMonth && currentDay > childDay)
            {
                age--;
            }*/

            int yrGr = (currentYear - childYear - ((childTerm == 1) ? 6 : 5));

            yearGroup = (yrGr < 1) ? "Reception" : (yrGr > 13) ? "Mature" : "Year " + yrGr;

            return yearGroup;
        }

        private string HashFunction(string line)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(line);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string outhash = string.Empty;

            foreach (byte x in hash)
            {
                outhash += string.Format("{0:x2}", x);
            }

            return outhash;
        }

        //TODO: TEST THIS
        private Boolean checkEmptyData()
        {
            bool completed = false;
            int counter = 0;
            int total = 0;
            foreach (var item in elements)
            {
                if (item.Value.Text.Trim().Equals(""))
                {
                    completed = false;
                    item.Value.BackColor = Color.FromArgb(255, 51, 51);
                }
                else
                {
                    total += (int)Math.Pow(2, counter);
                }

                counter++;
            }



            if (total == 15 || total == 23 || total == 31)
            {
                completed = true;
            }

            return completed;
        }

        public void loadData()
        { 
            string[] firstline = File.ReadLines(workingFile).First().Split(',');
            for (int i = 0; i < firstline.Length; i++)
            {
                header_names[i] = firstline[i];
            }

            using (var reader = new StreamReader(workingFile))
            using (var csv = new CsvReader(reader))
            {

                using (var dr = new CsvDataReader(csv))
                {
                    data = new DataTable();
                    data.Columns.Add(header_names[0], typeof(string));
                    data.Columns.Add(header_names[1], typeof(string));
                    data.Columns.Add(header_names[2], typeof(string));
                    data.Columns.Add(header_names[3], typeof(string));
                    data.Columns.Add(header_names[4], typeof(string));
                    data.Columns.Add(header_names[5], typeof(string));
                    data.Columns.Add(header_names[6], typeof(string));
                    data.Columns.Add(header_names[7], typeof(string));
                    data.Columns.Add(header_names[8], typeof(string));
                    data.Columns.Add(header_names[9], typeof(string));

                    data.Load(dr);
                }
            }
            updateYearGroups();
            data.PrimaryKey = new DataColumn[] { data.Columns[Properties.Settings.Default.hash_index] };
        }

        //TODO: Use for SQL
        private string generateSQL()
        {
            string sql = string.Empty;

            //Name Filter
            if (!txtName_Search.Text.Trim().Equals(""))
            {
                sql = "where [" + header_names[Properties.Settings.Default.name_index] + "] like '%" + txtName_Search.Text + "%'";
            }

            //Year Group Filter
            if (listCheck.CheckedItems.Count > 0)
            {
                if (sql.Contains("where"))
                {
                    sql += " and";
                }
                else
                {
                    sql = "where";
                }

                foreach (string item in listCheck.CheckedItems)
                {
                    sql += " [" + header_names[Properties.Settings.Default.year_index] + "] = '" + item + "' or ";
                }

                sql = sql.Substring(0, sql.LastIndexOf("or"));

            }

            //Contacted Filter
            if (!enableContacted.Checked)
            {
                if (sql.Contains("where"))
                {
                    sql += " and";
                }
                else
                {
                    sql = "where";
                }
                sql += " not [" + header_names[Properties.Settings.Default.contact_index] + "] = 'Yes'";
            }

            //Archived Filter
            if (!enableArchived.Checked)
            {
                if (sql.Contains("where"))
                {
                    sql += " and";
                }
                else
                {
                    sql = "where";
                }
                sql += " not [" + header_names[Properties.Settings.Default.archive_index] + "] = 'Yes'";
            }

            //Extras Filter
            if (txtExtras_Search.Text.Trim().Length > 0)
            {
                if (sql.Contains("where"))
                {
                    sql += " and";
                }
                else
                {
                    sql = "where";
                }
                string[] queryText = txtExtras_Search.Text.Trim().Split(',');
                sql += " [" + header_names[Properties.Settings.Default.details_index] + "] like '%" + queryText[0].Trim() + "%'";
                for (int i = 1; i < queryText.Length; i++)
                {
                    sql += " or [" + header_names[Properties.Settings.Default.details_index] + "] like '%" + queryText[i].Trim() + "%'";
                }

            }

            return sql;
        }

        public void loadCSV()
        {

            ArrayList files = new ArrayList();
            Regex re = new Regex("[0-9]{14,14}");
            if (!Directory.Exists(Application.StartupPath + data_folder) || Directory.Exists(Application.StartupPath + data_folder) && Directory.GetFiles(Application.StartupPath + data_folder, "*.csv", SearchOption.AllDirectories).Length == 0)
            {
                Console.WriteLine("Making directory");
                MessageBox.Show("Couldn't find data folder, please select a data set to copy from", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string foundFile = ofd.FileName;
                    Directory.CreateDirectory(Application.StartupPath + data_folder);
                    workingFile = Application.StartupPath + data_folder + "\\" + fileNameFormat + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
                    File.Copy(foundFile, workingFile);
                }
                else
                {
                    Application.Exit();
                }
            }
            foreach (string f in Directory.EnumerateFiles(Application.StartupPath + data_folder + "\\", "*.csv"))
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
            if (files.Count != 0)
            {
                workingFile = Application.StartupPath + data_folder + "\\" + fileNameFormat + "_" + files[files.Count - 1].ToString() + ".csv";
            }
            else
            {
                var res = MessageBox.Show("No CSV files found in data directory! \n Use the Converter to convert an existing one or move an existing one into the directory.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (res == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            if (files.Count > filesToKeep)
            {

                for (int i = 0; i < files.Count - filesToKeep; i++)
                {
                    string f = Application.StartupPath + data_folder + "\\" + fileNameFormat + "_" + files[i].ToString() + ".csv";
                    File.Delete(f);
                    Console.WriteLine("Deleting " + f);
                }
            }
        }

        /*public void loadConfig()
        {
            string cfg = Application.StartupPath + "\\config.cfg";
            try
            {
                StreamReader sr = new StreamReader(cfg);
                string line = sr.ReadLine();
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
        }*/

        public void updateData(Boolean save)
        {
            if (firstEdit)
            {

                string tmp = Application.StartupPath + data_folder + "\\" + fileNameFormat + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
                File.Copy(workingFile, tmp);
                workingFile = tmp;
                firstEdit = false;
            }

            if (save)
            {
                using (var textWriter = File.CreateText(workingFile))
                using (var csv = new CsvWriter(textWriter))
                {
                    // Write columns
                    foreach (DataColumn column in data.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();

                    // Write row values
                    foreach (DataRow row in data.Rows)
                    {
                        for (var i = 0; i < data.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]);
                        }
                        csv.NextRecord();
                    }
                }
            }

            DataTable dt = new DataTable();
            foreach (var item in header_names)
            {
                DataColumn cl = new DataColumn(item);
                dt.Columns.Add(cl);
            }
            string sql = generateSQL();
            if (sql.Contains("where"))
            {
                sql = sql.Substring(sql.IndexOf("where") + 5, sql.Length - sql.IndexOf("where") - 5).TrimStart();
            }
            DataRow[] foundRows = data.Select(sql);
            foreach (DataRow d in foundRows)
            {
                dt.Rows.Add(d.ItemArray);

            }

            // Hash Column
            dt.Columns[header_names[Properties.Settings.Default.hash_index]].Convert(val => val.ToString());
            
            // Date Added Column
            try
            {
                dt.Columns[header_names[Properties.Settings.Default.added_index]].Convert(val => parseDateTime(val.ToString()).ToString("dd/MM/yyyy"));
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to import date added: " + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Name Column
            dt.Columns[header_names[Properties.Settings.Default.name_index]].Convert(val => val.ToString());
            
            // DOB Column
            try
            {
                dt.Columns[header_names[Properties.Settings.Default.dob_index]].Convert(val => parseDateTime(val.ToString()).ToString("dd/MM/yyyy"));
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to import date of birth: " + e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            dt.Columns[header_names[Properties.Settings.Default.year_index]].Convert(val => val.ToString()); // School Year Column
            dt.Columns[header_names[Properties.Settings.Default.phone_index]].Convert(val => val.ToString()); // Phone Number Column
            dt.Columns[header_names[Properties.Settings.Default.email_index]].Convert(val => val.ToString()); // Email Column
            dt.Columns[header_names[Properties.Settings.Default.details_index]].Convert(val => val.ToString()); // Details Column
            dt.Columns[header_names[Properties.Settings.Default.contact_index]].Convert(val => val.ToString()); // Contacted Column
            dt.Columns[header_names[Properties.Settings.Default.archive_index]].Convert(val => val.ToString()); // Archived Column

            dataSearchView.DataSource = dt;
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

        public void editing(string[] data, string path, DataTable table)
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

            txtName_Add.Text = data[Properties.Settings.Default.name_index];
            txtDOB_Add.Text = data[Properties.Settings.Default.dob_index];
            txtPhone_Add.Text = data[Properties.Settings.Default.phone_index];
            txtYear_Add.Text = data[Properties.Settings.Default.year_index];
            txtEmail_Add.Text = data[Properties.Settings.Default.email_index];

            txtDOB_Add.Text = txtDOB_Add.Text.Split(' ')[0];
            richTextBox1.Text = data[Properties.Settings.Default.details_index];
            isContacted.Checked = (data[Properties.Settings.Default.contact_index].Equals("Yes")) ? true : false;
            isArchived.Checked = (data[Properties.Settings.Default.archive_index].Equals("Yes")) ? true : false;
            rowHash = data[Properties.Settings.Default.hash_index];
            date = data[Properties.Settings.Default.added_index].Split(' ')[0];
            workingFile = path;
            this.data = table;
        }

        private void updateYearGroups()
        {

            string[] DOBs = new string[data.Rows.Count];
            for (int i = 0; i < DOBs.Length; i++)
            {
                DOBs[i] = data.Rows[i].Field<string>(header_names[Properties.Settings.Default.dob_index]);
            }
            string[] years = new string[DOBs.Length];
            for (int i = 0; i < years.Length; i++)
            {
                string date = DOBs[i];
                try
                {
                    years[i] = getYearGroup(parseDateTime(date));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Got the following Error when handling entry '" + data.Rows[i].Field<string>(header_names[Properties.Settings.Default.name_index]) + "' : " + e.Message, "Unable to Update Year Groups!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i][header_names[Properties.Settings.Default.year_index]] = years[i];
            }

            for (int i = 0; i < data.Rows.Count; i++)
            {
                string line = string.Empty;
                for (int j = 1; j < data.Columns.Count; j++)
                {
                    line += data.Rows[i][j].ToString() + ",";
                }
                line = line.Substring(0, line.LastIndexOf(","));
                //TODO: Testing
                data.Rows[i][header_names[Properties.Settings.Default.hash_index]] = checkHashDuplicate(line);
                //data.Rows[i][header_names[Properties.Settings.Default.hash_index]] = HashFunction(line);
            }

        }

        private string checkHashDuplicate(string line) 
        {
            string hash = HashFunction(line);
            string sql = header_names[Properties.Settings.Default.hash_index] + " = '" + hash + "'";
            DataRow[] foundRows = data.Select(sql);
            if (foundRows.Length != 0)
            {
                return HashFunction(hash);
            }
            else {
                return hash;
            }
        }

        private DateTime parseDateTime(string date)
        {
            string[] formats = { "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy", "dd/MM/yy", "dd/MM/yy HH:mm:ss", "dd/MM/yyyy HH:mm", "dd/MM/yy HH:mm" };
            DateTime result = DateTime.MinValue;
            string actual = date.Trim().Replace(" CET", "").Replace(" AM", "").Replace(" PM", "");

            foreach (string format in formats)
            {
                DateTime.TryParseExact(actual, format, CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out result);
                if (result != DateTime.MinValue)
                {
                    break;
                }
            }

            if (result == DateTime.MinValue)
            {
                throw new Exception("Unabled to convert " + date + " to a valid date format!");
            }

            result = DateTime.ParseExact(result.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);

            return result;
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
                Object[] export = new Object[header_names.Length];

                export[Properties.Settings.Default.added_index] = DateTime.Today.ToString("dd/MM/yyyy");
                /*for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i].Text.Trim().Equals(""))
                    {
                        export[i + 2] = null;
                    }
                    else
                    {
                        export[i + 2] = elements[i].Text;
                    }

                }*/
                foreach (var item in elements) {
                    if (item.Value.Text.Trim().Equals(""))
                    {
                        export[(int)Properties.Settings.Default[item.Key]] = null;
                    }
                    else {
                        export[(int)Properties.Settings.Default[item.Key]] = item.Value.Text;
                    }
                }
                export[Properties.Settings.Default.details_index] = richTextBox1.Text;
                export[Properties.Settings.Default.contact_index] = (isContacted.Checked) ? "Yes" : "No";
                export[Properties.Settings.Default.archive_index] = (isArchived.Checked) ? "Yes" : "No";
                string line = string.Empty;
                for (int i = 1; i < export.Length; i++)
                {
                    line += export[i] + ",";
                }
                line = line.Substring(0, line.LastIndexOf(","));
                //TODO: Testing
                export[Properties.Settings.Default.hash_index] = checkHashDuplicate(line);
                //export[Properties.Settings.Default.hash_index] = HashFunction(line);

                if (!workingFile.Equals(string.Empty))
                {
                    //File.AppendAllText(workingFile, csvline);
                    DataRow row = data.NewRow();
                    row.ItemArray = export;
                    data.Rows.Add(row);
                    //dataSearchView.DataSource = updateData(true);
                    updateData(true);
                    foreach (var item in elements)
                    {
                        item.Value.Text = "";
                        item.Value.BackColor = Color.White;
                    }
                    richTextBox1.Text = "";
                    isContacted.Checked = false;
                    isArchived.Checked = false;
                    MessageBox.Show("Added entry to database!", "Complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string[] export = new string[header_names.Length];

            export[Properties.Settings.Default.added_index] = date;
            foreach (var item in elements)
            {
                export[(int)Properties.Settings.Default[item.Key]] = item.Value.Text;
            }
            export[Properties.Settings.Default.details_index] = richTextBox1.Text;
            export[Properties.Settings.Default.contact_index] = (isContacted.Checked) ? "Yes" : "No";
            export[Properties.Settings.Default.archive_index] = (isArchived.Checked) ? "Yes" : "No";
            string line = string.Empty;
            for (int i = 1; i < export.Length; i++)
            {
                line += export[i] + ",";
            }
            line = line.Substring(0, line.LastIndexOf(","));
            //TODO: Testing
            export[Properties.Settings.Default.hash_index] = checkHashDuplicate(line);
            //export[Properties.Settings.Default.hash_index] = HashFunction(line);
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
            if (result == DialogResult.Yes)
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
            updateData(false);
        }
        #endregion
    }
}
