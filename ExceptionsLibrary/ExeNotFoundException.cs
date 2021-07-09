using System;

namespace ExceptionsLibrary
{
    public class ExeNotFoundException : Exception
    {
        public string Message { get; set; }
        public Exception Except { get; set; }
        public ExeNotFoundException(string message, Exception except)
        {
            Message = message;
            Except = except;
        }
        public ExeNotFoundException() { }

    }
}
