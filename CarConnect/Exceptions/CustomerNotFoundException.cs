using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() { }
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
