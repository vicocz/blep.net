using Blep.Contract.Model;
using System;

namespace Blep.Framework.Presentation
{
    public class Int32PresentationFormat : IPresentationFormat
    {
        public Type ValueType => typeof(Int32);

        public bool TryGetValue(byte[] sourceData, out object value)
        {
            if (sourceData == null)
            {
                value = null;
                return false;
            }

            if (sourceData.Length < 4)
            {
                value = null;
                return false;
            }

            value = BitConverter.ToInt32(sourceData, 0);
            return true;
        }
    }
}
