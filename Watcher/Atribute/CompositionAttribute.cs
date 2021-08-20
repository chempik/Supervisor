using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    public class CompositionAttribute : Attribute
    {
        public string Name { get; set; }
        public CompositionAttribute(string name)
        {
            Name = name;
        }
    }
}
