using System;
using System.Collections.Generic;
using Home.Automation.Steam.Model;
using Home.Automation.Steam.Service.Implementation;
using NUnit.Framework;

namespace Home.Automation.Tests.Integration.Steam.User
{
    [TestFixture]
    public class FriendListTests
    {
        [Test]
        public static void ShouldGetFriendList()
        {
            SteamService steamService = new SteamService();
            IList<SteamFriend> friendList = steamService.GetFriendList(Environment.GetEnvironmentVariable("STEAM_WEB_API_KEY"), Environment.GetEnvironmentVariable("STEAM_PROFILE_ID"));
            Assert.That(friendList, !Is.Empty);
        }
    }
}
