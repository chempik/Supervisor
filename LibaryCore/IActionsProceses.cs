using System;
using System.Collections.Generic;
using System.Text;

namespace LibaryCore
{
    public interface IActionsProceses
    {
        public ShortProcess Start(string link);
        public bool Kill(string nameProceses);
        public bool Kill(int idProceses);
        public ShortProcess Details(int idProceses);
        public ShortProcess Details(string nameProceses);
        public List<ShortProcess> List();
        public bool KillOneProcess(string nameProceses);
    }
}
