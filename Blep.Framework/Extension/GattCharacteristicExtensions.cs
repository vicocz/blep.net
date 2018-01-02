using Blep.Contract.Model;
using Blep.Framework.Extension;
using Blep.Framework.Model;
using Blep.Framework.Registry;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Blep.Framework
{
    public static class GattCharacteristicExtensions
    {
        public static ICharacteristicInfo ToCharacteristicInfo(this GattCharacteristic characteristic)
        {
            var serviceInfo = characteristic.Service.ToServiceInfo();
            IPresentationFormat preferedFormat = null;
            var properties = characteristic.CharacteristicProperties.ToContract();

            // use default factory to recognize known services
            if (WellKnownCharacteristics.Default.TryCreate(characteristic.Uuid, out ICharacteristicTemplate template))
            {
                return new KnownCharacteristicInfo(template, serviceInfo, properties, preferedFormat);
            }

            return new GeneralCharacteristicInfo(serviceInfo, characteristic.Uuid, properties, preferedFormat);
        }

        public static CharacteristicProperties ToContract(this GattCharacteristicProperties gattProperties)
        {
            var properties = CharacteristicProperties.None;

            if (gattProperties.HasFlag(GattCharacteristicProperties.Broadcast))
                properties |= CharacteristicProperties.Broadcast;
            if (gattProperties.HasFlag(GattCharacteristicProperties.Read))
                properties |= CharacteristicProperties.Read;
            if (gattProperties.HasFlag(GattCharacteristicProperties.Write))
                properties |= CharacteristicProperties.Write;
            if (gattProperties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                properties |= CharacteristicProperties.WriteWithoutResponse;
            if (gattProperties.HasFlag(GattCharacteristicProperties.Indicate))
                properties |= CharacteristicProperties.Indicate;

            return properties;
        }
    }
}
