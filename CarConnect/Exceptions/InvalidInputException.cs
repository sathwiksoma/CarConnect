﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class InvalidInputException : Exception
    {
        public InvalidInputException() { }
        public InvalidInputException(string message) : base(message) { }
    }
}
