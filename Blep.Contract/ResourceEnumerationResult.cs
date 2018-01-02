namespace Blep.Contract.Model
{
    public class ResourceEnumerationResult
    {
        public enum EnumerationStatus
        {
            Success,
            Unreachable,
            ProtocolError,
            AccessDenied
        };

        public IDeviceInfo Device { get; }

        public EnumerationStatus Status { get; }

        public ResourceEnumerationResult(EnumerationStatus status) : this (null, status)
        {
        }

        public ResourceEnumerationResult(IDeviceInfo device, EnumerationStatus status)
        {
            Device = device;
            Status = status;
        }
    }
}