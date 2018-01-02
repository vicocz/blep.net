using Blep.Framework.Discovery;
using Blep.Framework.Extension;
using System;
using System.Linq;

namespace Blep.Diag.Commands
{
    public class DiscoveryCommand
    {
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        public void Execute()
        {
            Console.Write("Bluetooth Low Energy device discovery in progress...");

            var factory = new DiscoveryFactory().Create();
            var discoveredDevices = factory.EnumerateDevicesAsync(DefaultTimeout).Result.ToList();

            Console.WriteLine();
            Console.WriteLine($"Discovered devices: ({discoveredDevices.Count})");

            foreach (var device in discoveredDevices)
            {
                var blueDress = device.BluetoothAddress.ToBluetoothAddress();
                Console.WriteLine($"Device: {device.Name}, Id: {device.Id}, Address={blueDress}");
            }
        }
    }
}
