using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using LibaryCore;
using System.Linq;
using Setting;
using ExceptionsLibrary;
using System.Diagnostics;
using System.Management;

namespace Watcher
{
    public class Watch : IWatch
    {
        private readonly string  _file;
        private ActionsProceses action = new ActionsProceses();
        private int[] _oldId;
        private string[] _oldName;
        private int _time = 5000;
        

        public event EventHandler<ProcesesEventArgs> Started;
        public event EventHandler<ProcesesEventArgs> Opened;
        public event EventHandler<ProcesesEventArgs> Ended;

        public Watch (string folder)
        {
            _file = folder;
        }

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

        internal List<Set> Deserialize()
        {
            string[] FileArray = Directory.GetFiles(_file);
            List<Set> list = new List<Set>();
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

            foreach (var i in Deserialize())
            {
                var tmp = list.Where(x => x.Name == i.Name);
                sorted.AddRange(tmp);
            }

            return sorted;
        }

        public void Start(ref bool start)
        {
            List<ShortProcess> list = CheckProceses();

            var track = new Track(_file);

            if (_oldId == null)
            {
                _oldId = list.Select(x => x.Id).ToArray();
                _oldName = list.Select(x => x.Name).ToArray();
                OnProcesesEventArgs(CreateProcesesEventArgs(list), Started);
            }

            else
            {
                int[] id = list.Select(x => x.Id).ToArray();
                string[] name = list.Select(x => x.Name).ToArray();
                var addId = id.Except(_oldId);
                Check(addId, list, Opened);

                var deleteId = _oldId.Except(id);
                Check(deleteId, list, Ended);

                _oldId = id;
                _oldName = track.AutorestartProc(name,_oldName);
            }
            
            //track.TrackProc(list);
            Thread.Sleep(_time);
            Start(ref start);
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