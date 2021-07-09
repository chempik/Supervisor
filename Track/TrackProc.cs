using System;
using LibaryCore;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using Watcher;

namespace Track
{
    public class TrackProc
    {
        public void Serialize(ShortProcess proc, bool autorun, int numerosity)
        {
            string fileName = $@"XmlFiles\{proc.Name}.xml";
            TrackProceses track = new TrackProceses(proc.Name, autorun, numerosity);
            XmlSerializer formater = new XmlSerializer(typeof(TrackProceses));

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formater.Serialize(fs, track);
            }
        }
    }
}
