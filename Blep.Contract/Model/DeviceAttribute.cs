namespace Blep.Contract.Model
{
    public class DeviceAttribute
    {
        public DeviceAttribute(IServiceInfo service, ICharacteristicInfo characteristic)
        {
            Service = service;
            Characteristic = characteristic;
        }

        public IServiceInfo Service { get; }

        public ICharacteristicInfo Characteristic { get; }
    }
}
