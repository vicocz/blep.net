using System;

namespace Blep.Contract.Model
{
    public interface IPresentationFormat
    {
        Type ValueType { get; }

        bool TryGetValue(byte[] sourceData, out object value);
    }
}
