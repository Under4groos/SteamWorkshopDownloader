using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamWorkshopDownloaderLib.Model
{

    public enum StatusSteamCmd
    {
        CheckingUpdate,
        CheckingUpdate2,
        NULL,
    }

    public class ResSteamCmd : IDisposable
    {
        public Action InstallationСompleted;
        public Action<string> LogInstall;
         
        StatusSteamCmd StatusSteamCmd;
        public string _install { get; set; } = "App/steamcmd.exe";
        FileInfo fio;
        DirectoryInfo dio;
        DirectoryInfo dir_addons_dio;
        string dir_addons = "Addons install";
        Process process = new Process();

        public bool IsINstalling
        {
            get; set;
        } = false;
        public ResSteamCmd()
        {
            fio = new FileInfo(_install);
            dir_addons_dio = new DirectoryInfo(dir_addons);
            if (!Directory.Exists(dir_addons_dio.FullName))
                Directory.CreateDirectory(dir_addons_dio.FullName);
        }
        public void Install()
        {
            
            
            if (!IsValid)
            {
                if(!Directory.Exists(fio.Directory.FullName))
                    Directory.CreateDirectory(fio.Directory.FullName);
                File.WriteAllBytes(_install, Properties.Resource1.steamcmd);
            }
            else
            {
                  
            }
        }
        public string req(string id_game , string id)
        {
            return $"+login anonymous quit +force_install_dir \"{dir_addons_dio.FullName}\" +workshop_download_item {id_game} {id} validate +quit";
        }
        public void Login()
        {
            if (IsValid)
            {
                
                process.FileName = this._install;
                process.Args = $"+login anonymous +quit";
                process.EventDataReceivedHandler += (o, e) =>
                {
                    string DATA_ = e.Data;
                  
                    if(DATA_ == null || DATA_.Trim() == "-- type 'quit' to exit --" )
                    {
                        IsINstalling = true;
                        if (InstallationСompleted != null)
                            InstallationСompleted();
                        return;
                    }
                    if (Regex.IsMatch(DATA_, @"\[[\s\S]+?\][\s\S]+?\.\.\."))
                    {
                        if(LogInstall != null)
                            LogInstall(DATA_.ToUTF8());
                    }
                };
                process.Start();
            }  
        }
        public void DownloadAddon( string id  ,string id_game)
        {
            if(id.IsBValidString() || id_game.IsBValidString())
            {
                process.FileName = this._install;
                process.Args = req(id_game, id);
                process.EventDataReceivedHandler += (e, d) =>
                {
                    if(d.Data == null)
                    {
                        foreach (var item in Directory.GetDirectories(Path.Combine(dir_addons_dio.FullName , @"steamapps\workshop\content")))
                        {
                             
                            Directory.Move(item, Path.Combine(dir_addons_dio.FullName , new DirectoryInfo(item).Name));
                        }
                        Console.WriteLine("NULL");
                    } 
                    Console.WriteLine(d.Data);
                };
                process.Start();
            }
            
        }
        public void Dispose()
        {
            process.Dispose();
        }

        public bool IsValid
        {
            get
            {
                return File.Exists(_install);
            }
        }
    }
}
