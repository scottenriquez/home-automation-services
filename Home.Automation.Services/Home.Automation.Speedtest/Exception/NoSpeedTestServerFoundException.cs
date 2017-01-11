namespace Home.Automation.Speedtest.Exception
{
    public class NoSpeedTestServerFoundException : System.Exception
    {
        public NoSpeedTestServerFoundException() : base("No servers are available for testing speed and latency")
        {
            
        }
    }
}
