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
        private List<ShortProcess> _proceses = new List<ShortProcess>();
        private ActionsProceses action = new ActionsProceses();

        public event EventHandler<ProcesesEventArgs> started;
        public event EventHandler<ProcesesEventArgs> opened;
        public event EventHandler<ProcesesEventArgs> ended;
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
        
        private List<ShortProcess> CheckProceses()
        {
            List<ShortProcess> list = action.List();
            List<ShortProcess> sorted = new List<ShortProcess>();

            foreach (var i in Deserialization(_file))
            {
                var tmp = list.Where(x => x.Name == i.Name);
                sorted.AddRange(tmp);
            }

                // return sorted.Select(x => x.Id).ToArray();
                return sorted;

        }

        public void Start()
        {
            List<ShortProcess> list = CheckProceses();

            if (_proceses == null)
            {
                _proceses = list;
                
                OnProcesesEventArgs(CreateProcesesEventArgs(_proceses), started);
            }
            else
            {
                int[] id = list.Select(x => x.Id).ToArray();
                int[] oldId = _proceses.Select(x => x.Id).ToArray();
                List<ShortProcess> addProc = new List<ShortProcess>();
                List<ShortProcess> endProc = new List<ShortProcess>();
                
                var addId = id.Except(oldId);
                foreach (var i in addId)
                {
                    var tmp = list.Where(x => x.Id == i);
                    addProc.AddRange(tmp);
                }
                if (addProc != null)
                {
                    OnProcesesEventArgs(CreateProcesesEventArgs(addProc), opened);
                }

                var deleteId = oldId.Except(id);
                foreach (var i in deleteId)
                {
                    var tmp = _proceses.Where(x => x.Id == i);
                    endProc.AddRange(tmp);
                }
                if (endProc != null)
                {
                    OnProcesesEventArgs(CreateProcesesEventArgs(endProc), ended);
                }
            }

            Thread.Sleep(5000);
            Start();
        }
    }
}