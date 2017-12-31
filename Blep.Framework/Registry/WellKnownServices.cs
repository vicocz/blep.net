using Blep.Contract;
using Blep.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blep.Framework.Registry
{
    public class WellKnownServices : WellKnownEntityBase<Guid, IServiceInfo>
    {
        static WellKnownServices()
        {
            var entities = CollectEntities(typeof(WellKnownServices));

            Default = new WellKnownServices(entities);
        }

        /// <summary>
        /// Default factory
        /// </summary>
        public static readonly IFactory<Guid, IServiceInfo> Default;

        public WellKnownServices(IEnumerable<IServiceInfo> knownServices)
            : base (knownServices.ToDictionary(e => e.Uuid, e => e))
        {
        }

        public static readonly IServiceInfo GenericAccess = new WellKnownService("GenericAccess", 0x1800);

        public static readonly IServiceInfo DeviceInformation = new WellKnownService("DeviceInformation", 0x180A);
    }
}
