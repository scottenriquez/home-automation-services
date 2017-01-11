using Home.Automation.Speedtest.Service.Implementation;
using Home.Automation.Speedtest.Service.Interface;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.SpeedTest.Latency
{
    [TestFixture]
    public class TestLatencyTests
    {
        [Test]
        public static void ShouldTestUploadSpeed()
        {
            ISpeedTestService speedTestService = new NSpeedTestService();
            int latency = speedTestService.TestLatency();
            Assert.That(latency, Is.GreaterThan(0.0));
        }
    }
}
