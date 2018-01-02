using Blep.Contract.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blep.Contract
{
    public interface IDeviceDiscovery
    {
        IEnumerable<IDeviceInfo> EnumerateDevices(TimeSpan maxExecutionTime);
        Task<IEnumerable<IDeviceInfo>> EnumerateDevicesAsync(TimeSpan maxExecutionTime);

        ResourceEnumerationResult EnumerateResources(string deviceId);
        Task<ResourceEnumerationResult> EnumerateResourcesAsync(string deviceId);
    }
}
