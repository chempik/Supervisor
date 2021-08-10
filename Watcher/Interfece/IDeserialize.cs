using Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    public interface IDeserialize
    {
        public List<СompositionProc> Deserialize(string folder);
    }
}
