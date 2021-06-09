using System;
using System.IO;
using System.Xml.Serialization;

namespace Watcher
{
    [Serializable]
    public class LittleProcess
    {
        public string Name { get; set; }
        public bool Autorun { get; set; }
        public bool NumerosityOn { get; set; }
        public int Numerosity { get; set; }
        public string Link { get; set; }


        public LittleProcess()
        {

        }

        public LittleProcess (string name, bool autorun, bool numerosityOn, int numerosity, string link)
        {
            Name = name;
            Autorun = autorun;
            NumerosityOn = numerosityOn;
            Numerosity = numerosity;
            Link = link;
        }
    }
}
