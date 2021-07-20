using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Watcher
{
    public class XmlWatch
    {
        private string _folder;
        private ActionsProceses _action = new ActionsProceses();

        public XmlWatch(string folder)
        {
            _folder = folder;
        }

        internal List<Set> Deserialize()
        {
            string[] FileArray = Directory.GetFiles(_folder);
            List<Set> list = new List<Set>();
            var fileSystem = new FileSystem();

            foreach (string i in FileArray)
            {
                list.Add(fileSystem.Deserialize(i));
            }

            return list;
        }

        internal List<ShortProcess> CheckProceses()
        {
            List<ShortProcess> list = _action.List();
            List<ShortProcess> sorted = new List<ShortProcess>();

            foreach (var i in Deserialize())
            {
                var tmp = list.Where(x => x.Name == i.Name);
                sorted.AddRange(tmp);
            }

            return sorted;
        }
    }
}
