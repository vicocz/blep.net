﻿using System;

namespace Blep.Contract.Model
{
    public interface ICharacteristicInfo
    {
        string Name { get; }

        Guid Uuid { get; }

        IPresentationFormat PresentationFormat { get; }

        IServiceInfo Service { get; }

        CharacteristicProperties Properties { get; }
    }
}
