using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataViewer_v2
{
    public partial class Start : Form
    {

        private static readonly string version = "1.5.6";
        public Start()
        {
            
            InitializeComponent();
            this.lblVersion_Start.Text = this.lblVersion_Start.Text + version;
            this.lblVersion_Start.Location = new Point(this.Size.Width / 2 - this.lblVersion_Start.Size.Width / 2, this.lblVersion_Start.Location.Y);
        }

        private void btnStart_Start_Click(object sender, EventArgs e)
        {
            DataViewer dt = new DataViewer();
            dt.loadCSV();
            dt.loadData();
            dt.postLoad();
            dt.Show();
            this.Hide();
        }

        private void btnSettings_Start_Click(object sender, EventArgs e)
        {
            Settings st = new Settings(this);
            st.Show();
            this.Hide();
        }
    }
}
