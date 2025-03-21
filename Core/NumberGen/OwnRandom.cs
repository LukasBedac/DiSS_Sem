using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGen
{
    public class OwnRandom
    {
        private Random? Random { get; set; } = null;
        private List<Random>? Randoms { get; set; } = null;
        private List<Scope> Scopes { get; set; } = new List<Scope>();
        private bool IsDiscrete { get; set; }
        private Random ProbabilityGen { get; set; } = new Random(SeedGenerator.GetSeed());
        private const double CHECK = 0.000000000001;
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
                return Random.Next(Scopes[0].Max - Scopes[0].Min + 1) + Scopes[0].Min;
            }
            else
            {
                return NextInt(ProbabilityGen.NextDouble());
            }
        }
        private int NextInt(double probability)
        {
            int value = 0;
            if (Randoms?.Count > 0)
            {
                double scopeProbability = Scopes[0].Probability;
                for (int i = 0; i < Randoms.Count;i++)
                {
                    if (probability <= scopeProbability + CHECK)
                    {
                        return value = Randoms[i].Next(Scopes[i].Max - Scopes[i].Min) + Scopes[i].Min;
                    } else
                    {
                        scopeProbability += Scopes[i + 1].Probability;
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
                return Scopes[0].Min + ((1.0 - Double.Epsilon) * Random.NextDouble()
                            * (Scopes[0].Max - Scopes[0].Min));
            } else
            {
                return NextDouble(ProbabilityGen.NextDouble());
            }
        }
        private double NextDouble(double probability)
        {
            double value = 0;
            if (Randoms?.Count > 0)
            {
                double scopeProbability = Scopes[0].Probability;
                for (int i = 0; i < Randoms.Count; i++)
                {
                    if (probability <= scopeProbability + CHECK)
                    {
                        return value = Scopes[i].Min + ((1.0 - Double.Epsilon) * Randoms[i].NextDouble()
                            * (Scopes[i].Max - Scopes[i].Min));
                    } else
                    {
                        scopeProbability += Scopes[i + 1].Probability;
                    }
                }
            }
            throw new Exception("Random is null");
        }
    }
}
