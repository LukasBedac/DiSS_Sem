using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class CausalityException : Exception
    {
        public CausalityException() : base("Causality has been violated!") { }
    }
}
