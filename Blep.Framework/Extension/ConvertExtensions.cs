using System;
using System.Globalization;
using System.Linq;

namespace Blep.Framework.Extension
{
    public static class ConvertExtensions
    {
        private static readonly byte[] bluetoothBaseUuid = new byte[]
        {
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00,
            0x00, 0x10,
            0x80, 0x00,
            0x00, 0x80, 0x5F, 0x9B, 0x34, 0xFB,
        };

        private static readonly IFormatProvider invariantProvider = CultureInfo.InvariantCulture;

        /// <summary>
        /// Converts standard 128bit UUID to the well known 32bit UUID
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static ushort ToShortId(this Guid uuid)
        {
            // Extract short Uuid
            var bytes = uuid.ToByteArray();
            return (ushort)(bytes[0] | (bytes[1] << 8));
        }

        public static Guid ToUuid(this ushort shortId)
        {
            var uuidBytes = new byte[16];

            bluetoothBaseUuid.CopyTo(uuidBytes, 0);
            // apply short Uuid
            uuidBytes[0] = (byte)(shortId & 0xFF);
            uuidBytes[1] = (byte)(shortId >> 8);

            return new Guid(uuidBytes);
        }

        public static string ToHexString(this ushort value)
        {
            return value.ToString("X4", invariantProvider);
        }

        public static string ToInvariantString(this ushort value)
        {
            return value.ToString(invariantProvider);
        }

        public static string ToInvariantString(this Guid value)
        {
            return value.ToString("D", invariantProvider);
        }

        public static bool IsSigDefinedUuid(this Guid uuid)
        {
            var uuidBytes = uuid.ToByteArray();
            uuidBytes[0] = 0;
            uuidBytes[1] = 0;

            return uuidBytes.SequenceEqual(bluetoothBaseUuid);
        }
    }
}
