using Core.NumberGen;
using Core.NumberGen.Distributions;
using DiscordRPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Test
{
    public class TestGen
    {
        private const int NUMBERS = 50000;
        public TestGen()
        {
            EvenDistTest();
            TriangularDistTest();
            ExponentialDistTest();
        }

        private void TriangularDistTest()
        {
            string path = "TriangularDist.txt";
            TriangularDist triangularDist = new TriangularDist(false, new Scope(60, 480, 120));
            using (StreamWriter writer = new StreamWriter(path, append: false))
            {
                for (int i = 0; i < NUMBERS; i++)
                {
                    writer.WriteLine(triangularDist.NextDouble());
                }
                writer.Close();
            }
        }
        private void ExponentialDistTest()
        {
            string path = "ExponentialDist.txt";
            ExponentialDist exponentialDist = new ExponentialDist(30);
            using (StreamWriter writer = new StreamWriter(path, append: false))
            {
                for (int i = 0; i < NUMBERS; i++)
                {
                    writer.WriteLine(exponentialDist.NextDouble());
                }
                writer.Close();
            }
        }

        private void EvenDistTest()
        {
            string path = "EvenlDist.txt";
            EvenDist evenDist = new EvenDist(true,
                new List<Scope>
                {
                    new Scope(1, 2, 0.2),
                    new Scope(2, 3, 0.2),
                    new Scope(3, 4, 0.2),
                    new Scope(4, 5, 0.2),
                    new Scope(5, 6, 0.2),
                });
            using (StreamWriter writer = new StreamWriter(path, append: false))
            {
                for (int i = 0; i < NUMBERS; i++)
                {
                    writer.WriteLine(evenDist.NextInt());
                }
                writer.Close();
            }
        }

        }
}
