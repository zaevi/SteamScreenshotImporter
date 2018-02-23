using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SteamScreenshotImporter
{
    public partial class Main : Form
    {
        public static Action<string> Output;

        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SteamScreenImporter\";

        public static BindingList<string> ImageList = new BindingList<string>();

        dynamic Settings = new { SteamPath = string.Empty};

        public Main()
        {
            InitializeComponent();
            listBox.DataSource = ImageList;
            dataSet.Tables["UserGame"].Columns["name"].Expression = "Parent(appid).name";
            gameBox.DataSource = dataSet.Tables["UserGame"];

            userBox.SelectedValueChanged += (s, e)
                => dataSet.Tables["UserGame"].DefaultView.RowFilter = userBox.SelectedIndex>=0? "id=" + userBox.SelectedValue : "";

            Output = msg => outputBox.AppendText(Environment.NewLine + msg);
            SteamData.Data = dataSet;

            Directory.CreateDirectory(AppDataPath);
        }


        private void Main_Shown(object sender, EventArgs e)
        {
            if (!SteamData.Load(AppDataPath + "data.xml"))
                btnScan_LinkClicked(null, null);
            else
                LoadSettings();
        }

        string FindSteamPath(string path = null)
        {
            if (path == null)
            {
                try
                {
                    using (var key = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam"))
                        return Path.GetFullPath(key.GetValue("SteamPath") + "\\");
                }
                catch { MessageBox.Show("察觉不到Steam的存在! 请手动选择目录!"); }
            }
            else
            {
                if (Directory.Exists(Path.Combine(path, "userdata")))
                    return Path.GetFullPath(path + "\\");
                else
                    MessageBox.Show("没有找到userdata子目录, 请重新选择目录");
            }
            if (steamPathDialog.ShowDialog() == DialogResult.OK)
                return FindSteamPath(steamPathDialog.SelectedPath);
            return null;
        }

        private void btnScan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataSet.Clear();
            File.Delete(AppDataPath + "settings.xml");
            Steam.RootPath = FindSteamPath();
            Steam.Scan();
            SteamData.Save(AppDataPath + "data.xml");
        }

        string[] ImageExt = { ".bmp", ".jpeg", ".png", "jpg" };

        private bool IsImage(string file)
            => ImageExt.Any(e => file.EndsWith(e, StringComparison.OrdinalIgnoreCase)) && File.Exists(file);

        private void AddImages(string[] files)
        {
            foreach (var file in files)
            {
                if (IsImage(file)) ImageList.Add(file);
                else if (Directory.Exists(file)) AddImages(Directory.GetFiles(file));
            }
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
            => AddImages(e.Data.GetData(DataFormats.FileDrop) as string[]);

        private void Main_DragEnter(object sender, DragEventArgs e)
            => e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;

        private void btnAddImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (addImageDialog.ShowDialog() == DialogResult.OK)
                AddImages(addImageDialog.FileNames);
        }

        private void btnAddFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (addFolderDialog.ShowDialog() == DialogResult.OK)
                AddImages(new[] { addFolderDialog.SelectedPath });
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if(userBox.SelectedIndex == -1 || gameBox.SelectedIndex == -1)
            {
                Output("请选择用户和游戏!");
                return;
            }

            int userId = (int)userBox.SelectedValue, appId = (int)gameBox.SelectedValue;
            var screenshotDir = string.Format(@"{0}userdata\{1}\760\remote\{2}\screenshots\", Steam.RootPath, userId, appId);

            Steam.ImportImages(ImageList, screenshotDir);

            Output("导入成功");
            ImageList.Clear();
            SaveSettings();
        }

        private void LoadSettings()
        {
            var xmlPath = AppDataPath + "settings.xml";
            if (!File.Exists(xmlPath)) return;

            var xml = XElement.Load(xmlPath);
            userBox.SelectedValue = xml.Element("LastUser").Value;
            gameBox.SelectedValue = xml.Element("LastGame").Value;
            addImageDialog.InitialDirectory = xml.Element("LastFileDialogPath").Value;
            addFolderDialog.SelectedPath = xml.Element("LastFolderDialogPath").Value;

        }

        private void SaveSettings()
        {
            var xmlPath = AppDataPath + "settings.xml";
            new XElement("Settings",
                new XElement("LastUser", userBox.SelectedValue),
                new XElement("LastGame", gameBox.SelectedValue),
                new XElement("LastFileDialogPath", addImageDialog.InitialDirectory),
                new XElement("LastFolderDialogPath", addFolderDialog.SelectedPath))
                .Save(xmlPath);
        }
    }
}
