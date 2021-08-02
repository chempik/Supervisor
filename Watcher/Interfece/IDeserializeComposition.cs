using LibaryCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher.Interfece
{
    public interface IDeserializeComposition
    {
        public List<ShortProcess> CheckProceses(string folder);
    }
}
