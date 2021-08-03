using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class ProcessCannotBeCompletedException : Exception
    {
        public ProcessCannotBeCompletedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
