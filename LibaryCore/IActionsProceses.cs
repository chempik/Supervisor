using System;
using System.Collections.Generic;
using System.Text;

namespace LibaryCore
{
    /// <summary>
    ///interface to work with processes
    /// </summary>
    public interface IActionsProceses
    {
        /// <summary>
        /// starts process by exe file
        /// </summary>
        /// <param name="link"> link to exe file</param>
        /// <returns></returns>
        public ShortProcess Start(string link);

        /// <summary>
        /// kill proceses by name
        /// </summary>
        /// <param name="nameProceses">name procrses which is required killed</param>
        /// <returns>return true if proceses has benn killed</returns>
        public bool Kill(string nameProceses);

        /// <summary>
        /// kill proceses by id
        /// </summary>
        /// <param name="idProceses">id procrses which is required killed</param>
        /// <returns>return true if proceses has benn killed</returns>
        public bool Kill(int idProceses);

        /// <summary>
        /// shows more detailed information about a particular process
        /// </summary>
        /// <param name="idProceses">id of the process to be shown</param>
        /// <returns>proceses</returns>
        public ShortProcess Details(int idProceses);

        /// <summary>
        /// shows more detailed information about a particular process
        /// </summary>
        /// <param name="nameProceses">name of the process to be shown</param>
        /// <returns>proceses</returns>
        public ShortProcess Details(string nameProceses);

        /// <summary>
        /// displays a list of all processes
        /// </summary>
        /// <returns>returned list with proceses</returns>
        public List<ShortProcess> List();

        /// <summary>
        /// kills only one process, not a branch of processes
        /// </summary>
        /// <param name="nameProceses"></param>
        /// <returns>return true if proceses has benn killed</returns>
        public bool KillOneProcess(string nameProceses);
    }
}
