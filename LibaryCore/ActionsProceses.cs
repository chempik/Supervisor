using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using LibaryCore;

namespace SupervisorConsole
{
    public class ActionsProceses
    {
        private ShortProcess CreateShortProcess (Process process)
        {
            ShortProcess sProc = new ShortProcess();
            sProc.Name = process.ProcessName;
            sProc.Id = process.Id;
            sProc.Memory = process.PeakWorkingSet64;
            try
            {
                sProc.Location = process.MainModule.FileName;
            }
            catch (Exception)
            {
                sProc.Location = "n/a";
            }
            
            return sProc;
        }

        public void Start(string link)
        {
            Process.Start(link);
        }

        public void Kill(string nameProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process killed = Proc.First(x => x.ProcessName == nameProceses);
            killed.Kill();
        }

        public void Kill(int idProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process killed = Proc.First(x => x.Id==idProceses);
            killed.Kill(true);
        }

        public ShortProcess Details (int idProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process show = Proc.First(x => x.Id == idProceses);

            return CreateShortProcess(show);
        }

        public ShortProcess Details(string nameProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process show = Proc.First(x => x.ProcessName == nameProceses);

            return CreateShortProcess(show);
        }

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