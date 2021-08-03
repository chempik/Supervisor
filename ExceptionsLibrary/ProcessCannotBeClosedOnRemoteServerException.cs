using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class ProcessCannotBeClosedOnRemoteServerException : Exception
    {
        public ProcessCannotBeClosedOnRemoteServerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
