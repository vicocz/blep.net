using System;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;

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

        public static ulong ToBluetoothAddress(this string physicalAddress)
        {
            if (physicalAddress.Length != 17)
            {
                throw new FormatException($"The specified address {physicalAddress} is not a valid Bluetooth address.");
            }

            if (physicalAddress.IndexOf(":", StringComparison.Ordinal) > -1)
            {
                return ToBluetoothAddress(physicalAddress, ':');
            }

            throw new ArgumentException(nameof(physicalAddress));
        }

        private static ulong ToBluetoothAddress(string physicalAddress, char separator)
        {
            byte[] bytes = new byte[8];
            string[] parts = physicalAddress.Split(separator);
            for (int index = 0; index < 6; index++)
            {
                var part = parts[5 - index];
                if (!byte.TryParse(part, NumberStyles.HexNumber, invariantProvider, out byte value))
                {
                    throw new FormatException($"Failed to parse part '{part}' of Bluetooth address '{physicalAddress}");
                }
                bytes[index] = value;
            }
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static string ToBluetoothAddress(this ulong bluetoothAddress)
        {
            return bluetoothAddress.ToString("X6", invariantProvider);

        }

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

        public static string ToHexString(this uint value)
        {
            return value.ToString("X8", invariantProvider);
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
