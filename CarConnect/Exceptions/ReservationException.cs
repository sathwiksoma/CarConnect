using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class ReservationException : Exception
    {
        public ReservationException() { }
        public ReservationException(string message) :base(message) { }
    }
}
