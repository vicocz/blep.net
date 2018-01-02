using Blep.Contract.Model;
using Blep.Framework.Model;
using System;

namespace Blep.Framework
{
    public sealed class WellKnownCharacteristic : IdentifiableModelBase, ICharacteristicTemplate
    {
        public string Name { get; }

        public IPresentationFormat PresentationFormat { get; }

        public WellKnownCharacteristic(string name, Guid uuid, IPresentationFormat defaultPresentationFormat) 
            : base (uuid)
        {
            Name = name;
            PresentationFormat = defaultPresentationFormat;
        }

        public WellKnownCharacteristic(string name, ushort shortId, IPresentationFormat defaultPresentationFormat)
            : base(shortId)
        {
            Name = name;

            PresentationFormat = defaultPresentationFormat;
        }
    }
}
