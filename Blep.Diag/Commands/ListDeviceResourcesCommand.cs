using Blep.Contract.Model;
using Blep.Framework.Discovery;
using Blep.Framework.Extension;
using System;

namespace Blep.Diag.Commands
{
    public class ListDeviceResourcesCommand
    {
        private readonly string _deviceId;

        public ListDeviceResourcesCommand(string deviceId)
        {
            _deviceId = deviceId;
        }

        public void Execute()
        {
            Console.Write($"Listing resources of device Id: {_deviceId} ...");

            var discovery = new DiscoveryFactory().Create();
            var deviceResult = discovery.EnumerateResources(_deviceId);

            Console.WriteLine();

            if (deviceResult.Status != ResourceEnumerationResult.EnumerationStatus.Success)
            {
                Console.WriteLine($"Operation has failed due to status: {deviceResult.Status}");

                if (deviceResult.Device != null)
                {
                    DumpDevice(deviceResult.Device);
                }
            }
            else
            {
                Dump(deviceResult.Device);
            }            
        }

        private void DumpDevice(IDeviceInfo device)
        {
            var blueDress = device.BluetoothAddress.ToBluetoothAddress();

            Console.WriteLine($"Device name: {device.Name}");
            Console.WriteLine($"Device ID: {device.Id}");
            Console.WriteLine($"Device address: {blueDress}");
        }

        private void Dump(IDeviceInfo device)
        {
            DumpDevice(device);

            Console.WriteLine($"Device attributes: ({device.Characteristics.Count})");

            foreach (var attribute in device.Characteristics)
            {
                Dump(attribute);
            }
        }

        private void Dump(ICharacteristicInfo attribute)
        {
            var formatedProperties = attribute.Properties.ToString();
            Console.WriteLine($"  {attribute.Service.Name}.{attribute.Name} [{formatedProperties}]");
        }
    }
}
