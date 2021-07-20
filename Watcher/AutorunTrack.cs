﻿
using LibaryCore;
using Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watcher
{
    [TrackAtribute("Track")]
    public class AutorunTrack : Track, ITrack
    {
        private bool _autorun = true;
        public void Traced(List<ShortProcess> SProc, string folder)
        {
            if (_autorun)
            {
                var list = Data(folder).Where(x => x.GetType() == typeof(AuTorunProc));
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
