using System;

namespace Blep.Contract.Model
{
    public interface IServiceInfo
    {
        string Name { get; }

        Guid Uuid { get; }
    }
}
