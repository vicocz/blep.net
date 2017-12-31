using Blep.Contract;
using Blep.Contract.Model;
using System;

namespace Blep.Framework.Polling
{
    public class SimpleDevicePoller : DevicePollerBase, IActiveDevicePoller
    {
        public SimpleDevicePoller(Guid service, Guid characteristic) 
            : base (service, characteristic)
        {
        }

        public SimpleDevicePoller(Guid service, Guid characteristic, IPresentationFormat presentationFormat)
            : base(service, characteristic, presentationFormat)
        {
        }

        public bool TryPollDevice(string deviceId, out object value)
        {
            try
            {
                var pollResult = PollNow(deviceId, Windows.Devices.Bluetooth.BluetoothCacheMode.Uncached);

                value = pollResult.Result;
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
    }
}
