using Blep.Contract.Model;
using Blep.Framework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Blep.Framework.Polling
{
    public abstract class DevicePollerBase
    {
        public Guid Service { get; }

        public Guid Characteristic { get; }

        public IPresentationFormat DefaultPresentationFormat { get; }

        protected DevicePollerBase(Guid service, Guid characteristic) : this (service, characteristic, null)
        {
        }

        protected DevicePollerBase(Guid service, Guid characteristic, IPresentationFormat defaultFormat)
        {
            Service = service;
            Characteristic = characteristic;
            DefaultPresentationFormat = defaultFormat;
        }

        protected async Task<object> PollNow(string deviceId, BluetoothCacheMode mode)
        {
            try
            {
                using (var device = await CreateDevice(deviceId))
                {
                    var characteristic = await CreateCharacteristic(device, mode);

                    GattReadResult result = await characteristic.ReadValueAsync(mode);
                    if (result.Status != GattCommunicationStatus.Success)
                    {
                        throw result.ToException();
                    }

                    // force descriptors
                    var r = await characteristic.GetDescriptorsAsync();

                    // presentation format - first if available
                    var format = characteristic.PresentationFormats.FirstOrDefault();

                    if (format == null)
                    {
                        return result.ToValue(DefaultPresentationFormat);
                    }

                    return result.ToValue(format);
                }
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        protected async Task<BluetoothLEDevice> CreateDevice(string deviceId)
        {
            return await BluetoothLEDevice.FromIdAsync(deviceId);
        }

        protected async Task<GattCharacteristic> CreateCharacteristic(BluetoothLEDevice deveice, BluetoothCacheMode mode)
        {
            var serviceResult = await deveice.GetGattServicesForUuidAsync(Service);

            if (serviceResult.Status != GattCommunicationStatus.Success)
            {
                //TODO process proper result message 
                return null;
            }

            foreach (var service in serviceResult.Services)
            {
                var result = await service.GetCharacteristicsForUuidAsync(Characteristic);

                if (result.Status == GattCommunicationStatus.Success)
                {
                    return result.Characteristics.FirstOrDefault();
                }
            }

            return null;
        }
    }
}
