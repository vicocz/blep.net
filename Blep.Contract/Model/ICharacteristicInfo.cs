using System;

namespace Blep.Contract.Model
{
    public interface ICharacteristicInfo
    {
        string Name { get; }

        Guid Uuid { get; }

        IPresentationFormat DefaultPresentationFormat { get; }
    }
}
