using Blep.Contract.Model;
using System;
using System.Text;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Blep.Framework.Extension
{
    public static class GattReadResultExtensions
    {
        public static Exception ToException(this GattReadResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (result.Status == GattCommunicationStatus.Success)
                throw new ArgumentException($"Unexpected status: Success", nameof(result));

            //TODO exception per status

            return new ApplicationException($"Read has failed. Status:{result.Status}, Message:{result.ProtocolError}");
        }

        public static object ToValue(this GattReadResult result, GattPresentationFormat presentationFormat)
        {
            return result.ToValue(presentationFormat?.FormatType);
        }

        public static object ToValue(this GattReadResult result, byte? formatType)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (result.Status != GattCommunicationStatus.Success)
                throw new ArgumentException($"Unexpected status: {result.Status}", nameof(result));

            if (formatType == null)
            {
                return result.Value.ToUnknownFormat();
            }

            byte[] data;
            CryptographicBuffer.CopyToByteArray(result.Value, out data);

            return data.ToValue(formatType.Value);
        }

        public static object ToValue(this GattReadResult result, IPresentationFormat presentationFormat)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (result.Status != GattCommunicationStatus.Success)
                throw new ArgumentException($"Unexpected status: {result.Status}", nameof(result));

            if (presentationFormat == null)
            {
                return result.Value.ToUnknownFormat();
            }

            byte[] data;
            CryptographicBuffer.CopyToByteArray(result.Value, out data);

            if (presentationFormat.TryGetValue(data, out object value))
            {
                return value;
            }

            throw new ArgumentException($"Unexpected format of result: {data}", nameof(result));
        }

        private static object ToValue(this byte[] data, byte formatType)
        {
            //TODO use some kind of factory to process format
            if (formatType == GattPresentationFormatTypes.UInt32)
            {
                return data.ToIntValue();
            }
            if (formatType == GattPresentationFormatTypes.Utf8)
            {
                return data.ToStringValue();
            }

            return data.ToUnknownValue();
        }

        private static int ToIntValue(this byte[] data)
        {
            if (data.Length < 4)
                throw new ArgumentException("Value buffer is too small.");

            return BitConverter.ToInt32(data, 0);
        }

        private static string ToStringValue(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        private static string ToUnknownValue(this byte[] data)
        {
            return BitConverter.ToString(data);
        }

        public static string ToUnknownFormat(this IBuffer valueBuffer)
        {
            return CryptographicBuffer.EncodeToHexString(valueBuffer);
        }
    }
}
