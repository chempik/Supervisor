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
        
        public void Traced(List<ShortProcess> SProc, string folder)
        {
            var list = (List<TrackProc>)Data(folder).Where(x => x.GetType() == typeof(TrackProc));
            var action = new ActionsProceses();
            foreach (var i in list)
            {
                var proces = SProc.Where(x => x.Name == i.Name);
                while (proces.Count() > i.Track)
                {
                    action.KillOneProcess(i.Name);
                }
            }
        }
    }
}
