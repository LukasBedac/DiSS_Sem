using Core.Exceptions;
using Core.SimulationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventSimulation
{
    public class Event
    {
        public double Time { get; set; }
        public EventSimulation SimulationCore { get; set; }
        public Event(Simulation simulation, double time) 
        {
            SimulationCore = (EventSimulation)simulation;
            Time = time;
        }

        public virtual void Execute() 
        {

        }
    }
}
