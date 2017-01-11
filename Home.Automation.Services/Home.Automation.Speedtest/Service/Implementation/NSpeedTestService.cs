using System.Linq;
using Home.Automation.Speedtest.Helper;
using Home.Automation.Speedtest.Service.Interface;
using NSpeedTest;
using NSpeedTest.Models;

namespace Home.Automation.Speedtest.Service.Implementation
{
    public class NSpeedTestService : ISpeedTestService
    {
        private readonly SpeedTestClient _speedTestClient;
        private readonly Settings _settings;
        private readonly Server _nearestServer;

        public NSpeedTestService()
        {
            _speedTestClient = new SpeedTestClient();
            _settings = _speedTestClient.GetSettings();
            Ensure.ThatThereAreServersAvailable(_settings);
            _nearestServer = _settings.Servers.First();
        }

        public double TestDownloadSpeed()
        {
            
            return _speedTestClient.TestDownloadSpeed(_nearestServer);
        }

        public double TestUploadSpeed()
        {
            return _speedTestClient.TestUploadSpeed(_nearestServer);
        }

        public int TestLatency()
        {
            return _speedTestClient.TestServerLatency(_nearestServer);
        }
    }
}
