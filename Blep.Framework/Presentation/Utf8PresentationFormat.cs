using Blep.Contract.Model;
using System;
using System.Text;

namespace Blep.Framework.Presentation
{
    public class Utf8PresentationFormat : IPresentationFormat
    {
        public Type ValueType => typeof(string);

        public bool TryGetValue(byte[] sourceData, out object value)
        {
            if (sourceData == null)
            {
                value = null;
                return false;
            }

            try
            {
                value = Encoding.UTF8.GetString(sourceData);
                return true;
            }
            catch (DecoderFallbackException)
            {
                //TODO log exception
            }

            value = null;
            return false;
        }
    }
}
