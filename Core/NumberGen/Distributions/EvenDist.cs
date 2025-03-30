using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGen.Distributions
{
    public class EvenDist
    {
        private Random? Random { get; set; } = null;
        private List<Random>? Randoms { get; set; } = null;
        private List<Scope> Scopes { get; set; } = new List<Scope>();
        private bool IsDiscrete { get; set; }
        private Random ProbabilityGen { get; set; } = new Random(SeedGenerator.GetSeed());
        private const double CHECK = 0.000000000001;
        public EvenDist(bool isDiscrete, List<Scope> scopes)
        {
            IsDiscrete = isDiscrete;
            Scopes = scopes;
            CreateRandom();
        }
        private void CreateRandom()
        {
            if (Scopes.Count == 1)
            {
                Random = new Random(SeedGenerator.GetSeed());
            }
            else
            {
                Randoms = new List<Random>();
                for (int i = 0; i < Scopes.Count; i++)
                {
                    Randoms.Add(new Random(SeedGenerator.GetSeed()));
                }
            }
        }
        public double NextDouble()
        {
            if (IsDiscrete)
            {
                throw new DiscreteException();
            }
            double value = 0;
            if (Scopes.Count < 2 && Random != null)
            {
                return Scopes[0].Min + (Random.NextDouble()
                           * (Scopes[0].Max - Scopes[0].Min));
            }
            else
            {
                if (Randoms?.Count > 0)
                {
                    double scopeProbability = Scopes[0].Probability;
                    double probability = ProbabilityGen.NextDouble();
                    for (int i = 0; i < Randoms.Count; i++)
                    {
                        if (probability <= scopeProbability + CHECK)
                        {
                            return value = Scopes[i].Min + (Randoms[i].NextDouble()
                                * (Scopes[i].Max - Scopes[i].Min));
                        }
                        else
                        {
                            scopeProbability += Scopes[i + 1].Probability;
                        }
                    }
                }
            }
            throw new Exception("Random is null");
        }

        public int NextInt()
        {
            if (!IsDiscrete)
            {
                throw new ContinuousException();
            }
            int value = 0;
            if (Scopes.Count < 2 && Random != null)
            {
                return Random.Next(Scopes[0].Max - Scopes[0].Min + 1) + Scopes[0].Min;
            } 
            else
            {
                if (Randoms?.Count > 0)
                {
                    double scopeProbability = Scopes[0].Probability;
                    double probability = ProbabilityGen.NextDouble();
                    for (int i = 0; i < Randoms.Count; i++)
                    {
                        if (probability <= scopeProbability + CHECK)
                        {
                            return value = Randoms[i].Next(Scopes[i].Max - Scopes[i].Min) + Scopes[i].Min;
                        }
                        else
                        {
                            scopeProbability += Scopes[i + 1].Probability;
                        }
                    }
                } 
            }
            throw new Exception("Random is null");
        }
    }
}
