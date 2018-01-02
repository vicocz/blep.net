using Blep.Contract.Model;
using Blep.Framework.Discovery;
using Blep.Framework.Polling;
using System;

namespace Blep.Diag.Commands
{
    public class PollDeviceCommand
    {
        private readonly string _deviceId;

        public PollDeviceCommand(string deviceId)
        {
            _deviceId = deviceId;
        }

        public void Execute()
        {
            Console.WriteLine($"Polling device: {_deviceId}");
            Console.Write(" - List resources ...");

            var discovery = new DiscoveryFactory().Create();
            var deviceResult = discovery.EnumerateResources(_deviceId);

            Console.WriteLine();

            if (deviceResult.Status != ResourceEnumerationResult.EnumerationStatus.Success)
            {
                Console.WriteLine($"Operation has failed due to status: {deviceResult.Status}");
            }
            else
            {
                PollAttributes(deviceResult.Device);
            }
        }

        private void PollAttributes(IDeviceInfo device)
        {
            
            Console.WriteLine($" - device found having {device.Characteristics.Count} attributes");

            Console.Write(" - Polling attributes ...");
            Console.WriteLine();

            foreach (var attribute in device.Characteristics)
            {
                PollAttribute(device.Id, attribute);
            }
        }

        private void PollAttribute(string deviceId, ICharacteristicInfo attribute)
        {
            var poller = new ActiveDevicePollerFactory().Create(attribute);

            if (poller.TryPollDevice(deviceId, out object value))
            {
                string formatedValue = value.ToString();

                Console.WriteLine($"  {attribute.Service.Name}.{attribute.Name}: {formatedValue}");
            }
            else
            {
                Console.WriteLine($"  Failed to poll {attribute.Service.Name}.{attribute.Name}");
            }

        }
    }
}