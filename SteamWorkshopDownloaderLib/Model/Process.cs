using System;
using System.Diagnostics;
using System.IO;

namespace SteamWorkshopDownloaderLib.Model
{
    public class Process : IDisposable
    {
        //public Action<string> OutputDataReceived;
        public DataReceivedEventHandler EventDataReceivedHandler;
        System.Diagnostics.Process process;
        public string FileName
        {
            get; set;
        }
        public string Args
        {
            get; set;
        } = "";
        public void Start()
        {
            if (!File.Exists(FileName))
                return;
            process = System.Diagnostics.Process.Start(new ProcessStartInfo()
            { 
                   FileName = FileName,
                   Arguments = Args,
                   UseShellExecute = false,
                   CreateNoWindow = true,
                   RedirectStandardOutput = true,
            });
            process.BeginOutputReadLine();
            process.OutputDataReceived += EventDataReceivedHandler;
          
            process.Start();
        }
        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            process?.Close();
            process?.Dispose();
        }
    }
}
