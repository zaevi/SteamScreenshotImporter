using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameloop.Vdf;

namespace SteamScreenshotImporter
{
    static class VdfExtensions
    {
        public static VToken GetValue(this VObject vdf, string key)
        {
            if (vdf.TryGetValue(key, out var value))
                return value.Value;
            return null;
        }

        public static VToken GetValue(this VToken vdf, string key)
            => (vdf as VObject).GetValue(key);

        public static string GetString(this VToken vdf, string key)
            => (vdf as VObject).GetValue(key).ToString();

        public static VToken GetValue(this VToken vdf, params string[] keys)
        {
            VToken token = vdf;
            foreach (var key in keys)
            {
                if ((token as VObject).TryGetValue(key, out var value))
                    token = value.Value;
                else return null;
            }

            return token;
        }
    }
}
