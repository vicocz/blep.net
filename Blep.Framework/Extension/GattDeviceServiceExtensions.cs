﻿using Blep.Contract.Model;
using Blep.Framework.Registry;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Blep.Framework.Extension
{
    public static class GattDeviceServiceExtensions
    {
        public static IServiceInfo ToServiceInfo(this GattDeviceService service)
        {
            // use default factory to recognize known services
            if (WellKnownServices.Default.TryCreate(service.Uuid, out IServiceInfo value))
            {
                return value;
            }

            // use generic service
            return new GeneralServiceInfo(service.Uuid);
        }
    }
}
