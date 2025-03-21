using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGen
{
    public static class SeedGenerator
    {
        private static Random SeedGen = new Random();
        public static List<int> GeneratedUsedSeeds = new List<int>();
        public static int GetSeed()
        {
            lock (SeedGen)
            {
                int seed = SeedGen.Next();
                GeneratedUsedSeeds.Add(seed);
                return seed;
            }
        }
    }
}
