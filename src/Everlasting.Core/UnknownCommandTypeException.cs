using System;

namespace Everlasting.Core
{
    public class UnknownCommandTypeException : Exception
    {
        public UnknownCommandTypeException(string message)
            : base(message)
        {
        }
    }
}
