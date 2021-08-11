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
        public IFileSystem _fileSystem;
        public XmlDeserialize(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public List<СompositionProc> Deserialize(string folder)
        {
            string[] FileArray = _fileSystem.Directory.GetFiles(folder);
            List<СompositionProc> list = new List<СompositionProc>();
            var fileSystem = new FileSystem(_fileSystem);

            foreach (string i in FileArray)
            {
                list.Add(fileSystem.Deserialize(i));
            }

            return list;
        }
    }
}