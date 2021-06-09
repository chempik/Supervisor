using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using LibaryCore;

namespace Watcher
{
    public class FileSystem
    {
        public void Create(ShortProcess proc, bool autorun, bool numerosityOn, int numerosity)
        {
            string fileName = $@"XmlFiles\{proc.Name}.xml";
            SerializeXml(proc, fileName,  autorun,  numerosityOn, numerosity);
        }

        private void SerializeXml(ShortProcess proc, string file, bool autorun, bool numerosityOn, int numerosity)
        {
            LittleProcess littleProcess = new LittleProcess(proc.Name,autorun,numerosityOn,numerosity,proc.Location);
            XmlSerializer formatter = new XmlSerializer(typeof(LittleProcess));
            
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, littleProcess);
            }
        }

        public void Check(string file)
        {
            string absulute = @"XmlFiles\" + file;
            File.Exists(absulute);
        }

        public LittleProcess Deserialize(string file)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(LittleProcess));
            XDocument xdoc = XDocument.Load(file);
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                LittleProcess newProces = (LittleProcess)formatter.Deserialize(fs);
                return newProces;
            }
        }
    }
}
