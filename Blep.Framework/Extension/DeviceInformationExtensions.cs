using Blep.Contract.Model;
using System.Collections.Generic;
using Windows.Devices.Enumeration;

namespace Blep.Framework
{
    internal static class DeviceInformationExtensions
    {
        internal static DiscoveredDevice ToDiscoveredDevice(this DeviceInformation device)
        {
            return new DiscoveredDevice
            {
                Id = device.Id,
                Name = device.Name,
                Address = device.Properties.ToProperty<string>(WellKnownProperties.AepDeviceAddress),

                Attributes = new DeviceAttribute[0],
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
