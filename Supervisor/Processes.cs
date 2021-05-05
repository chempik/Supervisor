using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Supervisor
{
    public class Processes
    {
        public string Name { get; set; }
        public int PID { get; set; }
        public float Memory { get; set; }

        public Processes (string name, int pid, float memory)
        {
            Name = name;
            PID = pid;
            Memory = memory;
        }
    }
}
