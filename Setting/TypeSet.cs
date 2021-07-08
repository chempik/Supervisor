using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Setting
{
    public class TypeSet
    {
        public Type[] TypeList = Assembly.GetExecutingAssembly()
                 .GetTypes()
                 .Where(x => x.GetCustomAttribute<CustomAtribute>(true) != null)
                 .ToArray();
    }
}
