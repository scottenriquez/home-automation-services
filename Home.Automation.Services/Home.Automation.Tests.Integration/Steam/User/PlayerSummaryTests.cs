using System;
using Home.Automation.Steam.Model;
using Home.Automation.Steam.Service.Implementation;
using Home.Automation.Steam.Service.Interface;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.Steam.User
{
    [TestFixture]
    public class PlayerSummaryTests
    {
        [Test]
        public static void ShouldGetPlayerSummary()
        {
            ISteamService steamService = new SteamService();
            SteamPlayerSummary steamPlayerSummary = steamService.GetPlayerSummary(Environment.GetEnvironmentVariable("STEAM_WEB_API_KEY"), Environment.GetEnvironmentVariable("STEAM_PROFILE_ID"));
            Assert.That(steamPlayerSummary.PersonaName, Is.EqualTo("exoentropy"));
        }
    }
}
