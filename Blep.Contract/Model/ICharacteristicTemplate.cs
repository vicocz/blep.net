using System;

namespace Blep.Contract.Model
{
    /// <summary>
    /// Represents well-known GATT characteristics  
    /// </summary>
    public interface ICharacteristicTemplate
    {
        string Name { get; }

        Guid Uuid { get; }

        IPresentationFormat PresentationFormat { get; }
    }
}
