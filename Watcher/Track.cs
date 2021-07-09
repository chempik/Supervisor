using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LibaryCore;
using Setting;

namespace Watcher
{
    public class Track
    {
        private const string _file = @"XmlFiles";
        private ActionsProceses _actions = new ActionsProceses();

        private List<Proc> Data()
        {
            var watch = new Watch();
            var set = watch.Deserialize(_file);

            var proceses = new List<Proc>();

           foreach (var i in set)
            {
                foreach (var j in i.Proceses)
                {
                    j.Name = i.Name;
                    j.Link = i.Link;
                    proceses.Add(j);
                }
            }

            return proceses;
        }

        public void Autorun()
        {
            var list = Data().Where(x => x.GetType() == typeof(AuTorunProc));
            foreach (var i in list)
            {
                _actions.Start(i.Link);
            }
        }

        public string[] AutorestartProc (string[] Name, string[] oldName)
        {
            List<string> names = Name.ToList();
            List<AutorestartProc> list = (List<AutorestartProc>)Data().Where(x => x.GetType() == typeof(AutorestartProc));
            foreach (var i in list)
            {
                if (oldName.Contains(i.Name) && !Name.Contains(i.Name))
                {
                    _actions.Start(i.Link);
                    names.Add(i.Name);
                }
            }
            return names.ToArray();
        }

        public void TrackProc(List<ShortProcess> SProc)
        {
            List<TrackProc> list = (List<TrackProc>)Data().Where(x => x.GetType() == typeof(TrackProc));

            foreach (var i in list)
            {
                var proces = SProc.Where(x => x.Name == i.Name);
                while (proces.Count() > i.Track)
                {
                        _actions.KillOneProcess(i.Name);
                }
            }
        }
    }
}
