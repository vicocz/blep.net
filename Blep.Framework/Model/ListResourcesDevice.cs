using System.Collections.Generic;

namespace Blep.Contract.Model
{
    public class ListResourcesDevice : IDeviceInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ulong BluetoothAddress { get; set; }

        public IReadOnlyList<ICharacteristicInfo> Characteristics { get; set; }
    }
}
