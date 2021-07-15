using System;
using System.ServiceProcess;

namespace MyServise
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase.Run(new LoggingService());
        }
    }
}
