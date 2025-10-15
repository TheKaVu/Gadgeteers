using System;
using UnityEditor;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public struct Modifier : IComparable<Modifier>, IEquatable<Modifier>
    {
        [SerializeField]
        private Operation _operation;
        [SerializeField]
        private float _arg;
        [SerializeField]
        private KeyString _target;
        private Optional<int> _flagFilter;
        private GUID _id;

        public Operation Operation => _operation;
        public float Arg => _arg;
        public KeyString Target => _target;
        public Optional<int> FlagFilter => _flagFilter;

        public Modifier(Operation type, float arg, KeyString target)
        {
            _operation = type;
            _arg = arg;
            _target = target;
            _flagFilter = new(0, false);
            _id = new GUID();
        }
        public Modifier(Operation type, float arg, KeyString target, int flagFilter)
        {
            _operation = type;
            _arg = arg;
            _target = target;
            _flagFilter = new(flagFilter);
            _id = new GUID();
        }

        public float Process(float value)
        {
            return _operation switch
            {
                Operation.Add => value + _arg,
                Operation.AddMultiplied => value * (_arg + 1),
                _ => value,
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Modifier modifier &&
                   _id.Equals(modifier._id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id);
        }

        public int CompareTo(Modifier other)
        {
            return _operation.CompareTo(other._operation);
        }

        public bool Equals(Modifier other)
        {
            return _id.Equals(other._id);
        }

        public override string ToString()
        {
            var sign = _arg >= 0 ? "+" : string.Empty;
            switch (_operation)
            {
                case Operation.Add:
                    return $"{sign}{_arg} {_target}";
                case Operation.AddMultiplied:
                    return $"{sign}{_arg * 100}% {_target}";
            }

            return string.Empty;
        }
    }
}