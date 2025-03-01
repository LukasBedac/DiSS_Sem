using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NumberGenerator
{
    public static class SeedGenerator
    {
        private static Random SeedGen = new Random();
        
        public static int GetSeed()
        {
            lock (SeedGen)
            {
                return SeedGen.Next();
            }
        }
    }
}
