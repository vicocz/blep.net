using Blep.Contract.Model;
using System;

namespace Blep.Framework.Presentation
{
    public class UInt16PresentationFormat : PresentationFormatBase<UInt16>, IPresentationFormat
    {
        public bool TryGetValue(byte[] sourceData, out object value)
        {
            return TryGetValue(sourceData, sizeof(UInt16), out value);
        }

        protected override ushort ConvertValue(byte[] sourceData)
        {
            return BitConverter.ToUInt16(sourceData, 0);
        }
    }
}
