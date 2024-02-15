using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException() { }
        public VehicleNotFoundException(string message) : base(message) { }
    }
}
