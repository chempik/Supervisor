using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class DeletedObjectException : Exception
    {
        public DeletedObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
