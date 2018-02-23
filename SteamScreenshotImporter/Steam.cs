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

        // <k,v>
        public static Dictionary<string, string> Settings = null;

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

        public static bool ImportImages(IEnumerable<string> imageList, string screenshotDir)
        {
            Directory.CreateDirectory(screenshotDir);

            var time = DateTime.Now.ToString("yyyyMMddHHmmss_");
            int n = 1;

            foreach(var imagePath in imageList)
            {
                var name = time + (n++) + ".jpg";
                using (var image = Image.FromFile(imagePath))
                {
                    image.Save(screenshotDir + name, ImageFormat.Jpeg);
                    using (var thumbnail = GetThumbnail(image))
                        thumbnail.Save(screenshotDir + @"thumbnails\" + name, ImageFormat.Jpeg);
                }

                Main.Output(Path.GetFileName(imagePath) + " => " + name);
            }

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

        private const int Version = 1;

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
