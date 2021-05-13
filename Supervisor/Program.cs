using System;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ConsoleTables;
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

        [Command("Start")]

        public void Start([Option(0)] string link)
        {
            ShortProcess added = actions.Start(link);
            if (added != null)
            {
                var table = new ConsoleTable("Name", "Id", "Memory Usage", "Location");
                table.AddRow(added.Name, added.Id, added.Memory, added.Location);
                table.Write();
            }
            else Console.WriteLine("exe not found");
        }

        [Command("KillById")]

        public void Kill([Option(0)] int id)
        {
           bool inspector = actions.Kill(id);
            if (inspector) Console.WriteLine("Process has been removed");
            else Console.WriteLine("Process not found");
        }

        [Command("KillByName")]

        public void Kill([Option(0)] string name)
        {
            bool inspector = actions.Kill(name);
            if (inspector) Console.WriteLine("Process has been removed");
            else Console.WriteLine("Process not found");
        }


        [Command("DetailsById")]

        public void Details([Option(0)] int id)
        {
            ShortProcess proc = actions.Details(id);
            if (proc != null)
            {
                var table = new ConsoleTable("Name", "Id", "Memory Usage", "Location");
                table.AddRow(proc.Name, proc.Id, proc.Memory, proc.Location);
                table.Write();
            }
            else Console.WriteLine("Process not found");
        }

        [Command("DetailsByName")]
        public void Details([Option(0)] string name)
        {
            ShortProcess proc = actions.Details(name);
            if (proc != null)
            {
                var table = new ConsoleTable("Name", "Id", "Memory Usage", "Location");
                table.AddRow(proc.Name, proc.Id, proc.Memory, proc.Location);
                table.Write();
            }
            else Console.WriteLine("Process not found");
        }

        [Command("List")]
        public void List()
        {
            var table = new ConsoleTable("Name", "Id");
            foreach (var i in actions.List())
            {
                table.AddRow(i.Name, i.Id);
            }
            table.Write();
        }
    }
}
