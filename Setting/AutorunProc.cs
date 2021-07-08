using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using LibaryCore;

namespace Setting
{
    [Serializable]
    [CustomAtribute("Track")]
    [XmlInclude(typeof(AuTorunProc))]
    public class AuTorunProc : Proc
    {
        public bool Autorun { get; set; }
        public AuTorunProc(string name, string link)
        {
            Name = name;
            Link = link;
            Autorun = true;
        }
        public AuTorunProc(ShortProcess shortProcess)
        {
            Name = shortProcess.Name;
            Link = shortProcess.Location;
            Autorun = true;
        }
        public AuTorunProc() { }
    }
}

