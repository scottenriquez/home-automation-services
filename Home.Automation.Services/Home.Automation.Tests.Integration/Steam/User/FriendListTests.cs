using System;
using System.Collections.Generic;
using Home.Automation.Steam.Model;
using Home.Automation.Steam.Service.Implementation;
using Home.Automation.Steam.Service.Interface;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.Steam.User
{
    [TestFixture]
    public class FriendListTests
    {
        [Test]
        public static void ShouldGetFriendList()
        {
            ISteamService steamService = new SteamService();
            IList<SteamUser> friendList = steamService.GetFriendList(Environment.GetEnvironmentVariable("STEAM_WEB_API_KEY"), Environment.GetEnvironmentVariable("STEAM_PROFILE_ID"));
            Assert.That(friendList, !Is.Empty);
        }
    }
}
