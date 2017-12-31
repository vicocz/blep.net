using Blep.Contract.Model;
using System;

namespace Blep.Framework.Presentation
{
    /// <summary>
    /// Kinda fallback presentation format
    /// </summary>
    public class UnknownPresentationFormat : IPresentationFormat
    {
        public Type ValueType => typeof(string);

        public bool TryGetValue(byte[] sourceData, out object value)
        {
            value = sourceData == null ? null : BitConverter.ToString(sourceData);
            return sourceData != null;
        }
    }
}
