using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Supervisor
{
    public class ListProcesses : IEnumerable
    {
        private List<Processes> _processes = new List<Processes>();
        private Processes Indexer(int indexNumber)
        {
            if (indexNumber >= 0 && indexNumber < _processes.Count)
            {
                return _processes[indexNumber];
            }

            else throw new IndexOutOfRangeException();
        }
        public Processes this[int indexNumber]
        {
            get
            {
                return Indexer(indexNumber);
            }
        }
        public void Add(string name, int pid, float memory)
        {
            _processes.Add(new Processes(name, pid, memory));
        }

        public void Remove(string name)
        {
            _processes.Remove(_processes.First(x => x.Name == name));
        }

        public void Remove(int pid)
        {
            _processes.Remove(_processes.First(x => x.PID == pid));
        }

        public IEnumerator GetEnumerator()
        {
            return _processes.GetEnumerator();
        }
        public int Count
        {
            get
            {
                return _processes.Count;
            }
        }
    }
}
