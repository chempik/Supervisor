using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Watcher
{
    [DeserializeAttribute("Deserialize")]
    public class XmlDeserialize : IDeserialize
    {
        public List<Set> Deserialize(string folder)
        {
            string[] FileArray = Directory.GetFiles(folder);
            List<Set> list = new List<Set>();
            var fileSystem = new FileSystem();

            foreach (string i in FileArray)
            {
                list.Add(fileSystem.Deserialize(i));
            }

            return list;
        }
    }
}