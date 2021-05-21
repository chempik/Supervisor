using System;
using System.IO;
using System.Xml.Serialization;

namespace Watcher
{
    [Serializable]
    public class LittleProcess
    {
        public string Name { get; set; }

        public LittleProcess()
        {

        }

        public LittleProcess (string name)
        {
            Name = name;
        }
    }
}
