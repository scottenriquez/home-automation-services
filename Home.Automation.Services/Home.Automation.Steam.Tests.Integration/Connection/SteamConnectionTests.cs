using System;
using Home.Automation.Steam.Service.Implementation;
using NUnit.Framework;

namespace Home.Automation.Steam.Tests.Integration.Connection
{
    [TestFixture]
    public class SteamConnectionTests
    {
        [Test]
        public static void ShouldConnectToSteam()
        {
            SteamService steamService = new SteamService();
            steamService.Connect(Environment.GetEnvironmentVariable("STEAM_SERVICE_ACCOUNT_USERNAME"), Environment.GetEnvironmentVariable("STEAM_SERVICE_ACCOUNT_PASSWORD"));
            Assert.That(steamService.IsConnected, Is.EqualTo(true));
            Assert.That(steamService.IsLoggedOn, Is.EqualTo(true));
            steamService.Disconnect();
        }
    }
}
