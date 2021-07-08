using System;
using System.Collections.Generic;
using System.Text;

namespace Setting
{
    public class CustomAtribute : Attribute
    {
        public string Name { get; set; }

        public CustomAtribute (string name)
        {
            Name = name;
        }
    }
}
