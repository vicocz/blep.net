using Blep.Contract.Model;
using System;

namespace Blep.Framework.Presentation
{
    public class Int16PresentationFormat : PresentationFormatBase<Int16>, IPresentationFormat
    {
        public bool TryGetValue(byte[] sourceData, out object value)
        {
            return TryGetValue(sourceData, sizeof(Int16), out value);
        }

        protected override short ConvertValue(byte[] sourceData)
        {
            return BitConverter.ToInt16(sourceData, 0);
        }
    }
}
