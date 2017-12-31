namespace Blep.Contract
{
    public interface IActiveDevicePoller
    {
        bool TryPollDevice(string deviceId, out object value);
    }
}
