using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0424_Assignment
{
    internal class EmergencyRoom<TElement, TPriority>
    {
        PriorityQueue<string, int> patients;
        public int Count { get { return patients.Count; } }
        public EmergencyRoom()
        {
            this.patients = new PriorityQueue<string, int>();
        }

        public struct Patient
        {
            public string name;
            public int goldenTime;

            public Patient(string name, int goldenTime) { this.name = name; this.goldenTime = goldenTime; }
        }

        public void EnqueuePatient(string name, int goldenTime) 
        {
            patients.Enqueue(name, goldenTime);
        }
        public string DequeuePatient()
        {
            return patients.Dequeue();
        }
    }
}
