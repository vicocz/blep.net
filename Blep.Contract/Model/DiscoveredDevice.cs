using System.Collections.Generic;

namespace Blep.Contract.Model
{
    public class DiscoveredDevice
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public IReadOnlyList<DeviceAttribute> Attributes { get; set; }
    }
}
