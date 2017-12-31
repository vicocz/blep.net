﻿using Blep.Contract;
using Blep.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blep.Framework.Registry
{
    public class WellKnownCharacteristics : WellKnownEntityBase<Guid, ICharacteristicInfo>
    {
        static WellKnownCharacteristics()
        {
            var entities = CollectEntities(typeof(WellKnownCharacteristics));

            Default = new WellKnownCharacteristics(entities);
        }

        /// <summary>
        /// Default factory
        /// </summary>
        public static readonly IFactory<Guid, ICharacteristicInfo> Default;

        public WellKnownCharacteristics(IEnumerable<ICharacteristicInfo> knownCharcteristics)
            : base(knownCharcteristics.ToDictionary(e => e.Uuid, e => e))
        {
        }

        // GenericAccess
        public static readonly ICharacteristicInfo DeviceName =
            new WellKnownCharacteristic
            (
                "DeviceName",
                0x2A00,
                WellKnownPresentationFormats.Utf8
            );

        public static readonly ICharacteristicInfo Appearance =
            new WellKnownCharacteristic
            (
                "Appearance",
                0x2A01,
                WellKnownPresentationFormats.UInt16
            );

        public static readonly ICharacteristicInfo ModelNumberString =
            new WellKnownCharacteristic
            (
                "ModelNumberString",
                0x2A24,
                WellKnownPresentationFormats.Utf8
            );
        public static readonly ICharacteristicInfo FirmwareRevisionString =
            new WellKnownCharacteristic
            (
                "FirmwareRevisionString",
                0x2A26,
                WellKnownPresentationFormats.Utf8
            );
        public static readonly ICharacteristicInfo HardwareRevisionString =
            new WellKnownCharacteristic
            (
                "HardwareRevisionString",
                0x2A27,
                WellKnownPresentationFormats.Utf8
            );
        public static readonly ICharacteristicInfo SoftwareRevisionString =
            new WellKnownCharacteristic
            (
                "SoftwareRevisionString",
                0x2A28,
                WellKnownPresentationFormats.Utf8
            );
        public static readonly ICharacteristicInfo ManufacturerNameString =
            new WellKnownCharacteristic
            (
                "ManufacturerNameString",
                0x2A29,
                WellKnownPresentationFormats.Utf8
            );

    }
}
