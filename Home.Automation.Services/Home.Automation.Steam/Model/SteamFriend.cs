﻿namespace Home.Automation.Steam.Model
{
    public class SteamFriend
    {
        /// <summary>
        /// Friend's Steam ID
        /// </summary>
        public string SteamId { get; set; }
        /// <summary>
        /// Friend since date
        /// </summary>
        public string FriendSince { get; set; }
        /// <summary>
        /// Whether or not the friend is online
        /// </summary>
        public bool IsOnline { get; set; }
    }
}