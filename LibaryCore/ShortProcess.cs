using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


namespace LibaryCore
{
    public class ShortProcess
    {
        public string Name { get; internal set; }
        public int Id { get; internal set; }
        public long Memory { get; internal set; }
        public string Location { get; internal set; }
    }
}
