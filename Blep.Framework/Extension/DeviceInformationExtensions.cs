using Blep.Contract.Model;
using Blep.Framework.Extension;
using System.Collections.Generic;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace Blep.Framework
{
    internal static class DeviceInformationExtensions
    {
        internal static DiscoveredDevice ToDiscoveredDevice(this DeviceInformation device)
        {
            // convert DEVICE ID - NEED newer W10
            //var bluetoothDeviceId = BluetoothDeviceId.FromId(device.Id); 

            var deviceAddress = device.Properties.ToProperty<string>(WellKnownProperties.AepDeviceAddress);

            return new DiscoveredDevice
            {
                //Id = bluetoothDeviceId.Id,
                Id = device.Id,
                Name = device.Name,
                BluetoothAddress = deviceAddress.ToBluetoothAddress(),
            };
        }

        internal static T ToProperty<T>(this IReadOnlyDictionary<string, object> properties, string id)
        {
            if (properties.TryGetValue(id, out object value))
            {
                return (T)value;
            }
            return default(T);
        }
    }
}
