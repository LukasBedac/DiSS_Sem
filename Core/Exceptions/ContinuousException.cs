using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class ContinuousException : Exception
    {
        public ContinuousException() : base("This class is using Continuous distribution!")
        {

        }
    }
}
