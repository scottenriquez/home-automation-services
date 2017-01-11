using Home.Automation.Speedtest.Service.Implementation;
using Home.Automation.Speedtest.Service.Interface;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.SpeedTest.Speed
{
    [TestFixture]
    public class TestDownloadTests
    {
        [Test]
        public static void ShouldTestDownloadSpeed()
        {
            ISpeedTestService speedTestService = new NSpeedTestService();
            double speedInKbps = speedTestService.TestDownloadSpeed();
            Assert.That(speedInKbps, Is.GreaterThan(0.0));
        }
    }
}
