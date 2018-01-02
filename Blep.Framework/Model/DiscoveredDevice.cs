using System.Collections.Generic;
using System.Linq;

namespace Blep.Contract.Model
{
    public class DiscoveredDevice : IDeviceInfo
    {
        private static readonly IReadOnlyList<ICharacteristicInfo> EmptyList = new ICharacteristicInfo[0];

        public string Id { get; set; }
        public string Name { get; set; }

        public ulong BluetoothAddress { get; set; }

        public IReadOnlyList<ICharacteristicInfo> Characteristics => EmptyList;
    }
}
