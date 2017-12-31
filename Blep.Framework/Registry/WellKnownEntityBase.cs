using Blep.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blep.Framework.Registry
{
    public class WellKnownEntityBase<TKey, TValue> : IFactory<TKey, TValue> where TValue : class
    {
        protected readonly IDictionary<TKey, TValue> Map;

        protected WellKnownEntityBase() : this (new Dictionary<TKey, TValue>())
        {
        }

        protected WellKnownEntityBase(IDictionary<TKey, TValue> entityMap)
        {
            Map = entityMap;
        }

        protected static IReadOnlyCollection<TValue> CollectEntities(Type container)
        {
            // use reflection to discover requested static variables
            var bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField;

            var fieldValues = container
                                 .GetFields(bindingFlags)
                                 .Where(f => f.FieldType == typeof(TValue))
                                 .Select(field => (TValue)field.GetValue(null))
                                 .ToArray();

            return fieldValues;
        }

        public bool TryCreate(TKey key, out TValue value)
        {
            return Map.TryGetValue(key, out value);
        }
    }
}
