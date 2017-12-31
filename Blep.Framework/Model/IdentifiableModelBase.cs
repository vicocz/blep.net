using Blep.Framework.Extension;
using System;

namespace Blep.Framework.Model
{
    public class IdentifiableModelBase
    {
        protected IdentifiableModelBase(Guid uuid)
        {
            Uuid = uuid;
        }

        protected IdentifiableModelBase(ushort shortId)
        {
            Uuid = shortId.ToUuid();
        }

        public Guid Uuid { get; }

        public bool IsSigDefined => Uuid.IsSigDefinedUuid();

        protected string GetDefaultName()
        {
            return IsSigDefined
                ? Uuid.ToShortId().ToHexString()
                : Uuid.ToInvariantString();
        }
    }
}
