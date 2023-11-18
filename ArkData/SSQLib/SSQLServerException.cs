using System;

namespace SSQLib
{
    internal class SSQLServerException : Exception
    {
        public SSQLServerException(string message)
          : base(message)
        {
        }
    }
}
