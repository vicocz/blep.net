using Blep.Contract.Model;
using Blep.Framework.Extension;
using Blep.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;

namespace Blep.Framework.Registry
{
    public sealed class WellKnownService : IdentifiableModelBase, IServiceInfo
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
