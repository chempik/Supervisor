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
        private readonly string _folder;
        public Track()
        {
            _folder = Singl.getInstance().Folder;
        }
        protected List<Proc> Data()
        {
            var xmlWatch = new XmlDeserialize();
            var set = xmlWatch.Deserialize(_folder);

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
