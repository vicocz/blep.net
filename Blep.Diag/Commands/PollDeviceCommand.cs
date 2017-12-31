using Blep.Contract.Model;
using Blep.Framework.Discovery;
using Blep.Framework.Polling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var deviceInfo = discovery.EnumerateResources(_deviceId);

            PollAttributes(deviceInfo);
        }

        private void PollAttributes(Contract.Model.DiscoveredDevice device)
        {
            Console.WriteLine();
            Console.WriteLine($" - device found having {device.Attributes.Count} attributes");

            Console.Write(" - Polling attributes ...");
            Console.WriteLine();

            foreach (var attribute in device.Attributes)
            {
                PollAttribute(device.Id, attribute);
            }
        }

        private void PollAttribute(string deviceId, DeviceAttribute attribute)
        {
            var poller = new ActiveDevicePollerFactory().Create(attribute);

            if (poller.TryPollDevice(deviceId, out object value))
            {
                string formatedValue = value.ToString();

                Console.WriteLine($"  {attribute.Service.Name}.{attribute.Characteristic.Name}: {formatedValue}");
            }
            else
            {
                Console.WriteLine($"  Failed to poll {attribute.Service.Name}.{attribute.Characteristic.Name}");
            }

        }
    }
}
