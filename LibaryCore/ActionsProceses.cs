using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using LibaryCore;
using ExceptionsLibrary;
using System.ComponentModel;
using System.IO;

namespace LibaryCore
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
            catch (FileNotFoundException ex)
            {
                throw new ExeNotFoundException("The PATH environment variable has a string containing quotes.", ex);
            }
            catch (ObjectDisposedException ex )
            {
                throw new DeletedObjectException("The process object has already been disposed.", ex);
            }
            catch (Win32Exception ex)
            {
                throw new OpeningFileException("The associated process could not be terminated.", ex);
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
            catch (Win32Exception ex)
            {
                throw new OpeningFileException("The associated process could not be terminated.", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new IdNotFoundException("You are attempting to call Kill() for a process that is running on a remote computer.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DeletedObjectException("There is no process associated with this Process object.", ex);
            }
            catch (AggregateException ex)
            {
                throw new ProcessCannotBeCompletedException("error in one of the threads, the system interrupts the execution of all threads", ex);
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
            catch (Win32Exception ex)
            {
                throw new OpeningFileException("The associated process could not be terminated.", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new IdNotFoundException("You are attempting to call Kill() for a process that is running on a remote computer.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DeletedObjectException("There is no process associated with this Process object.", ex);
            }
            catch (AggregateException ex)
            {
                throw new ProcessCannotBeCompletedException("error in one of the threads, the system interrupts the execution of all threads", ex);
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

        public bool KillOneProcess(string nameProceses)
        {
            Process[] Proc = Process.GetProcesses();
            try
            {
                Process killed = Proc.First(x => x.ProcessName == nameProceses);
                killed.Kill(false);
                return true;
            }
            catch (Win32Exception ex)
            {
                throw new OpeningFileException("The associated process could not be terminated.", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new IdNotFoundException("You are attempting to call Kill() for a process that is running on a remote computer.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new DeletedObjectException("There is no process associated with this Process object.", ex);
            }
            catch (AggregateException ex)
            {
                throw new ProcessCannotBeCompletedException("error in one of the threads, the system interrupts the execution of all threads", ex);
            }
        }
    }
}