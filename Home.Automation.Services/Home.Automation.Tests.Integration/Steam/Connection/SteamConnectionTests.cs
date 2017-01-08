using System;
using Home.Automation.Steam.Service.Implementation;
using Home.Automation.Steam.Service.Interface;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.Steam.Connection
{
    public class SteamConnectionTests
    {
        [Test]
        public static void ShouldConnectToSteam()
        {
            ISteamService steamService = new SteamService();
            steamService.LogOn(Environment.GetEnvironmentVariable("STEAM_SERVICE_ACCOUNT_USERNAME"), Environment.GetEnvironmentVariable("STEAM_SERVICE_ACCOUNT_PASSWORD"));
            Assert.That(steamService.IsConnected, Is.EqualTo(true));
            Assert.That(steamService.IsLoggedOn, Is.EqualTo(true));
        }

        [Test]
        public static void ShouldDisconnectFromSteam()
        {
            ISteamService steamService = new SteamService();
            steamService.LogOn(Environment.GetEnvironmentVariable("STEAM_SERVICE_ACCOUNT_USERNAME"), Environment.GetEnvironmentVariable("STEAM_SERVICE_ACCOUNT_PASSWORD"));
            steamService.LogOff();
            Assert.That(steamService.IsConnected, Is.EqualTo(false));
            Assert.That(steamService.IsLoggedOn, Is.EqualTo(false));
        }
    }
}
