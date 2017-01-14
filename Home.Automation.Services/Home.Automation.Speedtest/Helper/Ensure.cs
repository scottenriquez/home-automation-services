using Home.Automation.Speedtest.Exception;
using MSpeedTest.Models;

namespace Home.Automation.Speedtest.Helper
{
    public static class Ensure
    {
        public static void ThatThereAreServersAvailable(Settings settings)
        {
            if (settings.Servers.Count == 0)
            {
                throw new NoSpeedTestServerFoundException();
            }
        }
    }
}
