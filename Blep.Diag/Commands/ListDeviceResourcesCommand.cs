using Blep.Contract.Model;
using Blep.Framework.Discovery;
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
            var deviceInfo = discovery.EnumerateResources(_deviceId);

            Console.WriteLine();

            Dump(deviceInfo);
        }

        private void Dump(DiscoveredDevice device)
        {
            Console.WriteLine($"Device name: {device.Name}");
            Console.WriteLine($"Device ID: {device.Id}");
            Console.WriteLine($"Device address: {device.Address}");

            Console.WriteLine($"Device attributes: ({device.Attributes.Count})");

            foreach (var attribute in device.Attributes)
            {
                Dump(attribute);
            }
        }

        private void Dump(DeviceAttribute attribute)
        {
            Console.WriteLine($"  {attribute.Service.Name}.{attribute.Characteristic.Name}");
        }
    }
}
