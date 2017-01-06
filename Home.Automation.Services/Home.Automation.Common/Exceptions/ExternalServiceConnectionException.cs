using System;

namespace Home.Automation.Common.Exceptions
{
    public class ExternalServiceConnectionException : Exception
    {
        public ExternalServiceConnectionException(string serviceName) : base("The server was unable to connect to " + serviceName)
        {
            
        }
    }
}
