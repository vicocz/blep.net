using Blep.Contract;
using Blep.Contract.Model;
using Blep.Framework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Blep.Framework.Discovery
{
    public partial class SimpleDiscovery : IDeviceDiscovery
    {
        public IEnumerable<IDeviceInfo> EnumerateDevices(TimeSpan maxExecutionTime)
        {
            return EnumerateDevicesAsync(maxExecutionTime).Result;
        }

        public async Task<IEnumerable<IDeviceInfo>> EnumerateDevicesAsync(TimeSpan maxExecutionTime)
        {
            using (var task = new DiscoveryTask())
            {
                return await task.Start(maxExecutionTime);
            }
        }

        public ResourceEnumerationResult EnumerateResources(string deviceId)
        {
            return EnumerateResourcesAsync(deviceId).Result;
        }

        public async Task<ResourceEnumerationResult> EnumerateResourcesAsync(string deviceId)
        {
            using (var bleDevice = await BluetoothLEDevice.FromIdAsync(deviceId))
            {
                if (bleDevice == null)
                {
                    return new ResourceEnumerationResult(ResourceEnumerationResult.EnumerationStatus.ProtocolError);
                }

                var discoveredCharacteristics = new List<ICharacteristicInfo>();
                var device = bleDevice.ToDeviceInfo(discoveredCharacteristics);

                // Note: BluetoothLEDevice.GattServices property will return an empty list for unpaired devices. 
                // For all uses we recommend using the GetGattServicesAsync method.
                // BT_Code: GetGattServicesAsync returns a list of all the supported services of 
                // the device (even if it's not paired to the system).
                // If the services supported by the device are expected to change during BT usage,
                // subscribe to the GattServicesChanged event.
                GattDeviceServicesResult result = await bleDevice.GetGattServicesAsync(BluetoothCacheMode.Uncached);

                if (result.Status != GattCommunicationStatus.Success)
                {
                    var status = result.Status.ToEnumerationStatus();
                    return new ResourceEnumerationResult(device, status);
                }

                foreach (var service in result.Services)
                {
                    var gattCharacteristicsResult = await service.GetCharacteristicsAsync();

                    if (result.Status != GattCommunicationStatus.Success)
                    {
                        var status = gattCharacteristicsResult.Status.ToEnumerationStatus();
                        return new ResourceEnumerationResult(device, status);
                    }

                    var servicChars = gattCharacteristicsResult.Characteristics.Select(ch => ch.ToCharacteristicInfo());
                    discoveredCharacteristics.AddRange(servicChars);
                }

                return new ResourceEnumerationResult(device, ResourceEnumerationResult.EnumerationStatus.Success);
            }
        }
    }
}
