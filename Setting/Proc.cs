using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using LibaryCore;

namespace Setting
{
    [Serializable]
    [XmlInclude(typeof(Proc))]
    [CustomAtribute("Track")]
    public class Proc : IProc
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public Proc (string name, string link)
        {
            Name = name;
            Link = link;
        }
        public Proc (ShortProcess shortProcess)
        {
            Name = shortProcess.Name;
            Link = shortProcess.Location;
        }
        public Proc() { }
    }
}