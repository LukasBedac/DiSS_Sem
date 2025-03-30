using Core.Exceptions;
using Core.SimulationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventSimulation
{
    public class EventSimulation : Simulation
    {
        public double SimulationTime { get; set; } = 0;
        private PriorityQueue<Event, double> EventCalendar = new PriorityQueue<Event, double>();
        public EventSimulation()
        {

        }

        public bool AddEvent(Event newEvent)
        {
            if (newEvent.Time < SimulationTime)
            {
                throw new CausalityException();
            } else
            {
                EventCalendar.Enqueue(newEvent, newEvent.Time);
                return true;
            }
        }

    }
}
