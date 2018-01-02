using System.Collections.Generic;

namespace Blep.Contract.Model
{
    public interface IDeviceInfo
    {
        string Id { get; }
        string Name { get;  }
        ulong BluetoothAddress { get; }

        IReadOnlyList<ICharacteristicInfo> Characteristics { get; }
    }
}
