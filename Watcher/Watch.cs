using System;
using System.Collections.Generic;
using System.Threading;
using LibaryCore;
using System.Linq;
using System.Reflection;
using Watcher.Interfece;
using System.IO.Abstractions;

namespace Watcher
{
    public class Watch : IWatch
    {
        private int[] _oldId;
        private int _time = 5000;
        private List<ITrack> _track;
        private IDeserializeComposition _deserializeComposition;
        private IFileSystem _fileSystem;
        private IActionsProceses _actionsProceses;
        internal readonly IConfig Config;

        public event EventHandler<ProcesesEventArgs> Started;
        public event EventHandler<ProcesesEventArgs> Opened;
        public event EventHandler<ProcesesEventArgs> Ended;

        public Watch (IConfig config, IDeserializeComposition deserializeComposition, IFileSystem fileSystem, IActionsProceses actionsProceses)
        {
            Config = config;
            _deserializeComposition = deserializeComposition;
            _fileSystem = fileSystem;
            _actionsProceses = actionsProceses;
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

        private List<ITrack> GetRuleList()
        {
            var list = new List<ITrack>();
            var typeList = Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(x => x.GetCustomAttribute<TrackAtribute>(true) != null)
                           .ToList();

            foreach (var i in typeList)
            {
                var exemp = Activator.CreateInstance(i, _fileSystem, _actionsProceses) as ITrack;
                list.Add(exemp);
            }

            return list;
        }
        private List<ShortProcess> GetDeserialize()
        {
            var list = new List<ShortProcess>();
            list.AddRange(_deserializeComposition.CheckProceses(Config.Folder));
            return list;
        }

        public void Start(ref bool start)
        {
            List<ShortProcess> list = GetDeserialize();

            if (_oldId == null)
            {
                _track = GetRuleList();
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