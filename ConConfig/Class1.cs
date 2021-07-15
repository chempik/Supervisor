using System;
using System.Configuration;
using System.Collections.Specialized;

namespace ConConfig
{
    public class Class1
    {
        public string sAttr = ConfigurationManager.AppSettings["Folder"];
        public void test()
        {

        }
    }
}
