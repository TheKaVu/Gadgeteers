using System;
using UnityEngine;

namespace Source.Gadgeteers
{
    [Serializable]
    public class Ref<T>
    {
        [SerializeField]
        private T _value;
        public T Value { get => _value; set => _value = value; }
        
        public Ref(T value)
        {
            _value = value;
        }

        public static implicit operator Ref<T>(T value)
        {
            return new Ref<T>(value);
        }

        public static implicit operator T(Ref<T> value)
        {
            return value.Value;
        }
    }
}