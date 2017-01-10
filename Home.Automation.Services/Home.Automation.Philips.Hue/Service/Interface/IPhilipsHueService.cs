using System.Threading.Tasks;
using Q42.HueApi.Models.Groups;

namespace Home.Automation.Philips.Hue.Service.Interface
{
    public interface IPhilipsHueService
    {
        Task<HueResults> TurnAllLightsOn();
    }
}
