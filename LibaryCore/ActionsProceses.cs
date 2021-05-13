using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using LibaryCore;
using ExceptionsLibrary;
using System.ComponentModel;
using System.IO;

namespace SupervisorConsole
{
    /// <summary>
    /// a class that provides access to work with processes
    /// </summary>
    public class ActionsProceses
    {
        private ShortProcess CreateShortProcess(Process process)
        {
            ShortProcess sProc = new ShortProcess();
            sProc.Name = process.ProcessName;
            sProc.Id = process.Id;
            sProc.Memory = process.PeakWorkingSet64;
            try
            {
                sProc.Location = process.MainModule.FileName;
            }
            catch (InvalidOperationException)
            {
                sProc.Location = "n/a";
            }
            catch (Win32Exception)
            {
                sProc.Location = "n/a";
            }
            catch (NotSupportedException)
            {
                sProc.Location = "n/a";
            }
            return sProc;
        }
        /// <summary>
        /// starts process by exe file
        /// </summary>
        /// <param name="link">link to exe file</param>
        /// <returns>returns this process</returns>
        public ShortProcess Start(string link)
        {
            try
            {
                Process added = Process.Start(link);
                return CreateShortProcess(added);
            }
            catch (FileNotFoundException)
            {
                throw new ExeNotFoundException();
            }
            catch (ObjectDisposedException)
            {
                throw new ExeNotFoundException();
            }
            catch (Win32Exception)
            {
                throw new ExeNotFoundException();
            }
        }
        /// <summary>
        /// kill proceses by name
        /// </summary>
        /// <param name="nameProceses">name procrses which is required killed</param>
        /// <returns>return true if proceses has benn killed</returns>
        public bool Kill(string nameProceses)
        {
            Process[] Proc = Process.GetProcesses();
            try
            {
                Process killed = Proc.First(x => x.ProcessName == nameProceses);
                killed.Kill(true);
                return true;
            }
            catch (Win32Exception) 
            {
                throw new IdNotFoundException();
            }
            catch (NotSupportedException)
            {
                throw new IdNotFoundException();
            }
            catch (InvalidOperationException)
            {
                throw new IdNotFoundException();
            }
            catch (AggregateException)
            {
                throw new IdNotFoundException();
            }
        }

        /// <summary>
        /// kill proceses by Id
        /// </summary>
        /// <param name="idProceses">Id procrses which is required killed</param>
        /// <returns>return true if proceses has benn killed</returns>
        public bool Kill(int idProceses)
        {
            Process[] Proc = Process.GetProcesses();
            try
            {
                Process killed = Proc.First(x => x.Id == idProceses);
                killed.Kill(true);
                return true;
            }
            catch (Win32Exception)
            {
                throw new NameNotFoundException();
            }
            catch (NotSupportedException)
            {
                throw new NameNotFoundException();
            }
            catch (InvalidOperationException)
            {
                throw new NameNotFoundException();
            }
            catch (AggregateException)
            {
                throw new NameNotFoundException();
            }
        }
        /// <summary>
        /// shows more detailed information about a particular process
        /// </summary>
        /// <param name="idProceses">id of the process to be shown</param>
        /// <returns>proceses</returns>
        public ShortProcess Details(int idProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process show = Proc.First(x => x.Id == idProceses);
            return CreateShortProcess(show);
        }
        /// <summary>
        /// shows more detailed information about a particular process
        /// </summary>
        /// <param name="nameProceses">Name of the process to be shown</param>
        /// <returns>proceses</returns>
        public ShortProcess Details(string nameProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process show = Proc.First(x => x.ProcessName == nameProceses);
            return CreateShortProcess(show);
        }
        /// <summary>
        /// displays a list of all processes
        /// </summary>
        /// <returns>returned list with proceses</returns>
        public List<ShortProcess> List()
        {
            List<ShortProcess> list = new List<ShortProcess>();

            foreach (var i in Process.GetProcesses())
            {
                ShortProcess temporary = CreateShortProcess(i);
                list.Add(temporary);
            }
            return list;
        }
    }
}