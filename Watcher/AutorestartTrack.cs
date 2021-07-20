using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watcher
{
    [TrackAtribute("Track")]
    public class AutorestartTrack : Track, ITrack
    {
        private string [] _oldName;

        public void Traced(List<ShortProcess> SProc, string folder)
        {
            var name = SProc.Select(x => x.Name);
            List<string> names = (List<string>)name;
            if (_oldName == null)
            {
                _oldName = name.ToArray();
            }

            else
            {
                var list = (List<AutorestartProc>)Data(folder).Where(x => x.GetType() == typeof(AutorestartProc));
                var action = new ActionsProceses();
                
                foreach (var i in list)
                {
                    if (_oldName.Contains(i.Name) && !names.Contains(i.Name))
                    {
                        action.Start(i.Link);
                        names.Add(i.Name);
                    }
                }

                _oldName = names.ToArray();
            }
        }
    }
}
