﻿using Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher
{
    interface IDeserialize
    {
        public List<Set> Deserialize(string folder);
    }
}