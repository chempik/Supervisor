using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LibaryCore;
using Setting;
using System.Reflection;

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
            var watch = Deserialize();
            List<СompositionProc> сompositions = new List<СompositionProc>();

            foreach (var i in watch)
            {
                var tmp = i.Deserialize(_folder);
                сompositions.AddRange(tmp);
            }

            var proceses = new List<Proc>();

            foreach (var i in сompositions)
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

        private List<IDeserialize> Deserialize()
        {
            var list = new List<IDeserialize>();
            var tupeList = Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(x => x.GetCustomAttribute<CustomDeserializeAttribute>(true) != null)
                           .ToList();

            foreach (var i in tupeList)
            {
                var exemp = Activator.CreateInstance(i) as IDeserialize;
                list.Add(exemp);
            }

            return list;
        }
    }
}
