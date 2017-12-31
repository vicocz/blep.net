using Blep.Contract.Model;
using System;

namespace Blep.Framework.Presentation
{
    public abstract class PresentationFormatBase <T> where T : struct
    {
        public Type ValueType => typeof(T);

        protected bool TryGetValue(byte[] sourceData, int minSize, out object value)
        {
            var result = TryGetValue(sourceData, minSize, out T typedValue);

            value = typedValue;
            return result;
        }

        protected bool TryGetValue(byte[] sourceData, int minSize, out T value)
        {
            value = default(T);

            if (sourceData == null)
            {
                return false;
            }

            if (sourceData.Length < minSize)
            {
                return false;
            }

            value = ConvertValue(sourceData);
            return true;
        }

        protected abstract T ConvertValue(byte[] sourceData);
    }
}
