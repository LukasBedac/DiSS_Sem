using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGenerator
{
    public class OwnRandom : Random
    {
        public OwnRandom(bool isDiscrete, List<List<int>> scope) 
        {
            Random scopeSelector = new Random(SeedGenerator.GetSeed());
            double value = scopeSelector.NextDouble();
            if (isDiscrete)
            {
                
            } else
            {

            }
            //TODO 1.0 - 4 Different generators for sem1
        }
    }
}
