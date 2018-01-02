using Blep.Contract;
using Blep.Contract.Model;
using System;

namespace Blep.Framework.Polling
{
    public class ActiveDevicePollerFactory
    {
        public IActiveDevicePoller Create(Guid service, Guid characteristic)
        {
            return new SimpleDevicePoller(service, characteristic);
        }

        public IActiveDevicePoller Create(ICharacteristicInfo characteristic)
        {
            return new SimpleDevicePoller(characteristic.Service.Uuid, characteristic.Uuid, characteristic.PresentationFormat);
        }
    }
}
