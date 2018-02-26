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

        public static VToken Create(this VToken vdf, params string[] keys)
        {
            VToken token = vdf;
            foreach (var key in keys)
            {
                if ((token as VObject).TryGetValue(key, out var value))
                    token = value.Value;
                else
                {
                    var val = new VObject();
                    (token as VObject).Add(key, val);
                    token = val;
                }
            }
            return token;
        }

        public static VProperty AddProperty(this VToken vdf, string key, object value = null)
        {
            var property = new VProperty(key, new VValue(value ?? ""));
            (vdf as VObject).Add(property);
            return property;
        }
    }
}
