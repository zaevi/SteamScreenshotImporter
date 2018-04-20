using Gameloop.Vdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using SteamScreenshotImporter.Localization;

namespace SteamScreenshotImporter
{
    static class Steam
    {
        public static string RootPath { get => SteamData.Data.Tables["Path"].Rows[0][0].ToString(); set => SteamData.Data.Tables["Path"].Rows.Add(value); }

        public static DataTable Users { get => SteamData.Data.Tables["Users"]; }
        public static DataTable UserGame { get => SteamData.Data.Tables["UserGame"]; }

        private static Dictionary<int, string> AppName = null;

        public static bool Scan()
        {
            if (RootPath == null) return false;

            AppName = AppName ?? GetAppList();

            Main.Output(text.getlocalmsg);

            var appsDir = Path.Combine(RootPath, "steamapps");
            var localGames = Directory.GetFiles(appsDir, "appmanifest_*.acf").Select(s => int.Parse(s.Split(new[] { '_', '.' })[1])).ToArray();

            Main.Output(text.getusermsg);

            var userdataDir = Path.Combine(RootPath, "userdata");
            foreach (var user in Directory.GetDirectories(userdataDir)
                .Select(d => new { Id = Path.GetFileName(d), CfgPath = d + @"\config\localconfig.vdf" })
                .Where(u => File.Exists(u.CfgPath)))
            {
                int.TryParse(user.Id, out int userID);
                var vdf = VdfConvert.Deserialize(File.ReadAllText(user.CfgPath)).Value as VObject;
                var name = vdf.GetValue("friends").GetString("PersonaName");
                Users.Rows.Add(userID, name);

                var userAppidList = (vdf.GetValue("Software", "Valve", "Steam", "Apps") as VObject).Children().Select(a => int.Parse(a.Key));
                foreach (var appid in userAppidList)
                {
                    if(!AppName.TryGetValue(appid, out string appName)) appName = "(" + appid +")";
                    UserGame.Rows.Add(userID, appid, appName, localGames.Contains(appid));
                }

                // Main.Output($"[{name}] 游戏:{UserGame.Compute("count(id)", "id=" + userID)} (本地:{UserGame.Compute("count(id)", "id=" + userID + " and local=true")})");
            }
            Main.Output(string.Format(text.scantotal, Users.Rows.Count, UserGame.AsEnumerable().Select(r => r["appid"]).Distinct().Count()));

            return true;
        }

        public static bool ImportImages(IEnumerable<string> imageList, int userId, int appId)
        {
            var screenshotDir = $@"{RootPath}userdata\{userId}\760\remote\{appId}\screenshots\";
            Directory.CreateDirectory(screenshotDir + @"thumbnails\");

            var time = DateTime.Now.ToString("yyyyMMddHHmmss_");
            int n = 1;

            var vdfPath = RootPath + @"\userdata\" + userId + @"\760\screenshots.vdf";
            File.Copy(vdfPath, vdfPath + ".bak", true);

            var vdf = VdfConvert.Deserialize(File.ReadAllText(vdfPath));
            var appVdf = vdf.Value.Create(appId.ToString()) as VObject;
            var appVdfIndex = appVdf.Count;

            foreach (var imagePath in imageList)
            {
                var name = time + (n++) + ".jpg";
                Size size;
                using (var image = Image.FromFile(imagePath))
                {
                    image.Save(screenshotDir + name, ImageFormat.Jpeg);
                    using (var thumbnail = GetThumbnail(image))
                        thumbnail.Save(screenshotDir + @"thumbnails\" + name, ImageFormat.Jpeg);
                    size = image.Size;
                }

                var token = new VObject();
                token.AddProperty("type", 1);
                token.AddProperty("filename", appId + "/screenshots/" + name);
                token.AddProperty("thumbnail", appId + "/screenshots/thumbnails/" + name);
                token.AddProperty("vrfilename");
                token.AddProperty("imported", 1);
                token.AddProperty("width", size.Width);
                token.AddProperty("height", size.Height);
                token.AddProperty("gameid", appId);
                token.AddProperty("creation", (int)(File.GetLastWriteTime(imagePath) - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds);
                token.AddProperty("caption");
                token.AddProperty("Permissions", 2);
                token.AddProperty("hscreenshot");
                appVdf.Add((appVdfIndex++).ToString(), token);

                Main.Output(Path.GetFileName(imagePath) + " => " + name);
            }

            Main.Output(text.savevdf);
            File.WriteAllText(vdfPath, VdfConvert.Serialize(vdf));

            return true;
        }

        private static Image GetThumbnail(Image origin)
        {
            var height = origin.Height * 200 / origin.Width;
            Image img = new Bitmap(origin, new Size(200, height));
            return img;
        }

        private static Dictionary<int, string> GetAppList()
        {
            var path = Main.AppDataPath + "appList.dat";
            if(!File.Exists(path))
            {
                Main.Output(text.getapplist);
                var appList = XDocument.Load("http://api.steampowered.com/ISteamApps/GetAppList/v2?format=xml")
                    .Root.Element("apps").Elements()
                    .ToDictionary(e => int.Parse(e.Element("appid").Value), e => e.Element("name").Value);
                using (var file = File.OpenWrite(path))
                    new BinaryFormatter().Serialize(file, appList);
                return appList;
            }
            else
            {
                using (var file = File.OpenRead(path))
                    return new BinaryFormatter().Deserialize(file) as Dictionary<int, string>;
            }
        }
    }

    static class SteamData
    {
        public static DataSet Data;

        public static bool Save(string path)
        {
            Data.WriteXml(path);
            return true;
        }

        public static bool Load(string path)
        {
            if (!File.Exists(path)) return false;
            Data.ReadXml(path);
            return true;
        }
    }
}
