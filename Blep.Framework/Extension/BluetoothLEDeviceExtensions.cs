using Blep.Contract.Model;
using System.Collections.Generic;
using Windows.Devices.Bluetooth;

namespace Blep.Framework.Extension
{
    public static class BluetoothLEDeviceExtensions
    {
        public static IDeviceInfo ToDeviceInfo(this BluetoothLEDevice device, IReadOnlyList<ICharacteristicInfo> characteristics)
        {
            return new ListResourcesDevice
            {
                Id = device.DeviceId,
                Name = device.Name,
                BluetoothAddress = device.BluetoothAddress,

                Characteristics = characteristics,
            };
        }
    }
}
