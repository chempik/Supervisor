using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LibaryCore;
using Setting;

namespace Watcher
{
    public abstract class Track
    {

        protected List<Proc> Data(string folder)
        {
            var watch = new Watch(folder);
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
