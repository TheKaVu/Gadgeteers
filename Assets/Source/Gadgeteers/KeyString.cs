using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

namespace Source.Gadgeteers
{
    [Serializable]
    public struct KeyString : IEquatable<KeyString>
    {
        public static readonly string Format = @"[a-z0-9_]+(\.[a-z0-9_]+)*";
        
        public static readonly KeyString Empty = new();
        
        [SerializeField]
        private string _value;

        public int Depth { get; }

        public KeyString Root => GetParent(Depth);

        public KeyString(string value)
        {
            if (!Regex.IsMatch(value, "^" + Format + "$")) throw new ArgumentException($"String \"{value}\" doesn't match the key format.");
            _value = value;
            
            Depth = _value.Split('.').Length - 1;
        }

        public KeyString GetParent(int depth = 1)
        {
            if(depth > Depth) return this;
            var path = _value.Split('.');
            var s = path[0];
            for (int i = 1; i < depth; i++)
            {
                s += $".{path[i]}";
            }
            return s;
        }
        
        public bool BelongsTo(KeyString other)
        {
            return _value.IndexOf(other) == 0;
        }
        
        public override string ToString() => _value;

        public bool Equals(KeyString other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is KeyString other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }

        public static bool operator ==(KeyString left, KeyString right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(KeyString left, KeyString right)
        {
            return !(left == right);
        }

        public static implicit operator KeyString(string value)
        {
            return new KeyString(value);
        }
        
        public static implicit operator string(KeyString value)
        {
            return value.ToString();
        }
    }
}