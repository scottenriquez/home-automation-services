using System;

namespace Home.Automation.Common.Exceptions
{
    public class ExternalServiceLogOnException : Exception
    {
        public ExternalServiceLogOnException(string serviceName) : base("The server was unable to log on to " + serviceName)
        {
            
        }
    }
}
