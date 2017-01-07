using System;
using Home.Automation.Steam.Service.Implementation;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.Steam.User
{
    [TestFixture]
    public class FriendListTests
    {
        [Test]
        public static void ShouldGetFriends()
        {
            SteamService steamService = new SteamService();
            steamService.GetFriendList(Environment.GetEnvironmentVariable("STEAM_WEB_API_KEY"), Environment.GetEnvironmentVariable("STEAM_PROFILE_ID"));
        }
    }
}
