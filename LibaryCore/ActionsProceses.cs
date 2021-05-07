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
        public event EventHandler<ProcessEventArgs> GetDetails;
        protected virtual void OnProcessEventArgs(ProcessEventArgs e, EventHandler<ProcessEventArgs> occasion)
        {
            EventHandler<ProcessEventArgs> raiseEvent = occasion;
            raiseEvent?.Invoke(this, e);
        }

        private ProcessEventArgs CreateEventArgs(Process proc)
        {
            ProcessEventArgs args = new ProcessEventArgs();
            args.proc = proc;
            return args;
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
            killed.Kill();
        }

        public void Details (int idProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process show = Proc.First(x => x.Id == idProceses);

            OnProcessEventArgs(CreateEventArgs(show), GetDetails);
        }

        public void Details(string nameProceses)
        {
            Process[] Proc = Process.GetProcesses();
            Process show = Proc.First(x => x.ProcessName == nameProceses);

            OnProcessEventArgs(CreateEventArgs(show), GetDetails);
        }
    }
}