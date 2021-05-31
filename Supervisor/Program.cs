using System;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ConsoleTables;
using LibaryCore;
using Watcher;


namespace SupervisorConsole
{
    internal class Program : ConsoleAppBase
    {
        private ActionsProceses actions = new ActionsProceses();

        static async Task Main(string[] args)
        {
            // target T as ConsoleAppBase.
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
        }

        /// <summary>
        /// starts process by exe file
        /// </summary>
        /// <param name="link">link to exe file</param>
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
        /// <summary>
        /// kill proceses by Id
        /// </summary>
        /// <param name="id">Id procrses which is required killed</param>
        [Command("KillById")]

        public void Kill([Option(0)] int id)
        {
           bool inspector = actions.Kill(id);
            if (inspector) Console.WriteLine("Process has been removed");
            else Console.WriteLine("Process not found");
        }
        /// <summary>
        /// kill proceses by Name
        /// </summary>
        /// <param name="name">name procrses which is required killed</param>
        [Command("KillByName")]

        public void Kill([Option(0)] string name)
        {
            bool inspector = actions.Kill(name);
            if (inspector) Console.WriteLine("Process has been removed");
            else Console.WriteLine("Process not found");
        }

        /// <summary>
        /// shows more detailed information about a particular process
        /// </summary>
        /// <param name="id">id of the process to be shown</param>
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
        /// <summary>
        /// shows more detailed information about a particular process
        /// </summary>
        /// <param name="name">name of the process to be shown</param>
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

        /// <summary>
        /// displays a list of all processes
        /// </summary>
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

        [Command("File")]
        public void fileTest()
        {
            FileSystem fileSystem = new FileSystem();
            fileSystem.Create(actions.Details("Taskmgr"));
        }
        
        [Command ("Watch")]
        public void Watch()
        {
            Watch a = new Watch();
            a.WatchForProcessStart();
            a.WatchForProcessEnd();
             

           /*var wmiClass = new System.Management.ManagementClass("Win32_ComputerSystem");
            foreach (var prop in wmiClass.Properties)
            {
                Console.WriteLine(prop.Name);

            }*/
            Console.ReadKey();
        }
    }
}
