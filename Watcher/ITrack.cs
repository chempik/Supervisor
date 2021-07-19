using LibaryCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    interface ITrack
    {
        public void Traced(List<ShortProcess> SProc, string folder) { }
    }
}
