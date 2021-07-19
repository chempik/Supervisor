using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watcher
{
    public class NumerosityTrack : Track
    {
        
        public NumerosityTrack(string folder) : base(folder)
        {
            _file = folder;
        }

        public void Traced(List<ShortProcess> SProc)
        {
            var list = (List<TrackProc>)Data().Where(x => x.GetType() == typeof(TrackProc));
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
