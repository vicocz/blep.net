using Blep.Contract.Model;
using Blep.Framework.Extension;
using Blep.Framework.Registry;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Blep.Framework
{
    public static class GattCharacteristicExtensions
    {
        public static DeviceAttribute ToDeviceAttribute(this GattCharacteristic characteristic)
        {
            return new DeviceAttribute
            (
                characteristic.Service.ToServiceInfo(),
                characteristic.ToCharacteristicInfo()
            );
        }

        public static ICharacteristicInfo ToCharacteristicInfo(this GattCharacteristic characteristice)
        {
            // use default factory to recognize known services
            if (WellKnownCharacteristics.Default.TryCreate(characteristice.Uuid, out ICharacteristicInfo value))
            {
                return value;
            }

            return new GeneralCharacteristicInfo(characteristice.Uuid);
        }
    }
}
