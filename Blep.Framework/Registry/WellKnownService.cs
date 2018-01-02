using Blep.Contract.Model;
using Blep.Framework.Model;
using System;

namespace Blep.Framework.Registry
{
    public sealed class WellKnownService : IdentifiableModelBase, IServiceTemplate
    {
        public string Name { get; }
        
        public WellKnownService(string name, Guid uuid) : base (uuid)
        {
            Name = name;
        }

        public WellKnownService(string name, ushort shortId) : base(shortId)
        {
            Name = name;
        }
    }
}
