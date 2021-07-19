using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watcher
{
    public class AutorestartTrack : Track
    {
        public AutorestartTrack(string folder) : base(folder)
        {
            _file = folder;
        }

        public string[] Tracked(string[] Name, string[] oldName)
        {
            List<string> names = Name.ToList();
            var list = (List<AutorestartProc>)Data().Where(x => x.GetType() == typeof(AutorestartProc));
            var action = new ActionsProceses();
            foreach (var i in list)
            {
                if (oldName.Contains(i.Name) && !Name.Contains(i.Name))
                {
                    action.Start(i.Link);
                    names.Add(i.Name);
                }
            }
            return names.ToArray();
        }
    }
}
