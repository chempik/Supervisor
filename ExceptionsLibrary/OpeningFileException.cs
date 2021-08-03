using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class OpeningFileException : Exception
    {
        public OpeningFileException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
