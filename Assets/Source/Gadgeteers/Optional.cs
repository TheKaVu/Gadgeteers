using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Gadgeteers
{
    [Serializable]
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        [SerializeField]
        private T _value;
        [SerializeField]
        private bool _enabled;

        public readonly T Value => _value;
        public readonly bool Enabled => _enabled;

        public Optional(T value, bool enabled = true)
        {
            _value = value;
            _enabled = enabled;
        }

        public readonly bool Equals(Optional<T> other)
        {
            return Equals((object)other);
        }

        public readonly override bool Equals(object obj)
        {
            return obj is Optional<T> optional &&
                   EqualityComparer<T>.Default.Equals(_value, optional._value);
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }
    }
}
