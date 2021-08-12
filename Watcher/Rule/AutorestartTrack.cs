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
    public class AutorestartTrack : Track, ITrack
    {
        public AutorestartTrack(IFileSystem fileSystem, IActionsProceses actionsProceses ) : base(fileSystem, actionsProceses) { }

        private string [] _oldName;

        public void Traced(List<ShortProcess> shortProces, IConfig config)
        {
            var name = shortProces.Select(x => x.Name).ToList();
            List<string> names = (List<string>)name;
            if (_oldName == null)
            {
                _oldName = name.ToArray();
            }

            else
            {
                var list = Data(config.Folder).Where(x => x.GetType() == typeof(AutorestartProc));
                
                foreach (var i in list)
                {
                    if (_oldName.Contains(i.Name) && !names.Contains(i.Name))
                    {
                        _actionsProceses.Start(i.Link);
                        names.Add(i.Name);
                    }
                }

                _oldName = names.ToArray();
            }
        }
    }
}
