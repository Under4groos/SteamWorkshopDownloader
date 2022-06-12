using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamWorkshopDownloaderLib.Model
{
    public static class Str
    {
        public static string ToUTF8(this string str)
        {
            var utf8 = Encoding.UTF8;
            byte[] utfBytes = utf8.GetBytes(str);
            return utf8.GetString(utfBytes, 0, utfBytes.Length);
        }
        public static bool IsBValidString(this string str)
        {
            str = str.Trim();
            return !(str == "" || str == string.Empty);
        }
    }
}
