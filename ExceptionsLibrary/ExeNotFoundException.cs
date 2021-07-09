using System;

namespace ExceptionsLibrary
{
    public class ExeNotFoundException : Exception
    {
        public ExeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
