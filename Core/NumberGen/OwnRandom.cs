using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGen
{
    public class OwnRandom
    {
        public Random? Random { get; set; } = null;
        public List<Random>? Randoms { get; set; } = null;
        public List<Scope>? Scopes { get; set; } = null;
        public bool IsDiscrete { get; set; }
        public Random? ProbabilityGen { get; set; } = null;
        public OwnRandom(bool isDiscrete, List<Scope> scopes) 
        {
            IsDiscrete = isDiscrete;
            Scopes = scopes;
            CreateRandom(scopes);
        }
        private void CreateRandom(List<Scope> scopes) 
        {
            if (scopes.Count == 1)
            {
                Random = new Random(SeedGenerator.GetSeed());
            } else
            {
                Randoms = new List<Random>();
                for (int i = 0; i < scopes.Count;i++)
                {
                    Randoms.Add(new Random(SeedGenerator.GetSeed()));
                }
            }
        }
        public int NextInt()
        {
            if (Random != null)
            {
                return Random.Next();
            }
            else
            {
                throw new Exception("Random is null");
            }
        }
        public int NextInt(double probability)
        {
            int value = 0;
            if (Randoms?.Count > 0)
            {
                for (int i = 0; i < Randoms.Count;i++)
                {
                    if (probability < Scopes?[0].Probability)
                    {
                        value = Randoms[i].Next(Scopes[i].Min, Scopes[i].Max + 1);
                    }
                }
            }
            else
            {
                throw new Exception("Random is null");
            }
            return value;
        }

        public double NextDouble()
        {
            if (Random != null)
            {
                return Random.NextDouble();
            } else
            {
                throw new Exception("Random is null");
            }
        }
        public double NextDouble(double probability)
        {
            double value = 0;
            if (Randoms?.Count > 0)
            {
                for (int i = 0; i < Randoms.Count; i++)
                {
                    if (probability < Scopes?[0].Probability)
                    {
                        value = Scopes[i].Min + Randoms[i].NextDouble() 
                            * (Scopes[i].Max - Scopes[i].Min);
                    }
                }
            }
            else
            {
                throw new Exception("Random is null");
            }
            return value;
        }
    }
}
