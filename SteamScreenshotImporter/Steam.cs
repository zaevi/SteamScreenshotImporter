using Gameloop.Vdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml;

namespace SteamScreenshotImporter
{
    static class Steam
    {
        public static string RootPath { get; set; } = null;

        public static DataTable Users { get => SteamData.Data.Tables["Users"]; }
        public static DataTable Games { get => SteamData.Data.Tables["Games"]; }
        public static DataTable UserGame { get => SteamData.Data.Tables["UserGame"]; }

        public static bool Scan()
        {
            if (RootPath == null) return false;

            Main.Output("获取已安装的游戏...");

            var appsDir = Path.Combine(RootPath, "steamapps");
            foreach(var cfg in Directory.GetFiles(appsDir, "appmanifest_*.acf"))
            {
                var vdf = VdfConvert.Deserialize(File.ReadAllText(cfg)).Value as VObject;
                var appid = int.Parse(vdf.GetString("appid"));
                Games.Rows.Add(appid, vdf.GetString("name"));
            }

            Main.Output("获取玩家信息...");

            var userdataDir = Path.Combine(RootPath, "userdata");
            var localGames = from r in Games.AsEnumerable() select (int)r["appid"];
            foreach (var user in Directory.GetDirectories(userdataDir)
                .Select(d => new { Id = Path.GetFileName(d), CfgPath = d + @"\config\localconfig.vdf" })
                .Where(u => File.Exists(u.CfgPath)))
            {
                int.TryParse(user.Id, out int userID);
                var vdf = VdfConvert.Deserialize(File.ReadAllText(user.CfgPath)).Value as VObject;
                var name = vdf.GetValue("friends").GetString("PersonaName");

                Users.Rows.Add(userID, name);

                var apps = vdf.GetValue("Software", "Valve", "Steam", "Apps");
                foreach (var appid in (apps as VObject).Children().Select(a => int.Parse(a.Key)).Intersect(localGames))
                    UserGame.Rows.Add(userID, appid);
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                
                Main.Output(name.ToString());
            }

            return true;
        }

        public static bool ImportImages(IEnumerable<string> imageList, int userId, int appId)
        {
            var screenshotDir = string.Format(@"{0}userdata\{1}\760\remote\{2}\screenshots\", RootPath, userId, appId);
            Directory.CreateDirectory(screenshotDir);

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

            Main.Output("写入截图记录...");
            File.WriteAllText(vdfPath, VdfConvert.Serialize(vdf));

            return true;
        }

        private static Image GetThumbnail(Image origin)
        {
            var width = origin.Width * 200 / origin.Height;
            Image img = new Bitmap(origin, new Size(width, 200));
            return img;
        }
    }

    class SteamData
    {
        public static DataSet Data;

        public static bool Save(string path)
        {
            using (var writer = XmlWriter.Create(path, new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Auto, Indent = true }))
            {
                Data.WriteXml(writer);
                writer.WriteElementString("SteamPath", Steam.RootPath);
            }
            return true;
        }

        public static bool Load(string path)
        {
            if (!File.Exists(path)) return false;
            using (var reader = XmlReader.Create(path, new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Auto, IgnoreWhitespace = true }))
            {
                Data.ReadXml(reader);
                Steam.RootPath = reader.ReadElementContentAsString();
            }
            return true;
        }
    }
}
