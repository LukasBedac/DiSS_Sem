using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class DiscreteException : Exception
    {
        public DiscreteException() : base("This class is using Discrete distribution!")
        {

        }
    }
}
