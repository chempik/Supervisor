using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    public class Singl
    {
        private static Singl instance;
        public string Folder;

        public void Init (string folder)
        {
            Folder = folder;
        }
        private Singl()
        { }

        public static Singl getInstance()
        {
            if (instance == null)
                instance = new Singl();
            return instance;
        }
    }
}
