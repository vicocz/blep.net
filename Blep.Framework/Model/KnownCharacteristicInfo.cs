using Blep.Contract.Model;
using System;

namespace Blep.Framework.Model
{
    /// <summary>
    /// Represents odel of well-known characteristic
    /// </summary>
    public class KnownCharacteristicInfo : ICharacteristicInfo
    {
        public ICharacteristicTemplate Template { get; }

        public string Name => Template.Name;

        public Guid Uuid => Template.Uuid;

        public IPresentationFormat PresentationFormat { get; }

        public IServiceInfo Service { get; }

        public CharacteristicProperties Properties { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="template">Requiered characteristic template</param>
        /// <param name="service">Required parent service</param>
        /// <param name="presentationFormat">Optional overriding of presentation format</param>
        public KnownCharacteristicInfo(ICharacteristicTemplate template, IServiceInfo service, CharacteristicProperties properties, IPresentationFormat presentationFormat)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
            Service = service ?? throw new ArgumentNullException(nameof(service));
            PresentationFormat = presentationFormat ?? template.PresentationFormat;
            Properties = properties;
        }
    }
}
