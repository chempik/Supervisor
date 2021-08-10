
using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace Watcher
{
    [TrackAtribute("Track")]
    public class AutorunTrack : Track, ITrack
    {
        public AutorunTrack(IFileSystem fileSystem) : base(fileSystem) { }
        private bool _autorun = true;

        public void Traced(List<ShortProcess> shortProces, IConfig config)
        {
            if (_autorun)
            {
                var list = Data(config.Folder).Where(x => x.GetType() == typeof(AuTorunProc));
                var action = new ActionsProceses();
                foreach (var i in list)
                {
                    action.Start(i.Link);
                }
                _autorun = false;
            }
        }
    }
}
