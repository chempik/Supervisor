using LibaryCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Setting
{
    [Serializable]
    [CustomAtribute("Track")]
    [XmlInclude(typeof(AutorestartProc))]
    public class AutorestartProc : Proc
    {
        public bool Restart { get; set; }
        public AutorestartProc(string name, string link)
        {
            Name = name;
            Link = link;
            Restart = true;
        }
        public AutorestartProc(ShortProcess shortProcess)
        {
            Name = shortProcess.Name;
            Link = shortProcess.Location;
            Restart = true;
        }
        public AutorestartProc() { }
    }
}
