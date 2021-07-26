using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    public class CustomDeserializeAttribute : Attribute
    {
        public string Name { get;set; }
        public CustomDeserializeAttribute(string name)
        {
            Name = name;
        }
    }
}
