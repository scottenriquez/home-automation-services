namespace Home.Automation.Steam.Service.Interface
{
    public interface ISteamService
    {
        /// <summary>
        /// Connect and log into Steam
        /// </summary>
        /// <param name="username">Service account username</param>
        /// <param name="password">Service account password</param>
        void Connect(string username, string password);
    }
}
