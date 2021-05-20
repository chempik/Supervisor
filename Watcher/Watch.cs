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
    public class Watch
    {
        public event EventHandler<ProcesesEventArgs> Ploddered;
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
            string [] fileList = Directory.GetFiles($@"C:\Users\rsemeniak\Downloads\Supervisor\Supervisor\Supervisor\Watcher\XmlFiles");

            if (fileList.Length == 0)
            {
                throw new XmlFileNotFoundException();
            }

            List<LittleProcess> list = Deserializetion(fileList);
            await Task.Run(() => Warden(list));
        }

        public void End()
        {
            on = false;
        }

        private List<LittleProcess> Deserializetion(string[] files)
        {
            List<LittleProcess> list = new List<LittleProcess>();
            var fileSystem = new FileSystem();

            foreach (string i in files)
            {
                list.Add(fileSystem.Deserializetion(i));
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
                OnProcesesEventArgs(CreateProcesesEventArgs(sortProc.Count(), i.Name),Ploddered);
            }
        }
        private void Warden(List<LittleProcess> processes)
        {
            while (on)
            {
                Plodder(processes);
                Thread.Sleep(5000);
            }
        }
    }
}
