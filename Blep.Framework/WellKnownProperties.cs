using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blep.Framework
{
    /// <summary>
    /// Set of well known properties, see: https://msdn.microsoft.com/en-us/library/windows/desktop/ff521659(v=vs.85).aspx
    /// </summary>
    public static class WellKnownProperties
    {
        public static readonly string[] BaseProperties = 
        {
            "System.Devices.Aep.DeviceAddress",
            "System.Devices.Aep.IsConnected",
            "System.Devices.Aep.Bluetooth.Le.IsConnectable"
        };

        public static readonly string AepDeviceAddress = "System.Devices.Aep.DeviceAddress";
    }
}
