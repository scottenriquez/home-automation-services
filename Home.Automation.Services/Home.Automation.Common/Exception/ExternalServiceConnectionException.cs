using System;

namespace Home.Automation.Common.Exception
{
    public class ExternalServiceConnectionException : System.Exception
    {
        public ExternalServiceConnectionException(string serviceName) : base("The server was unable to connect to " + serviceName)
        {
            
        }
    }
}
