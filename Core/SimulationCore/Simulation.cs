using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimulationCore
{
    public class Simulation
    {
        public int Replications { get; set; }
        public virtual void BeforeSimulation() { }
        public virtual void AfterSimulation() { }
        public virtual void BeforeReplication() { }
        public virtual void AfterReplication() { }
        public virtual void OneReplication() { }
        public virtual void Simulate(int replications)
        {
            //for (int i = 0; i < replications; i++)
            //{
            //    OneReplication();
            //}
        }
    }
}
