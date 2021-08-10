using System;
using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ConsoleTables;
using LibaryCore;
using Watcher;
using Setting;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Watcher.Interfece;
using System.IO.Abstractions;

namespace SupervisorConsole
{
    internal class Program : ConsoleAppBase
    {
        public Program(IWatch watch)
        {
            _watch = (Watch)watch;
        }

        private Watch _watch;
        private ActionsProceses actions = new ActionsProceses();
        private static string _folder = ConfigurationManager.AppSettings["Folder"];
        static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IConfig, Config>(Configer)
                    .AddScoped<IWatch, Watch>()
                    .AddSingleton<IDeserializeComposition,DeserializeComposition>()
                    .AddSingleton<IFileSystem,System.IO.Abstractions.FileSystem>());
            await hostBuilder.RunConsoleAppFrameworkAsync<Program>(args);
            
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
            var fileSystem = new Watcher.FileSystem();
            Proc[] procs = new Proc[] { new TrackProc() };
            var set = new СompositionProc("Skype", $@"C:\Program Files\WindowsApps\Microsoft.SkypeApp_15.73.124.0_x86__kzf8qxf38zg5c\Skype\Skype.exe", procs);

            fileSystem.Create(set);
        }

        [Command("Watch")]
        public void Watch()
        {
            var watch = _watch;
            bool ifStart = true;
            watch.Started += delegate (object sender, ProcesesEventArgs e)
            {
                var table = new ConsoleTable("Name", "Id");
                for (int i = 0; i < e.proc.Count; i++)
                {
                    table.AddRow(e.proc[i].Name, e.proc[i].Id);
                }
                table.Write();
            };

            watch.Opened += delegate (object sender, ProcesesEventArgs e)
            {
                for (int i = 0; i < e.proc.Count; i++)
                {
                    Console.WriteLine($"new proces, name - {e.proc[i].Name}, id - {e.proc[i].Id}");
                }
            };

            watch.Ended += delegate (object sender, ProcesesEventArgs e)
            {
                for (int i = 0; i < e.proc.Count; i++)
                {
                    Console.WriteLine($"stop proces, name - {e.proc[i].Name}, id - {e.proc[i].Id}");
                }
            };

            watch.Start(ref ifStart);
        }

        private static Config Configer(IServiceProvider arg)
        {
            return new Config {Folder = _folder };
        }
    }
}
