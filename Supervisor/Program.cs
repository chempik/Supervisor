using System;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ConsoleTables;
using System.Diagnostics;
using LibaryCore;

namespace SupervisorConsole
{
    class Program : ConsoleAppBase
    {
        private ActionsProceses actions = new ActionsProceses();

        static async Task Main(string[] args)
        {
            // target T as ConsoleAppBase.
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
        }

        [Command("start")]
        
        public void Start(string link)
        {
            actions.Start(link);
        }

        [Command ("kill")]

        public void Kill(int id)
        {
            actions.Kill(id);
        }

        [Command("kill")]

        public void Kill(string name)
        {
            actions.Kill(name);
        }

        [Command ("details")]

        public void Details(int id)
        {
            actions.GetDetails += delegate (object sender, ProcessEventArgs e)
            {
                Console.WriteLine($"Name - {e.proc.ProcessName}, Id - {e.proc.Id}, memory usage - {e.proc.PeakWorkingSet64}, location - adapted in the future");
            };
            actions.Details(id);
        }

        [Command("details")]
        public void Details(string name)
        {
            actions.GetDetails += delegate (object sender, ProcessEventArgs e)
            {
                Console.WriteLine($"Name - {e.proc.ProcessName}, Id - {e.proc.Id}, memory usage - {e.proc.PeakWorkingSet64}, location - adapted in the future");
            };
            actions.Details(name);
        }

        [Command("list")]

        public void List()
        {
            Process[] array = Process.GetProcesses();
            foreach (var i in array)
            {
                Console.WriteLine($"Name - {i.ProcessName}, Id - {i.Id}");
            }
        }
    }
}
