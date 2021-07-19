using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;

namespace Track
{
    public class HeadTrack
    {
        private readonly string _file;
        private ActionsProceses _actions = new ActionsProceses();

        public HeadTrack(string folder)
        {
            _file = folder;
        }

        private List<Proc> Data()
        {
            var watch = new Watch(_file);
            var set = watch.Deserialize();

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
    }
}
