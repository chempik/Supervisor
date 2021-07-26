using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watcher
{
    [TrackAtribute("Track")]
    public class NumerosityTrack : Track, ITrack
    {
        public NumerosityTrack() : base() { }
        public void Traced(List<ShortProcess> shortProces)
        {
            var list = Data().Where(x => x.GetType() == typeof(TrackProc)).Cast<TrackProc>().ToList();
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
