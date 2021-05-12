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
            actions.Kill(id);
        }

        [Command("KillByName")]

        public void Kill([Option(0)] string name)
        {

            actions.Kill(name);
        }


        [Command("DetailsById")]

        public void Details([Option(0)] int id)
        {
            ShortProcess proc = actions.Details(id);
            var table = new ConsoleTable("Name", "Id", "Memory Usage", "Location");
            table.AddRow(proc.Name, proc.Id, proc.Memory, proc.Location);
            table.Write();
        }

        [Command("DetailsByName")]
        public void Details([Option(0)] string name)
        {
            ShortProcess proc = actions.Details(name);
            var table = new ConsoleTable("Name", "Id", "Memory Usage", "Location");
            table.AddRow(proc.Name, proc.Id, proc.Memory, proc.Location);
            table.Write();
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
