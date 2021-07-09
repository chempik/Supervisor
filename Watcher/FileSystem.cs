using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using LibaryCore;
using Setting;
namespace Watcher
{
    public class FileSystem
    {
        internal Set Deserialize(string file)
        {
            var atters = new XmlAttributes();
            var over = new XmlAttributeOverrides();
            var list = new TypeSet().TypeList;
            foreach (var i in list)
            {
                var attr = new XmlElementAttribute();
                attr.ElementName = i.Name;
                attr.Type = i;
                atters.XmlElements.Add(attr);
            }

            over.Add(typeof(Set), nameof(Set.Proceses), atters);

            var s = new XmlSerializer(typeof(Set), over);

            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                return (Set)s.Deserialize(fs);
            }
        }

        private void Serialize(Set setProc, string file)
        {
            var attrs = new XmlAttributes();
            var list = new TypeSet().TypeList;
            foreach (var i in list)
            {
                var attr = new XmlElementAttribute();
                attr.ElementName = i.Name;
                attr.Type = i;
                attrs.XmlElements.Add(attr);
            }

            var attrOver = new XmlAttributeOverrides();
            attrOver.Add(typeof(Set), nameof(Set.Proceses), attrs);
            var s = new XmlSerializer(typeof(Set), attrOver);

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                s.Serialize(fs, setProc);
            }
        }

        public void Create (Set set)
        {
            string fileName = $@"XmlFiles\{set.Name}.xml";
            Serialize(set, fileName);
        }
    }
}
