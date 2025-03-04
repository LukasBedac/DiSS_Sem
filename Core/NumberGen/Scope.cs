using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGen
{
    public class Scope
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public double Probability { get; set; }
        public Scope(int min, int max, double probability)
        {
            Min = min; 
            Max = max; 
            Probability = probability;
        }
    }
}
