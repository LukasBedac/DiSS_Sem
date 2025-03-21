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
        public bool Stopped { get; set; } = false;

        public virtual void BeforeSimulation() { }
        public virtual void AfterSimulation() { }
        public virtual void BeforeReplication() { }
        public virtual void AfterReplication() { }
        public virtual void OneReplication() { }
        public virtual void Simulate()
        {
            BeforeSimulation();
            for (int i = 0; i < Replications; i++)
            {
                if (Stopped)
                {
                    break; //Correct stop - no return, because afterSimulation won't be called
                }
                BeforeReplication();
                OneReplication();
                AfterReplication(); 
            }
            AfterSimulation();
        }
    }
}
