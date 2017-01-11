using Home.Automation.Speedtest.Service.Implementation;
using Home.Automation.Speedtest.Service.Interface;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.SpeedTest.Speed
{
    [TestFixture]
    public class TestUploadTests
    {
        [Test]
        public static void ShouldTestUploadSpeed()
        {
            ISpeedTestService speedTestService = new NSpeedTestService();
            double speedInKbps = speedTestService.TestUploadSpeed();
            Assert.That(speedInKbps, Is.GreaterThan(0.0));
        }
    }
}
