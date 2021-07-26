using System;
using LibaryCore;

namespace Setting
{
    public class СompositionProc
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public Proc[] Proceses { get; set; }

        public СompositionProc() { }
        public СompositionProc(string name, string link, Proc[] proceses)
        {
            Name = name;
            Link = link;
            Proceses = proceses;
        }
    }
}
