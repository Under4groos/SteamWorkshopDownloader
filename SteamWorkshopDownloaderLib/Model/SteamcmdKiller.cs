using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamWorkshopDownloaderLib.Model
{
    public static class SteamcmdKiller
    {
 
        public static void Start()
        {
            if (!File.Exists("steamcmd_killer.exe"))
            {
                File.WriteAllBytes("steamcmd_killer.exe", Properties.Resource1.steamcmd_killer);
                 
            }
            System.Diagnostics.Process.Start("steamcmd_killer.exe");
        }
    }
}
