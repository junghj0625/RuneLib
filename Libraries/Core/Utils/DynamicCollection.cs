using System;
using System.Collections.Generic;
using UnityEngine;



namespace Rune
{
    [Obsolete]
    public class DynamicCollection
    {
        public Attribute<T> SetAttribute<T>(string name, T valueDefault = default)
        {
            if (_attributes.ContainsKey(name)) { Debug.LogWarning($"Attribute already exist. ({name})"); return null; }

            var attr = new Attribute<T> { Value = valueDefault, DefaultValue = valueDefault };

            _attributes.Add(name, attr);

            return attr;
        }

        public Attribute<T> GetAttribute<T>(string name)
        {
            if (!_attributes.ContainsKey(name)) { Debug.LogWarning($"Attribute not found. ({name})"); return null; }

            return _attributes[name] as Attribute<T>;
        }

        public void SetAttributeValue<T>(string name, T value)
        {
            var attr = GetAttribute<T>(name);

            if (attr != null) attr.Value = value;
        }

        public T GetAttributeValue<T>(string name)
        {
            var attr = GetAttribute<T>(name);

            return attr != null ? attr.Value : default;
        }

        public void ResetAttributes()
        {
            foreach (var attribute in _attributes.Values) attribute.Reset();
        }


        public LooseEvent SetLooseEvent(string key)
        {
            if (_looseEvents.ContainsKey(key)) { Debug.LogWarning($"Loose event already exist. ({key})"); return null; }

            var looseEvent = new LooseEvent();

            _looseEvents.Add(key, looseEvent);

            return looseEvent;
        }

        public LooseEvent GetLooseEvent(string key)
        {
            if (!_looseEvents.ContainsKey(key)) { Debug.LogWarning($"Loose event not found. ({key})"); return null; }

            return _looseEvents[key] as LooseEvent;
        }

        public LooseEvent<T> SetLooseEvent<T>(string key)
        {
            if (_looseEvents.ContainsKey(key)) { Debug.LogWarning($"Loose event already exist. ({key})"); return null; }

            var looseEvent = new LooseEvent<T>();

            _looseEvents.Add(key, looseEvent);

            return looseEvent;
        }

        public LooseEvent<T> GetLooseEvent<T>(string key)
        {
            if (!_looseEvents.ContainsKey(key)) { Debug.LogWarning($"Loose event not found. ({key})"); return null; }

            return _looseEvents[key] as LooseEvent<T>;
        }


        public Registry<T> SetRegistry<T>(string name)
        {
            if (_registries.ContainsKey(name)) { Debug.LogWarning("Registry already exist. (" + name + ")"); return null; }

            var registry = new Registry<T>();

            _registries.Add(name, registry);

            return registry;
        }

        public Registry<T> GetRegistry<T>(string name)
        {
            if (!_registries.ContainsKey(name)) { Debug.LogWarning("Registry not found. (" + name + ")"); return null; }

            return _registries[name] as Registry<T>;
        }



        private readonly Dictionary<string, AttributeBase> _attributes = new();

        private readonly Dictionary<string, LooseEventBase> _looseEvents = new();

        private readonly Dictionary<string, RegistryBase> _registries = new();
    }
}