using System;
using Home.Automation.Common.Exceptions;
using Home.Automation.Steam.Service.Interface;
using SteamKit2;

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
        /// The name of the service to pass to exception handlers
        /// </summary>
        private const string SERVICE_NAME = "Steam";
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
        /// The service account's username
        /// </summary>
        private string _username;
        /// <summary>
        /// The service account's password
        /// </summary>
        private string _password;
        /// <summary>
        /// Whether or not the client is awaiting a callback
        /// </summary>
        private bool _isExecutingRequest;

        /// <summary>
        /// Steam service constructor method
        /// </summary>
        public SteamService()
        {
            IsConnected = false;
            IsLoggedOn = false;
            _steamClient = new SteamClient();
            _callbackManager = new CallbackManager(_steamClient);
            _steamUser = _steamClient.GetHandler<SteamUser>();
            _callbackManager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            _callbackManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);
            _callbackManager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            _callbackManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            _isExecutingRequest = false;
        }

        /// <summary>
        /// Connect and log into the Steam network
        /// </summary>
        /// <param name="username">Service account username</param>
        /// <param name="password">Service account password</param>
        public void Connect(string username, string password)
        {
            _username = username;
            _password = password;
            _isExecutingRequest = true;
            _steamClient.Connect();
            while (_isExecutingRequest)
            {
                _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
            }
        }

        /// <summary>
        /// Disconnect from the Steam network
        /// </summary>
        public void Disconnect()
        {
            _isExecutingRequest = true;
            _steamClient.Disconnect();
            while (_isExecutingRequest)
            {
                _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
            }
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
