using Blep.Framework.Model;
using System;

namespace Blep.Contract.Model
{
    public class GeneralServiceInfo : IdentifiableModelBase, IServiceInfo
    {
        public string Name { get; }

        public GeneralServiceInfo(Guid uuid) : base(uuid)
        {
            // derive name from UUID
            Name = GetDefaultName();
        }
    }
}
