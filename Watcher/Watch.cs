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
    public class Watch : IWatch
    {
        private const string  _file = @"XmlFiles";
        private List<ShortProcess> _proceses;
        private ActionsProceses action = new ActionsProceses();
        private int[] oldId;

        public event EventHandler<ProcesesEventArgs> Started;
        public event EventHandler<ProcesesEventArgs> Opened;
        public event EventHandler<ProcesesEventArgs> Ended;
        protected virtual void OnProcesesEventArgs (ProcesesEventArgs e, EventHandler<ProcesesEventArgs> occasion)
        {
            EventHandler<ProcesesEventArgs> raiseEvent = occasion;
            raiseEvent?.Invoke(this, e);
        }

        private ProcesesEventArgs CreateProcesesEventArgs (List<ShortProcess> proc)
        {
            ProcesesEventArgs args = new ProcesesEventArgs();
            args.proc = proc;
            return args;
        }

        private List<LittleProcess> Deserialize(string files)
        {
            string[] FileArray = Directory.GetFiles(files);
            List<LittleProcess> list = new List<LittleProcess>();
            var fileSystem = new FileSystem();

            foreach (string i in FileArray)
            {
                list.Add(fileSystem.Deserialize(i));
            }

            return list;
        }
        
        private List<ShortProcess> CheckProceses()
        {
            List<ShortProcess> list = action.List();
            List<ShortProcess> sorted = new List<ShortProcess>();

            foreach (var i in Deserialize(_file))
            {
                var tmp = list.Where(x => x.Name == i.Name);
                sorted.AddRange(tmp);
            }

            return sorted;
        }

        public void Start()
        {
            List<ShortProcess> list = CheckProceses();

            if (oldId == null)
            {
                oldId = list.Select(x => x.Id).ToArray();

                OnProcesesEventArgs(CreateProcesesEventArgs(list), Started);
            }

            else
            {
                int[] id = list.Select(x => x.Id).ToArray();
                
                var addId = id.Except(oldId);
                Check(addId, list, Opened);

                var deleteId = oldId.Except(id);
                Check(deleteId, list, Ended);
               
                oldId = id;
            }

            Thread.Sleep(5000);
            Start();
        }
        private void Check(IEnumerable<int> id, List<ShortProcess> sProc, EventHandler<ProcesesEventArgs> e)
        {
            List<ShortProcess> checkId = new List<ShortProcess>();
            foreach (var i in id)
            {
                var tmp = sProc.Where(x => x.Id == i);
                checkId.AddRange(tmp);
            }
            if (checkId.Count != 0)
            {
                OnProcesesEventArgs(CreateProcesesEventArgs(checkId), e);
            }
        }
    }
}