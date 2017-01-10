using System.Threading.Tasks;
using Home.Automation.Philips.Hue.Service.Interface;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Groups;

namespace Home.Automation.Philips.Hue.Service.Implementation
{
    public class LocalPhilipsHueService : IPhilipsHueService
    {
        private string _internalIpAddress;
        private string _internalAppKey;
        private readonly ILocalHueClient _localHueClient;

        public LocalPhilipsHueService(string internalIpAddress, string internalAppKey)
        {
            _internalIpAddress = internalIpAddress;
            _internalAppKey = internalAppKey;
            _localHueClient = new LocalHueClient(internalIpAddress);
            _localHueClient.Initialize(_internalAppKey);
        }

        public async Task<HueResults> TurnAllLightsOn()
        {
            LightCommand command = new LightCommand();
            command.TurnOn();
            return await _localHueClient.SendCommandAsync(command);
        }
    }
}
