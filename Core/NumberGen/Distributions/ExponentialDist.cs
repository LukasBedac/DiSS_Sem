using Core.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGen.Distributions
{
    public class ExponentialDist
    {
        private double Lambda { get; set; }
        private Random UNumber { get; set; } = new Random(SeedGenerator.GetSeed());
        public ExponentialDist(double lambda) 
        {
            Lambda = 1/lambda;
        }

        public double NextDouble()
        {
            double u = UNumber.NextDouble();
            return -Math.Log(1 - u) / Lambda;
        }
    }
}
