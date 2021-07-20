using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    public class DeserializeAttribute : Attribute
    {
        public string Name { get;set; }
        public DeserializeAttribute(string name)
        {
            Name = name;
        }
    }
}
