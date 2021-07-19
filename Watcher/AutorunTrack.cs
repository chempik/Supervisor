
using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watcher
{
    public class AutorunTrack : Track
    {
        public AutorunTrack(string folder) : base (folder)
        {
            _file = folder;
        }

        public void Tracked()
        {
            var list = Data().Where(x => x.GetType() == typeof(AuTorunProc));
            var action = new ActionsProceses();
            foreach (var i in list)
            {
                action.Start(i.Link);
            }
        }
    }
}
