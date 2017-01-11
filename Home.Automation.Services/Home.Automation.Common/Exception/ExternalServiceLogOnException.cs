using System;

namespace Home.Automation.Common.Exception
{
    public class ExternalServiceLogOnException : System.Exception
    {
        public ExternalServiceLogOnException(string serviceName) : base("The server was unable to log on to " + serviceName)
        {
            
        }
    }
}
