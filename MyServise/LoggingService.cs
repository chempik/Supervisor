using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Text;
using Watcher;
namespace MyServise
{
    class LoggingService : ServiceBase
    {
        private const string _logFileLocation = @"C:\temp\servicelog.txt";
        private string _folder = ConfigurationManager.AppSettings["Folder"];
        private bool _start = true;
        private void Log(string logMessage)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
            File.AppendAllText(_logFileLocation, DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
        }

        protected override void OnStart(string[] args)
        {
            Log("Starting");
            var track = new AutorunTrack(_folder);
            track.Tracked();
            ForWatch(ref _start);
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _start = false;
            Log("Stopping");
            base.OnStop();
        }

        protected override void OnPause()
        {
            
            Log("Pausing");
            base.OnPause();
        }

        private void ForWatch(ref bool start)
        {
            var watch = new Watch(_folder);
            watch.Started += delegate (object sender, ProcesesEventArgs e)
            {
                for (int i = 0; i < e.proc.Count; i++)
                {
                    Log($"Name - {e.proc[i].Name}, ID - {e.proc[i].Id}");
                }
            };

            watch.Opened += delegate (object sender, ProcesesEventArgs e)
            {
                for (int i = 0; i < e.proc.Count; i++)
                {
                    Log($"new proces, name - {e.proc[i].Name}, id - {e.proc[i].Id}");
                }
            };

            watch.Ended += delegate (object sender, ProcesesEventArgs e)
            {
                for (int i = 0; i < e.proc.Count; i++)
                {
                    Log($"stop proces, name - {e.proc[i].Name}, id - {e.proc[i].Id}");
                }
            };

            watch.Start(ref start);
        }


    }
}
