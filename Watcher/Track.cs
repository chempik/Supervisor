using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LibaryCore;

namespace Watcher
{
    public class Track
    {
        private const string _file = @"XmlFiles";
        private Watch _watch = new Watch();
        private ActionsProceses _actions = new ActionsProceses();

        public void Treker()
        {
            List<LittleProcess> list = _watch.Deserialize(_file);
            var sortedList = list.Where(x => x.NumerosityOn == true);
            var allProces = _actions.List();
        }

        public void Autorun()
        {
            List<LittleProcess> list = _watch.Deserialize(_file);
            var sortedList = list.Where(x => x.Autorun == true);
            
            foreach (var i in sortedList)
            {
                _actions.Start(i.Link);
            }
        }
    }
}
