using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using LibaryCore;

namespace Setting
{
    [Serializable]
    [CustomAtribute("Track")]
    [XmlInclude(typeof(TrackProc))]
    public class TrackProc : Proc
    {
        public int Track { get; set; }

        public TrackProc(string name, string link, int track)
        {
            Name = name;
            Link = link;
            Track = track;
        }
        public TrackProc(string name, string link)
        {
            Name = name;
            Link = link;
            Track = 10;
        }
        public TrackProc(ShortProcess shortProcess)
        {
            Name = shortProcess.Name;
            Link = shortProcess.Location;
            Track = 10;
        }
        public TrackProc() { }
    }
}

