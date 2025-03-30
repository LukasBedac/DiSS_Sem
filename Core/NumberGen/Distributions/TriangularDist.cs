using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGen.Distributions
{
    public class TriangularDist
    {
        private Random? Random { get; set; } = null;
        private Scope Scope { get; set; }
        private bool IsDiscrete { get; set; }
        private Random ProbabilityGen { get; set; } = new Random(SeedGenerator.GetSeed());
        public TriangularDist(bool isDiscrete, Scope scope)
        {
            IsDiscrete = isDiscrete;
            Scope = scope;
            Random = new Random(SeedGenerator.GetSeed());
        }
        public int NextInt()
        {
            double u = Random != null ? Random.NextDouble() : throw new Exception("Random was null");
            double c = (Scope.Modus - Scope.Min) / (Scope.Max - Scope.Min);
            if (u <= c)
            {
                return (int)Math.Round(
                    Scope.Min + Math.Sqrt(u * (Scope.Max - Scope.Min)
                    * (Scope.Modus - Scope.Min)));
            }
            else
            {
                return (int)Math.Round(
                    Scope.Max - Math.Sqrt((1 - u) * (Scope.Max - Scope.Min)
                    * (Scope.Max - Scope.Modus)));
            }
        }

        public double NextDouble()
        {
            double u = Random != null ? Random.NextDouble() : throw new Exception("Random was null");
            double c = (double)(Scope.Modus - Scope.Min) / (Scope.Max - Scope.Min);
            if (u < c)
            {
                return Scope.Min + Math.Sqrt(u * (Scope.Max - Scope.Min)
                    * (Scope.Modus - Scope.Min));
            } else
            {
                return Scope.Max - Math.Sqrt((1 - u) * (Scope.Max - Scope.Min)
                    * (Scope.Max - Scope.Modus));
            }
        }
    }
}
