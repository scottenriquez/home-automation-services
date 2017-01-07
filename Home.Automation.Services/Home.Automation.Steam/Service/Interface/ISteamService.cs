namespace Home.Automation.Steam.Service.Interface
{
    public interface ISteamService
    {
        /// <summary>
        /// Connect and log into the Steam network to perform restricted actions
        /// </summary>
        /// <param name="username">Service account username</param>
        /// <param name="password">Service account password</param>
        void LogOn(string username, string password);

        /// <summary>
        /// Disconnect from the Steam network to perform restricted actions
        /// </summary>
        void LogOff();

        /// <summary>
        /// Get the friend list for a Steam user via the Steam Web API (logging on is not required)
        /// </summary>
        void GetFriendList(string apiKey, string steamId);
    }
}
