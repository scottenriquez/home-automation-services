using System;
using System.Collections.Generic;
using Home.Automation.Common.Exceptions;
using Home.Automation.Steam.Model;
using Home.Automation.Steam.Service.Interface;
using SteamKit2;
using SteamKit2.Unified.Internal;

namespace Home.Automation.Steam.Service.Implementation
{
    public class SteamService : ISteamService
    {
        /// <summary>
        /// Whether or not the client is connected to Steam
        /// </summary>
        public bool IsConnected { get; set; }
        /// <summary>
        /// Whether or not the client is authenticated by Steam
        /// </summary>
        public bool IsLoggedOn { get; set; }

        /// <summary>
        /// The Steam client
        /// </summary>
        private SteamClient _steamClient;
        /// <summary>
        /// The callback manager for handling events
        /// </summary>
        private CallbackManager _callbackManager;
        /// <summary>
        /// The authenticated Steam user
        /// </summary>
        private SteamUser _steamUser;
        /// <summary>
        /// The account's username
        /// </summary>
        private string _username;
        /// <summary>
        /// The account's password
        /// </summary>
        private string _password;
        /// <summary>
        /// Whether or not the client is awaiting a callback
        /// </summary>
        private bool _isExecutingRequest;

        /// <summary>
        /// The name of the service to pass to exception handlers
        /// </summary>
        private const string SERVICE_NAME = "Steam";
        /// <summary>
        /// Web API interface name for Steam user endpoints
        /// </summary>
        private const string STEAM_USER_WEB_API_INTERFACE = "ISteamUser";
        /// <summary>
        /// The wait interval for Steam callbacks
        /// </summary>
        private const int CALLBACK_WAIT_INTERVAL_IN_SECONDS = 1;
        /// <summary>
        /// The initial index in the Web API response key value pari
        /// </summary>
        private const int CHILDREN_WEB_API_INDEX = 0;
        /// <summary>
        /// 
        /// </summary>
        private const int STEAM_ID_API_RESULT_INDEX = 0;
        /// <summary>
        /// 
        /// </summary>
        private const int FRIEND_SINCE_API_RESULT_INDEX = 2;

        /// <summary>
        /// Steam service constructor method
        /// </summary>
        public SteamService()
        {
            _isExecutingRequest = false;
        }

        /// <summary>
        /// Connect and log into the Steam network to perform restricted actions
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        public void LogOn(string username, string password)
        {
            //prevents multiple connection attempts
            if (!IsConnected)
            {
                _username = username;
                _password = password;
                _steamClient = new SteamClient();
                _callbackManager = new CallbackManager(_steamClient);
                _steamUser = _steamClient.GetHandler<SteamUser>();
                _callbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
                _callbackManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);
                _callbackManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
                _callbackManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
                _isExecutingRequest = true;
                _steamClient.Connect();
                while (_isExecutingRequest)
                {
                    _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(CALLBACK_WAIT_INTERVAL_IN_SECONDS));
                }
            }
        }

        /// <summary>
        /// Disconnect from the Steam network
        /// </summary>
        public void LogOff()
        {
            //prevents multiple disconnection attempts
            if (IsConnected)
            {
                _isExecutingRequest = true;
                _steamClient.Disconnect();
                while (_isExecutingRequest)
                {
                    _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(CALLBACK_WAIT_INTERVAL_IN_SECONDS));
                }
            }
        }

        /// <summary>
        /// Get the friend list for a Steam user via the Steam Web API (logging on is not required)
        /// </summary>
        /// <param name="apiKey">API key for the Steam Web API</param>
        /// <param name="steamId">Target user's Steam ID</param>
        /// <returns>Friend list for the given Steam ID</returns>
        public IList<SteamFriend> GetFriendList(string apiKey, string steamId)
        {
            IList<SteamFriend> friendList = new List<SteamFriend>();
            using (dynamic steamUserApiInterface = WebAPI.GetInterface(STEAM_USER_WEB_API_INTERFACE, apiKey))
            {
                KeyValue apiResponse = steamUserApiInterface.GetFriendList(steamid: steamId);
                foreach (KeyValue friend in apiResponse.Children[CHILDREN_WEB_API_INDEX].Children)
                {
                    friendList.Add(new SteamFriend()
                    {
                        SteamId = friend.Children[STEAM_ID_API_RESULT_INDEX].Value,
                        FriendSince = friend.Children[FRIEND_SINCE_API_RESULT_INDEX].Value
                    });
                }
                return friendList;
            }
        }

        /// <summary>
        /// Get status information for a given Steam user
        /// </summary>
        /// <param name="apiKey">API key for the Steam Web API</param>
        /// <param name="steamId">Target user's Steam ID</param>
        /// <returns>Status information for the Steam user</returns>
        public SteamPlayerSummary GetPlayerSummary(string apiKey, string steamId)
        {
            using (dynamic steamUserApiInterface = WebAPI.GetInterface(STEAM_USER_WEB_API_INTERFACE, apiKey))
            {
                KeyValue apiResponse = steamUserApiInterface.GetPlayerSummaries(steamids: steamId);
                Console.WriteLine(apiResponse);
            }
            return null;
        }

        /// <summary>
        /// Handler function to be executed after attempting to connect to Steam
        /// </summary>
        /// <param name="connectedCallback">The callback result recieved from the attempt to connect to the Steam network</param>
        private void OnConnected(SteamClient.ConnectedCallback connectedCallback)
        {
            if (connectedCallback.Result != EResult.OK)
            {
                throw new ExternalServiceConnectionException(SERVICE_NAME);
            }
            IsConnected = true;
            _steamUser.LogOn(new SteamUser.LogOnDetails
            {
                Username = _username,
                Password = _password
            });
        }

        /// <summary>
        /// Handler function to be executed when the client is disconnected from the Steam network
        /// </summary>
        /// <param name="disconnectedCallback">The callback result recieved from disconnecting from the Steam network</param>
        private void OnDisconnected(SteamClient.DisconnectedCallback disconnectedCallback)
        {
            IsConnected = false;
            IsLoggedOn = false;
            _isExecutingRequest = false;
        }

        /// <summary>
        /// Handler function to be executed when the user logs into the Steam network
        /// </summary>
        /// <param name="loggedOnCallback">The callback result recieved from attempting to log on user to the Steam network</param>
        private void OnLoggedOn(SteamUser.LoggedOnCallback loggedOnCallback)
        {
            if (loggedOnCallback.Result != EResult.OK)
            {
                throw new ExternalServiceLogOnException(SERVICE_NAME);
            }
            IsLoggedOn = true;
            _isExecutingRequest = false;
        }

        /// <summary>
        /// Handler function to be executed when the user logs out of the Steam network
        /// </summary>
        /// <param name="loggedOffCallback">The callback result recieved from attempting to log out user from the Steam network</param>
        private void OnLoggedOff(SteamUser.LoggedOffCallback loggedOffCallback)
        {
            IsLoggedOn = false;
            _isExecutingRequest = false;
        }
    }
}
