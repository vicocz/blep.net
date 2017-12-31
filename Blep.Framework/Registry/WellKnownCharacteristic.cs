using Blep.Contract.Model;
using Blep.Framework.Extension;
using Blep.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blep.Framework
{
    public sealed class WellKnownCharacteristic : IdentifiableModelBase, ICharacteristicInfo
    {
        public string Name { get; }

        public IPresentationFormat DefaultPresentationFormat { get; }

        public WellKnownCharacteristic(string name, Guid uuid, IPresentationFormat defaultPresentationFormat) 
            : base (uuid)
        {
            Name = name;
            DefaultPresentationFormat = defaultPresentationFormat;
        }

        public WellKnownCharacteristic(string name, ushort shortId, IPresentationFormat defaultPresentationFormat)
            : base(shortId)
        {
            Name = name;

            DefaultPresentationFormat = defaultPresentationFormat;
        }
    }
}
