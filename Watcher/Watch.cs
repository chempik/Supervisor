using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LibaryCore;
using System.Linq;
using ExceptionsLibrary;
using System.Diagnostics;
using System.Management;

namespace Watcher
{
    public class Watch
    {

       /* public  void Start()
        {
            string [] fileList = Directory.GetFiles($@"netcoreapp3.1\XmlFiles");

            if (fileList.Length == 0)
            {
                throw new XmlFileNotFoundException();
            }

            List<LittleProcess> list = Deserialization(fileList);
          //  await Task.Run(() => Warden(list));
        }*/

        private List<LittleProcess> Deserialization(string[] files)
        {
            List<LittleProcess> list = new List<LittleProcess>();
            var fileSystem = new FileSystem();

            foreach (string i in files)
            {
                list.Add(fileSystem.Deserialization(i));
            }
            return list;
        }

        public EventHandler<EventArgs> started;
        public EventHandler<EventArgs> finished;
        private ManagementEventWatcher startProcWatcher;
        private ManagementEventWatcher endProcWatcher;
        /*
        protected virtual void OnEvent(EventArgs e, EventHandler<EventArgs> eve)
        {
            EventHandler<EventArgs> raiseEvent = eve;
            raiseEvent?.Invoke(this, e);
        }

        public void StopAllWatchers()
        {
            startProcWatcher.Stop();
            endProcWatcher.Stop();
        }*/

        public void WatchForProcessStart()
        {
            string queryString =
                "SELECT TargetInstance" +
                "  FROM __InstanceCreationEvent " +
                "WITHIN  .025 " +
                " WHERE TargetInstance ISA 'Win32_Process' "
                //+ "   AND TargetInstance.Name = '" + processName + "'";
                + "   AND TargetInstance.Name like '%'";

            // The dot in the scope means use the current machine
            string scope = @"\\.\root\CIMV2";

            // Create a watcher and listen for events
            startProcWatcher = new ManagementEventWatcher(scope, queryString);
            startProcWatcher.EventArrived += ProcessStarted;
            startProcWatcher.Start();

        }

        private void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = targetInstance.Properties["Name"].Value.ToString();
            string exePath = targetInstance.Properties["ExecutablePath"].Value.ToString();
            string action = "BEGAN";
            Console.WriteLine(String.Format("{0}|\t{1}|\t{2}|\t{3}", DateTime.Now, action, processName, exePath));
        }

        private void WatchForProcessEnd()
        {
            string queryString =
                "SELECT TargetInstance" +
                "  FROM __InstanceDeletionEvent " +
                "WITHIN  .025 " +
                " WHERE TargetInstance ISA 'Win32_Process' "
                //+ "   AND TargetInstance.Name = '" + processName + "'";
                + "   AND TargetInstance.Name like '%'";
            // The dot in the scope means use the current machine
            string scope = @"\\.\root\CIMV2";

            // Create a watcher and listen for events
            endProcWatcher = new ManagementEventWatcher(scope, queryString);
            endProcWatcher.EventArrived += ProcessEnded;
            endProcWatcher.Start();
        }

        private void ProcessEnded(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = targetInstance.Properties["Name"].Value.ToString();
            string action = "ENDED";

            Console.WriteLine(String.Format("{0}|{1}|{2}", DateTime.Now, action, processName));
        }


    }
}