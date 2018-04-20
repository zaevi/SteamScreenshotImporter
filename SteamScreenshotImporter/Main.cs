using Microsoft.Win32;
using SteamScreenshotImporter.Localization;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SteamScreenshotImporter
{
    public partial class Main : Form
    {
        public static Action<string> Output;

        public static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SteamScreenImporter\";

        private static readonly BindingList<string> ImageList = new BindingList<string>();

        public Main()
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;

            listBox.DataSource = ImageList;

            Output = msg => outputBox.AppendText(Environment.NewLine + msg);
            SteamData.Data = dataSet;

            Directory.CreateDirectory(AppDataPath);

            label1.Text = text.selectuser;
            label2.Text = text.selectapp;
            btnScan.Text = text.scan;
            checkShowAll.Text = text.showall;
            label3.Text = text.dragmsg;
            btnAddImage.Text = text.addimage;
            btnAddFolder.Text = text.addfolder;
            btnClear.Text = text.clear;
            btnImport.Text = text.import;
            linkSteam.Text = text.profile;
            linkGithub.Text = text.github;
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            var filter = "local=" + !checkShowAll.Checked +
                (userBox.SelectedIndex >= 0 ? " and id=" + userBox.SelectedValue : "");
            dataSet.Tables["UserGame"].DefaultView.RowFilter = filter;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            if (!SteamData.Load(AppDataPath + "data.xml"))
                btnScan_LinkClicked(null, null);
            userBox.DataSource = dataSet.Tables["Users"];
            gameBox.DataSource = dataSet.Tables["UserGame"];
            LoadSettings();
        }

        string FindSteamPath(string path = null)
        {
            if (path == null)
            {
                try
                {
                    using (var key = Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam"))
                        return Path.GetFullPath(key?.GetValue("SteamPath") + "\\");
                }
                catch { MessageBox.Show(text.nosteamdir); }
            }
            else
            {
                if (Directory.Exists(Path.Combine(path, "userdata")))
                    return Path.GetFullPath(path + "\\");
                else
                    MessageBox.Show(text.errsteamdir);
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

        private readonly string[] ImageExt = { ".bmp", ".jpeg", ".png", "jpg" };

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

        private void btnClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => ImageList.Clear();

        private void btnImport_Click(object sender, EventArgs e)
        {
            if(userBox.SelectedIndex == -1 || gameBox.SelectedIndex == -1)
            {
                Output(text.wrongselect);
                return;
            }
            if(ImageList.Count == 0)
            {
                Output(text.noimage);
                return;
            }

            int userId = (int)userBox.SelectedValue, appId = (int)gameBox.SelectedValue;
            Steam.ImportImages(ImageList, userId, appId);

            Output(text.imported);
            ImageList.Clear();
            SaveSettings();
        }

        private void LoadSettings()
        {
            var xmlPath = AppDataPath + "settings.xml";
            if (!File.Exists(xmlPath)) return;

            var xml = XElement.Load(xmlPath);
            userBox.SelectedValue = xml.Element("LastUser")?.Value;
            checkShowAll.Checked = bool.Parse(xml.Element("ShowAllApp")?.Value);
            gameBox.SelectedValue = xml.Element("LastGame")?.Value;
            addImageDialog.InitialDirectory = xml.Element("LastFileDialogPath")?.Value;
            addFolderDialog.SelectedPath = xml.Element("LastFolderDialogPath")?.Value;
        }

        private void SaveSettings()
        {
            var xmlPath = AppDataPath + "settings.xml";
            new XElement("Settings",
                new XElement("LastUser", userBox.SelectedValue),
                new XElement("LastGame", gameBox.SelectedValue),
                new XElement("LastFileDialogPath", addImageDialog.InitialDirectory),
                new XElement("LastFolderDialogPath", addFolderDialog.SelectedPath),
                new XElement("ShowAllApp", checkShowAll.Checked))
                .Save(xmlPath);
        }

        private void linkSteam_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => System.Diagnostics.Process.Start("http://steamcommunity.com/id/zaeworks/");

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            => System.Diagnostics.Process.Start("https://github.com/Zaeworks/SteamScreenshotImporter");
    }
}
