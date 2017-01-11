namespace Home.Automation.Speedtest.Service.Interface
{
    public interface ISpeedTestService
    {
        double TestDownloadSpeed();
        double TestUploadSpeed();
        int TestLatency();
    }
}
