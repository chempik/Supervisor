using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


namespace LibaryCore
{
    public class ShortProcess
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long Memory { get; set; }
        public string Location { get; set; }
        public ShortProcess(Process process)
        {
            Name = process.ProcessName;
            Id = process.Id;
            Memory = process.PeakWorkingSet64;
            Location = process.MainModule.FileName;
        }
    }
}
