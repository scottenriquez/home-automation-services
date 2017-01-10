using System;
using System.Threading.Tasks;
using Home.Automation.Philips.Hue.Service.Implementation;
using Home.Automation.Philips.Hue.Service.Interface;
using NUnit.Framework;

namespace Home.Automation.Tests.Local.Hue
{
    [TestFixture]
    public class HueLightTests
    {
        [Test]
        public static async Task ShouldSetGameDayLights()
        {
            IPhilipsHueService philipsHueService = new LocalPhilipsHueService(Environment.GetEnvironmentVariable("PHILIPS_BRIDGE_LOCAL_IP_ADDRESS"), Environment.GetEnvironmentVariable("PHILIPS_HUE_APPKEY"));
            await philipsHueService.TurnAllLightsOn();
        }
    }
}
