using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace SteamScreenshotImporter
{
    public partial class Main : Form
    {
        public static Action<string> Output;

        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SteamScreenImporter\";

        public Main()
        {
            InitializeComponent();
            dataSet.Tables["UserGame"].Columns["name"].Expression = "Parent(appid).name";
            gameBox.DataSource = dataSet.Tables["UserGame"];

            userBox.SelectedValueChanged += (s, e) 
                => dataSet.Tables["UserGame"].DefaultView.RowFilter = "id="+ userBox.SelectedValue;

            Output = msg => outputBox.AppendText(Environment.NewLine + msg);
            SteamData.Data = dataSet;

            Directory.CreateDirectory(AppDataPath);
        }


        private void Main_Shown(object sender, EventArgs e)
        {
            if (!SteamData.Load(AppDataPath + "data.xml"))
                btnScan_LinkClicked(null, null);
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        string FindSteamPath(string path=null)
        {
            if (path == null)
            {
                try
                {
                    using (var key = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam"))
                        return key.GetValue("SteamPath").ToString();
                }
                catch { MessageBox.Show("察觉不到Steam的存在! 请手动选择目录!"); }
            }
            else
            {
                if (Directory.Exists(Path.Combine(path, "userdata")))
                    return path;
                else
                    MessageBox.Show("没有找到userdata子目录, 请重新选择目录");
            }
            if(steamPathDialog.ShowDialog() == DialogResult.OK)
                return FindSteamPath(steamPathDialog.SelectedPath);
            return null;
        }

        private void btnScan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Steam.RootPath = FindSteamPath();
            Steam.Scan();
            SteamData.Save(AppDataPath + "data.xml");
        }
    }
}
