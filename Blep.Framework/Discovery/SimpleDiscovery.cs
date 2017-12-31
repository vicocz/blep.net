using Blep.Contract;
using Blep.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Blep.Framework.Discovery
{
    public partial class SimpleDiscovery : IDeviceDiscovery
    {
        public IEnumerable<DiscoveredDevice> EnumerateDevices(TimeSpan maxExecutionTime)
        {
            return EnumerateDevicesAsync(maxExecutionTime).Result;
        }

        public async Task<IEnumerable<DiscoveredDevice>> EnumerateDevicesAsync(TimeSpan maxExecutionTime)
        {
            using (var task = new DiscoveryTask())
            {
                return await task.Start(maxExecutionTime);
            }
        }

        public DiscoveredDevice EnumerateResources(string deviceId)
        {
            return EnumerateResourcesAsync(deviceId).Result;
        }

        public async Task<DiscoveredDevice> EnumerateResourcesAsync(string deviceId)
        {
            using (var bleDevice = await BluetoothLEDevice.FromIdAsync(deviceId))
            {
                if (bleDevice == null)
                {
                    return null;
                }

                var discoveredAttributes = new List<DeviceAttribute>();
                var device = new DiscoveredDevice
                {
                    Id = bleDevice.DeviceId,
                    Name = bleDevice.Name,
                    Address = bleDevice.BluetoothAddress.ToString(),

                    Attributes = discoveredAttributes,
                };

                // Note: BluetoothLEDevice.GattServices property will return an empty list for unpaired devices. 
                // For all uses we recommend using the GetGattServicesAsync method.
                // BT_Code: GetGattServicesAsync returns a list of all the supported services of 
                // the device (even if it's not paired to the system).
                // If the services supported by the device are expected to change during BT usage,
                // subscribe to the GattServicesChanged event.
                GattDeviceServicesResult result = await bleDevice.GetGattServicesAsync(BluetoothCacheMode.Uncached);

                if (result.Status == GattCommunicationStatus.Success)
                {
                    foreach (var service in result.Services)
                    {
                        var gattCharacteristicsResult = await service.GetCharacteristicsAsync();

                        foreach (var characteristic in gattCharacteristicsResult.Characteristics)
                        {
                            var attribute = characteristic.ToDeviceAttribute();
                            discoveredAttributes.Add(attribute);
                        }
                    }
                }
                else
                {
                    //rootPage.NotifyUser("Device unreachable", NotifyType.ErrorMessage);
                }

                return device;
            }
        }
    }
}
