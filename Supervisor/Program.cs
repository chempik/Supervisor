using System;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ConsoleTables;
using LibaryCore;
using System.Collections.Generic;
using System.Diagnostics;

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

        public void Start([Option(0)] string link)
        {
            actions.Start(link);
        }

        [Command("kill by id")]

        public void Kill([Option(0)] int id)
        {

            actions.Kill(id);
        }

        [Command("kill by name")]

        public void Kill([Option(0)] string name)
        {

            actions.Kill(name);
        }


        [Command("details by id")]

        public void Details([Option(0)] int id)
        {
            actions.GetDetails += delegate (object sender, ProcessEventArgs e)
            {
                Console.WriteLine($"Name - {e.proc.Name}, Id - {e.proc.Id}, memory usage - {e.proc.Memory}, location - {e.proc.Location}");
            };
            actions.Details(id);
        }

        [Command("details by name")]
        public void Details([Option(0)] string name)
        {
            actions.GetDetails += delegate (object sender, ProcessEventArgs e)
            {
                Console.WriteLine($"Name - {e.proc.Name}, Id - {e.proc.Id}, memory usage - {e.proc.Memory}, location - {e.proc.Location}");
            };
            actions.Details(name);
        }

        [Command("list")]
        public void List()
        {
            List<ShortProcess> list = actions.List();
            foreach (var i in list)
            {
                Console.WriteLine($"Name - {i.Name}, Id - {i.Id}");
            }

            /*Process[] a = Process.GetProcesses();
            List<Process> b = new List<Process>();

            foreach (var i in a)
            {
                b.Add(i);
            }

            foreach (var i in b)
            {
                Console.WriteLine(i.ProcessName);
            }*/
        }
    }
}
