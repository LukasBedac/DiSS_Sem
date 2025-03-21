using Core.NumberGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test
{
    public class TestGen
    {
        public TestGen()
        {
            List<Scope> scopes = new List<Scope>();
            scopes.Add(new Scope(0, 1, 0.2));
            scopes.Add(new Scope(1, 2, 0.2));
            scopes.Add(new Scope(2, 3, 0.2));
            scopes.Add(new Scope(3, 4, 0.2));
            scopes.Add(new Scope(4, 5, 0.2));
            OwnRandom rand = new OwnRandom(true, scopes);

            List<double> number1 = new List<double>();
            List<double> number2 = new List<double>();
            List<double> number3 = new List<double>();
            List<double> number4 = new List<double>();
            List<double> number5 = new List<double>();
            for (int i = 0; i < 10000; i++)
            {
                double value = rand.NextDouble();
                if (value < 1)
                {
                    number1.Add(value);
                } else if (value < 2)
                {
                    number2.Add(value);
                }
                else if (value < 3)
                {
                    number3.Add(value);
                }
                else if (value < 4)
                {
                    number4.Add(value);
                } else
                {
                    number5.Add(value);
                }
            }

            System.Diagnostics.Debug.WriteLine(number1.Count);
            System.Diagnostics.Debug.WriteLine(number2.Count);
            System.Diagnostics.Debug.WriteLine(number3.Count);
            System.Diagnostics.Debug.WriteLine(number4.Count);
            System.Diagnostics.Debug.WriteLine(number5.Count);
        }
        
        
    }
}
