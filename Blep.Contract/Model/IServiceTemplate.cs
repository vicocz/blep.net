using System;

namespace Blep.Contract.Model
{
    public interface IServiceTemplate
    {
        string Name { get; }

        Guid Uuid { get; }
    }
}
