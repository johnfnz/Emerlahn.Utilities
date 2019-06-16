using System;

namespace Emerlahn.Utilities.Testing
{
    public class AssertionException : Exception
    {
        public AssertionException(string message) : base(message)
        {
        }

        public AssertionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
