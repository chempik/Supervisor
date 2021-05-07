using System;
using System.Diagnostics;

namespace LibaryCore
{
    public class ProcessEventArgs : EventArgs
    {
        public Process proc { get; set; }
    }
}
