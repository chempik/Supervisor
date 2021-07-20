using LibaryCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Watcher
{
    public  class DeserializeSet
    {
        private string _folder;
        private ActionsProceses _action = new ActionsProceses();

        public DeserializeSet(string folder)
        {
            _folder = folder;
        }

        public List<ShortProcess> CheckProceses()
        {
            var DeserializeList = new List<IDeserialize>();
            List<ShortProcess> list = _action.List();
            List<ShortProcess> sorted = new List<ShortProcess>();
            var deserializeSet = Validate();

            foreach (var i in deserializeSet)
            {
                foreach (var j in i.Deserialize(_folder))
                {
                    var tmp = list.Where(x => x.Name == j.Name);
                    sorted.AddRange(tmp);
                }
            }

            return sorted;
        }

        private List<IDeserialize> Validate()
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
