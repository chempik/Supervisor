using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
