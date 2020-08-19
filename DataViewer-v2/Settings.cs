using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataViewer_v2
{
    public partial class Settings : Form
    {

        private Dictionary<string, int> headers = new Dictionary<string, int>();
        private Dictionary<string, string> settings = new Dictionary<string, string>();
        private string[] setting_names = { "hash_index", "added_index", "name_index", "dob_index", "year_index", "phone_index", "email_index", "details_index", "contact_index", "archive_index" };
        private string[] header_names = { "Hash", "Date Added", "Name", "DOB", "School Year", "Phone Number", "Email Address", "Extra Details", "Contacted", "Archived" };
        private Form start;

        private int oldvalue;

        /*private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;*/
        public Settings(Form start)
        {
            InitializeComponent();
            addHeaderSettingsRows();
            this.numFiles_Setting.Value = (int)Properties.Settings.Default["file_amount"];
            this.txtFileName_Setting.Text = Properties.Settings.Default["file_name"].ToString();
            this.start = start;
        }

        private void btnBack_Setting_Click(object sender, EventArgs e)
        {
            this.start.Show();
            this.Dispose();
        }
        private void addHeaderSettingsRows()
        {
            /*headers.Add(Properties.Settings.Default.hash_index, "Hash");
            headers.Add(Properties.Settings.Default.added_index, "Date Added");
            headers.Add(Properties.Settings.Default.name_index, "Name");
            headers.Add(Properties.Settings.Default.dob_index, "DOB");
            headers.Add(Properties.Settings.Default.year_index, "School Year");
            headers.Add(Properties.Settings.Default.phone_index, "Phone Number");
            headers.Add(Properties.Settings.Default.email_index, "Email Address");
            headers.Add(Properties.Settings.Default.details_index, "Extra Details");
            headers.Add(Properties.Settings.Default.contact_index, "Contacted");
            headers.Add(Properties.Settings.Default.archived_index, "Archived");*/

            for (int i = 0; i < setting_names.Length; i++) {
                settings.Add(header_names[i], setting_names[i]);
            }

            foreach (var item in settings) {
                headers.Add(item.Key, (int)Properties.Settings.Default[item.Value]);
            }

            foreach (var item in headers)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataHeaders_Setting);
                row.Cells[1].Value = item.Value;
                row.Cells[0].Value = item.Key;
                dataHeaders_Setting.Rows.Add(row);
            }
        }

        private void btnSave_Setting_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow item in dataHeaders_Setting.Rows)
            {

                int value = int.Parse(item.Cells[1].Value.ToString());

                if (list.Contains(value))
                {
                    MessageBox.Show("The index " + value + " appears more than once! Please ensure that each index only appears once!", "Unable to Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    list.Add(value);
                }
            }

            foreach (DataGridViewRow row in dataHeaders_Setting.Rows) {

                string name = row.Cells[0].Value.ToString();
                Console.WriteLine(name);
                string setting = settings[name];
                Console.WriteLine(setting);
                int index = int.Parse(row.Cells[1].Value.ToString());
                Properties.Settings.Default[setting] = index;
            
            }

            Properties.Settings.Default["file_amount"] = (int)numFiles_Setting.Value;
            Properties.Settings.Default["file_name"] = txtFileName_Setting.Text;

            Properties.Settings.Default.Save();
            MessageBox.Show("Done! Finished Saving Setting!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataHeaders_Settings_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            e.Control.KeyPress -= new KeyPressEventHandler(this.colIndex_KeyPress);
            if (this.dataHeaders_Setting.CurrentCell.ColumnIndex == this.dataHeaders_Setting.Columns.IndexOf(this.colIndex)) {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(this.colIndex_KeyPress);
                }
            }
        }

        private void colIndex_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataHeaders_Setting_EndEdit(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex > -1 && this.dataHeaders_Setting.Rows[e.RowIndex].Cells[1].Value != null)
            {
                int value = Int32.Parse(this.dataHeaders_Setting.Rows[e.RowIndex].Cells[1].Value.ToString());
                if(value >= headers.Count)
                {
                    this.dataHeaders_Setting[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                }
            }
        }

        private void dataHeaders_Settings_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldvalue = int.Parse(this.dataHeaders_Setting[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        #region drag
        /*
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = this.dataHeaders_Setting.DoDragDrop(
                    this.dataHeaders_Setting.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = this.dataHeaders_Setting.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = this.dataHeaders_Setting.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                this.dataHeaders_Setting.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                this.dataHeaders_Setting.Rows.RemoveAt(rowIndexFromMouseDown);
                this.dataHeaders_Setting.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

            }
        }*/
        #endregion
    }
}
