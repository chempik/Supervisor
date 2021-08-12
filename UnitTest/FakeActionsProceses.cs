using LibaryCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    internal class FakeActionsProceses : IActionsProceses
    {
        public ShortProcess Details(int idProceses)
        {
            throw new NotImplementedException();
        }

        public ShortProcess Details(string nameProceses)
        {
            throw new NotImplementedException();
        }

        public bool Kill(string nameProceses)
        {
            throw new NotImplementedException();
        }

        public bool Kill(int idProceses)
        {
            throw new NotImplementedException();
        }

        public bool KillOneProcess(string nameProceses)
        {
            throw new NotImplementedException();
        }

        public List<ShortProcess> List()
        {
            throw new NotImplementedException();
        }

        public ShortProcess Start(string link)
        {
            throw new NotImplementedException();
        }
    }
}
