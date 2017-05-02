using System;

namespace Everlasting.Core
{
    public class InvalidExecutablePathException : Exception
    {
        public InvalidExecutablePathException(string message)
            : base(message)
        {
        }
    }
}
