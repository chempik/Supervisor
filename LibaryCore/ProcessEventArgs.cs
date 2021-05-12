using System;
using System.Diagnostics;

namespace LibaryCore
{
    public class ProcessEventArgs : EventArgs
    {
        public ShortProcess proc { get; set; }

        public ProcessEventArgs(Process process)
        {
            proc = new ShortProcess(process);
        }
    }
}
