using System.Collections.Generic;
using Home.Automation.Steam.Model;

namespace Home.Automation.Steam.Service.Interface
{
    public interface ISteamService
    {
        /// <summary>
        /// Whether or not the client is connected to Steam
        /// </summary>
        bool IsConnected { get; set; }
        /// <summary>
        /// Whether or not the client is authenticated by Steam
        /// </summary>
        bool IsLoggedOn { get; set; }

        /// <summary>
        /// Connect and log into the Steam network to perform restricted actions
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        void LogOn(string username, string password);

        /// <summary>
        /// Disconnect from the Steam network
        /// </summary>
        void LogOff();

        /// <summary>
        /// Get the friend list for a Steam user via the Steam Web API (logging on is not required)
        /// </summary>
        /// <param name="apiKey">API key for the Steam Web API</param>
        /// <param name="steamId">Target user's Steam ID</param>
        /// <returns>Friend list for the Steam user</returns>
        IList<SteamUser> GetFriendList(string apiKey, string steamId);

        /// <summary>
        /// Get status information for a given Steam user
        /// </summary>
        /// <param name="apiKey">API key for the Steam Web API</param>
        /// <param name="steamId">Target user's Steam ID</param>
        /// <returns>Status information for the Steam user</returns>
        SteamPlayerSummary GetPlayerSummary(string apiKey, string steamId);
    }
}
