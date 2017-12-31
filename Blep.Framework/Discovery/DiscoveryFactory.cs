using Blep.Contract;

namespace Blep.Framework.Discovery
{
    public class DiscoveryFactory
    {
        public IDeviceDiscovery Create()
        {
            return new SimpleDiscovery();
        }
    }
}
