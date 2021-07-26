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

        public AutorestartTrack() : base() { }
        public void Traced(List<ShortProcess> shortProces)
        {
            var name = shortProces.Select(x => x.Name).ToList();
            List<string> names = (List<string>)name;
            if (_oldName == null)
            {
                _oldName = name.ToArray();
            }

            else
            {
                var list = Data().Where(x => x.GetType() == typeof(AutorestartProc));
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
