using Blep.Contract.Model;
using Blep.Framework.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blep.Diag.Commands
{
    public class DiscoveryCommand
    {
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        public void Execute()
        {
            Console.Write("BLE device discovery in progress...");

            var factory = new DiscoveryFactory().Create();
            var discoveredDevices = factory.EnumerateDevicesAsync(DefaultTimeout).Result.ToList();

            Console.WriteLine();
            Console.WriteLine($"Discovered devices: ({discoveredDevices.Count})");

            foreach (var device in discoveredDevices)
            {
                Console.WriteLine($"Device: {device.Name}, Id: '{device.Id}', Address={device.Address}");
            }
        }
    }
}
