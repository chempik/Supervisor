using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    public class TrackAtribute : Attribute
    {
        public string Name { get; set; }

        public TrackAtribute(string name)
        {
            Name = name;
        }
    }
}
