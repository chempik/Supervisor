using LibaryCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Watcher.Interfece;
using System.IO.Abstractions;

namespace Watcher
{
    [CompositionAttribute("Deserialize")]
    public  class DeserializeComposition : IDeserializeComposition
    {
        private IFileSystem _fileSystem;
        private ActionsProceses _action = new ActionsProceses();
        public DeserializeComposition(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public List<ShortProcess> CheckProceses(string folder)
        {
            var DeserializeList = new List<IDeserialize>();
            List<ShortProcess> list = _action.List();
            List<ShortProcess> sorted = new List<ShortProcess>();
            var ListDeserialize = GetListDeserialize();

            foreach (var i in ListDeserialize)
            {
                foreach (var j in i.Deserialize(folder))
                {
                    var tmp = list.Where(x => x.Name == j.Name);
                    sorted.AddRange(tmp);
                }
            }

            return sorted;
        }

        private List<IDeserialize> GetListDeserialize()
        {
            var list = new List<IDeserialize>();
            var tupeList = Assembly.GetExecutingAssembly()
                           .GetTypes()
                           .Where(x => x.GetCustomAttribute<CustomDeserializeAttribute>(true) != null)
                           .ToList();

            foreach (var i in tupeList)
            {
                var exemp = Activator.CreateInstance(i,_fileSystem) as IDeserialize;
                list.Add(exemp);

            }

            return list;
        }
    }
}
