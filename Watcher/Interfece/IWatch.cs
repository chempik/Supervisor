using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Watcher
{
    public interface IWatch
    {
        public void Start(CancellationToken token);
    }
}
