using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LibaryCore;
using Setting;
using System.Reflection;
using System.IO.Abstractions;

namespace Watcher
{
    public abstract class Track : ITrack
    {
        protected IFileSystem _fileSystem;
        public Track(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        protected List<Proc> Data(string folder)
        {
            var watch = Deserialize();
            List<СompositionProc> сompositions = new List<СompositionProc>();

            foreach (var i in watch)
            {
                var tmp = i.Deserialize(folder);
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
                var exemp = Activator.CreateInstance(i, _fileSystem) as IDeserialize;
                list.Add(exemp);
            }

            return list;
        }
    }
}
