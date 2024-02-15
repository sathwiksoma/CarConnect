using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() { }
        public DatabaseConnectionException(string message) : base(message) { }
        public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException) { }
    }
}
