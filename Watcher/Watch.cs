using System;
using System.Collections.Generic;
using System.Threading;
using LibaryCore;
using System.Linq;
using System.Reflection;

namespace Watcher
{
    public class Watch : IWatch
    {
        private int[] _oldId;
        private int _time = 5000;
        private List<ITrack> _track;
        public readonly IConfig Config;

        public event EventHandler<ProcesesEventArgs> Started;
        public event EventHandler<ProcesesEventArgs> Opened;
        public event EventHandler<ProcesesEventArgs> Ended;

        public Watch (IConfig config)
        {
            Config = config;
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

        private List<ITrack> Validate()
        {
            var list = new  List<ITrack>();
            var tupeList = Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(x => x.GetCustomAttribute<TrackAtribute>(true) != null)
                           .ToList();

            foreach (var i in tupeList)
            {
                var exemp = Activator.CreateInstance(i) as ITrack;
                list.Add(exemp);
            }

            return list;
        }


        public void Start(ref bool start)
        {
            var deserializeComposition = new DeserializeComposition(Config.Folder);
            List<ShortProcess> list = deserializeComposition.CheckProceses();

            if (_oldId == null)
            {
                _track = Validate();
                _oldId = list.Select(x => x.Id).ToArray();
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
            }
            foreach (var i in _track)
            {
                i.Traced(list,Config);
            }
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