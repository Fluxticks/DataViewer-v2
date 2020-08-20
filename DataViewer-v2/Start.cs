using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataViewer_v2
{
    public partial class Start : Form
    {
        private string version = "1.5.0";
        private string data_folder  = "\\data";

        public Start()
        {
            var tmp = Properties.Settings.Default.file_amount;
            Properties.Settings.Default.file_amount = 4;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.file_amount = tmp;
            Properties.Settings.Default.Save();
            checkForUpdate();
            InitializeComponent();
            this.lblVersion_Start.Text = this.lblVersion_Start.Text + version;
            this.lblVersion_Start.Location = new Point(this.Size.Width / 2 - this.lblVersion_Start.Size.Width / 2, this.lblVersion_Start.Location.Y);

            
        }

        private async Task checkForUpdate() {
            using (var mgr = new UpdateManager(@"C:\Users\Benji\Documents\Coding\VisualStudio\Projects\DataViewer-v2\Releases"))
            {
               var res = await mgr.CheckForUpdate();
               var next = res.FutureReleaseEntry.Filename.ToSemanticVersion().ToString();
               var prev = res.CurrentlyInstalledVersion.Filename.ToSemanticVersion().ToString();
               string folder = Application.StartupPath.Replace(@"GGWLApp\app-" + prev, "") + "DataViewer_v2";

                if (res.FutureReleaseEntry != res.CurrentlyInstalledVersion) {
                //if (res.FutureReleaseEntry == res.CurrentlyInstalledVersion){
                   
                    var opt = MessageBox.Show("There is an update available, would you like to download it? \n" + prev + "->" + next, "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (opt == DialogResult.Yes)
                    { 
                        var cwd = Application.StartupPath;
                        var next_path = Application.StartupPath.Replace(prev, next);
                        await mgr.UpdateApp();
                        if(Directory.Exists(Application.StartupPath + data_folder))
                        {
                            Directory.Move(Application.StartupPath + data_folder, next_path + data_folder);
                        }
                        var dir = folder + "\\" + getLastDir(folder);
                        var conf = dir + @"\" + prev + @".0\user.config";
                        if (File.Exists(folder + @"\user.config")) {
                            File.Delete(folder + @"\user.config");
                        }
                        File.Copy(conf, folder + @"\user.config");

                        MessageBox.Show("Update complete! Re-Open application to use the updated version.");
                        Application.Exit();
                    }
                }
                else
                {
                    getSettings(folder, prev);
                }
            }
        }

        private string getLastDir(string folder)
        {
            
            var di = new DirectoryInfo(folder);
            var dir = di.EnumerateDirectories()
                                .OrderBy(d => d.CreationTime)
                                .Select(d => d.Name)
                                .ToList().Last();
            return dir;
        }

        private void getSettings(string path, string prev) {
            if (File.Exists(path + @"\user.config")) {
                var dir = getLastDir(path);
                File.Delete(path + "\\" + dir + "\\" + prev + @".0\user.config");
                File.Copy(path + @"\user.config", path +"\\"+ dir +"\\"+prev+@".0\user.config");
                File.Delete(path + @"\user.config");
                Properties.Settings.Default.Reload();
            }
        }

        private async Task checkGitHubUpdate() {
            using (var manager = UpdateManager.GitHubUpdateManager(@"C:\Users\Benji\Documents\Coding\VisualStudio\Projects\Release"))
            {
                await manager.Result.UpdateApp();
            }
        }

        private void btnStart_Start_Click(object sender, EventArgs e)
        {
            DataViewer dt = new DataViewer(data_folder);
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
