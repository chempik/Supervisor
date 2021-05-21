using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LibaryCore;
using System.Linq;
using ExceptionsLibrary;

namespace Watcher
{
    public class Watch : IWatch
    {
        public event EventHandler<ProcesesEventArgs> Started;
        private bool on = true;

        private ProcesesEventArgs CreateProcesesEventArgs(int counter, string name)
        {
            ProcesesEventArgs args = new ProcesesEventArgs();
            args.Counter = counter;
            args.Name = name;
            return args;
        }

        protected virtual void OnProcesesEventArgs(ProcesesEventArgs e, EventHandler<ProcesesEventArgs> occasion)
        {
            EventHandler<ProcesesEventArgs> raiseEvent = occasion;
            raiseEvent?.Invoke(this, e);
        }

        public async void Start()
        {
            string [] fileList = Directory.GetFiles($@"netcoreapp3.1\XmlFiles");

            if (fileList.Length == 0)
            {
                throw new XmlFileNotFoundException();
            }

            List<LittleProcess> list = Deserialization(fileList);
            await Task.Run(() => Warden(list));
        }

        public void End()
        {
            on = false;
        }

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

        private void Plodder(List<LittleProcess> processes)
        {
            var action = new ActionsProceses();
            List<ShortProcess> shortProcesses = new List<ShortProcess>();

            foreach (var i in processes) 
            { 
                shortProcesses = action.List();
                var sortProc = shortProcesses.Where(x => x.Name == i.Name);
                OnProcesesEventArgs(CreateProcesesEventArgs(sortProc.Count(), i.Name),Started);
            }
        }
        private void Warden(List<LittleProcess> processes)
        {
            //fix this!
            /* while (on)
            {
                Plodder(processes);
                Thread.Sleep(5000);
            } */
        }
    }
}
