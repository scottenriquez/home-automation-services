namespace Home.Automation.Steam.Model
{
    public class SteamFriendStatus
    {
        /// <summary>
        /// Friend's Steam metadata
        /// </summary>
        public SteamFriend SteamFriend { get; set; }
        /// <summary>
        /// Whether or not the friend is online
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
