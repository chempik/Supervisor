using System;
using LibaryCore;

namespace Setting
{
    public class Set
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public Proc[] Proceses { get; set; }

        public Set() { }
        public Set(string name, string link, Proc[] proceses)
        {
            Name = name;
            Link = link;
            Proceses = proceses;
        }
    }
}
