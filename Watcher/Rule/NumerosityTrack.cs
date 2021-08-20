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
        public NumerosityTrack(IFileSystem fileSystem, IActionsProceses actionsProceses) : base(fileSystem, actionsProceses) { }
        public void Traced(List<ShortProcess> shortProces, IConfig config)
        {
            var list = Data(config.Folder).Where(x => x.GetType() == typeof(TrackProc)).Cast<TrackProc>().ToList();
            foreach (var i in list)
            {
                var proces = shortProces.Where(x => x.Name == i.Name);
                var count = proces.Count();
                if (i.Track > 0)
                {
                    while (count > i.Track)
                    {
                        _actionsProceses.KillOneProcess(i.Name);
                        count--;
                    }
                }
                else
                {
                    while (count > 0)
                    {
                        _actionsProceses.KillOneProcess(i.Name);
                        count--;
                    }
                }
            }
        }
    }
}
