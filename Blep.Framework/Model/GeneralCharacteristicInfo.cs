using Blep.Framework.Model;
using Blep.Framework.Registry;
using System;

namespace Blep.Contract.Model
{
    public sealed class GeneralCharacteristicInfo : IdentifiableModelBase, ICharacteristicInfo
    {
        public string Name { get; }
        
        public IPresentationFormat PresentationFormat { get; }

        public IServiceInfo Service { get; }

        public CharacteristicProperties Properties { get; }

        public GeneralCharacteristicInfo(IServiceInfo service, Guid uuid, CharacteristicProperties properties, IPresentationFormat presentationFormat) : base (uuid)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
            PresentationFormat = presentationFormat ?? WellKnownPresentationFormats.Unknown;
            Properties = properties;

            // derive name from UUID
            Name = GetDefaultName();
        }
    }
}
