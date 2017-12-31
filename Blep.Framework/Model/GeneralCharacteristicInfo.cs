using Blep.Framework.Model;
using Blep.Framework.Registry;
using System;

namespace Blep.Contract.Model
{
    public sealed class GeneralCharacteristicInfo : IdentifiableModelBase, ICharacteristicInfo
    {
        public string Name { get; }

        public IPresentationFormat DefaultPresentationFormat => WellKnownPresentationFormats.Unknown;

        public GeneralCharacteristicInfo(Guid uuid) : base (uuid)
        {
            // derive name from UUID
            Name = GetDefaultName();
        }
    }
}
