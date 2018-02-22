using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Gameloop.Vdf;
using System.Data;

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
    }

    class SteamData
    {

        public static DataSet Data;

        private const int Version = 1;

        public static bool Save(string path)
        {
            return false;
        }

        public static bool Load(string path)
        {
            return false;
        }

        public static void New()
        {
            Steam.Settings = new Dictionary<string, string>();
        }
    }
}
