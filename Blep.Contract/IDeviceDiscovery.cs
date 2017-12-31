using Blep.Contract.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blep.Contract
{
    public interface IDeviceDiscovery
    {
        IEnumerable<DiscoveredDevice> EnumerateDevices(TimeSpan maxExecutionTime);
        Task<IEnumerable<DiscoveredDevice>> EnumerateDevicesAsync(TimeSpan maxExecutionTime);

        DiscoveredDevice EnumerateResources(string deviceId);
        Task<DiscoveredDevice> EnumerateResourcesAsync(string deviceId);
    }
}
