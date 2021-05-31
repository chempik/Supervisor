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
        private const string  _file = @"XmlFiles";
        private const string scope = @"root\CIMV2";
        private ManagementEventWatcher startProcWatcher;
        private ManagementEventWatcher endProcWatcher;
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

        private List<LittleProcess> Deserialization(string files)
        {
            string[] FileArray = Directory.GetFiles(files);
            List<LittleProcess> list = new List<LittleProcess>();
            var fileSystem = new FileSystem();

            foreach (string i in FileArray)
            {
                list.Add(fileSystem.Deserialization(i));
            }
            return list;
        }


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

        private string QueryString(List<LittleProcess> list)
        {


            string query =
           "SELECT * FROM __InstanceCreationEvent "
           + "WITHIN 1 WHERE " +
             "TargetInstance isa \"Win32_Process\""
            + "   AND TargetInstance.Name = 'Opera.exe'";
            //+ "   AND TargetInstance.Name = '" + "Taskmgr.exe" + "'";
            //Taskmgr.exe
            /*string queryString =
                @"SELECT TargetInstance
                 FROM __InstanceCreationEvent 
                WITHIN  .025  
                WHERE TargetInstance ISA 'Win32_Process' 
                   AND TargetInstance.Name IN ({0})";*/
            // return string.Format(queryString, id);
            return query;
        }
        private void Wait(ManagementEventWatcher procWatcher)
        {
            while (true)
            {
                ManagementBaseObject e = procWatcher.WaitForNextEvent();
            }
        }

        public async void WatchForProcessStart()
        {
            // The dot in the scope means use the current machine


            // Create a watcher and listen for events
            startProcWatcher = new ManagementEventWatcher(scope, QueryString(Deserialization(_file)));
            startProcWatcher.EventArrived += ProcessStarted;
            startProcWatcher.Start();
            await Task.Run(()=> Wait(startProcWatcher));
        }

        private void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = targetInstance.Properties["Name"].Value.ToString();
            //string processId = targetInstance.Properties["SystemSKUNumber"].Value.ToString();
           // string exePath = targetInstance.Properties["ExecutablePath"].Value.ToString();
            string action = "BEGAN";
            Console.WriteLine(String.Format("{0}|{1}|{2}", DateTime.Now, action, processName));
        }

        public async void WatchForProcessEnd()
        {
            // Create a watcher and listen for events
            endProcWatcher = new ManagementEventWatcher(scope, QueryString(Deserialization(_file)));
            endProcWatcher.EventArrived += ProcessEnded;
            endProcWatcher.Start();
            await Task.Run(() => Wait(endProcWatcher));
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