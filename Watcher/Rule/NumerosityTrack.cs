using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace Watcher
{
    [TrackAtribute("Track")]
    public class NumerosityTrack : Track, ITrack
    {
        public NumerosityTrack(IFileSystem fileSystem) : base(fileSystem) { }
        public void Traced(List<ShortProcess> shortProces, IConfig config)
        {
            var list = Data(config.Folder).Where(x => x.GetType() == typeof(TrackProc)).Cast<TrackProc>().ToList();
            var action = new ActionsProceses();
            foreach (var i in list)
            {
                var proces = shortProces.Where(x => x.Name == i.Name);
                while (proces.Count() > i.Track)
                {
                    action.KillOneProcess(i.Name);
                }
            }
        }
    }
}
