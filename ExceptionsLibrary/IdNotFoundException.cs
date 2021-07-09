using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class IdNotFoundException : Exception
    {
        public string Message { get; set; }
        public Exception Except {get; set;}
        public IdNotFoundException (string message, Exception except)
        {
            Message = message;
            Except = except;
        }
        public IdNotFoundException() { }
    }
}
