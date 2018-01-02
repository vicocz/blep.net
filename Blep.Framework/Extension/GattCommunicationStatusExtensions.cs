using Blep.Contract.Model;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Blep.Framework.Extension
{
    public static class GattCommunicationStatusExtensions
    {
        public static ResourceEnumerationResult.EnumerationStatus ToEnumerationStatus(this GattCommunicationStatus status)
        {
            if (status == GattCommunicationStatus.Success)
                return ResourceEnumerationResult.EnumerationStatus.Success;
            if (status == GattCommunicationStatus.AccessDenied)
                return ResourceEnumerationResult.EnumerationStatus.AccessDenied;
            if (status == GattCommunicationStatus.Unreachable)
                return ResourceEnumerationResult.EnumerationStatus.Unreachable;

            return ResourceEnumerationResult.EnumerationStatus.ProtocolError;
        }
    }
}
