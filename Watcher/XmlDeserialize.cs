using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace Watcher
{
    [CustomDeserializeAttribute("Deserialize")]
    public class XmlDeserialize : IDeserialize
    {
        public IFileSystem FileSystem;
        public XmlDeserialize(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }
        public List<СompositionProc> Deserialize(string folder)
        {
            string[] FileArray = FileSystem.Directory.GetFiles(folder);
            List<СompositionProc> list = new List<СompositionProc>();
            var fileSystem = new FileSystem();

            foreach (string i in FileArray)
            {
                list.Add(fileSystem.Deserialize(i));
            }

            return list;
        }
    }
}