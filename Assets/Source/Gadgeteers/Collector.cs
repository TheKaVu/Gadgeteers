using System;
using System.Collections.Generic;
using System.Linq;

namespace Source.Gadgeteers
{
    public class Collector<T>
    {
        private Func<IEnumerable<T>> _supply;

        public List<T> Invoke()
        {
            List<T> result = new();
            if(_supply == null) return result;
            foreach (var d in _supply.GetInvocationList())
            {
                result.AddRange((IEnumerable<T>)d.DynamicInvoke()); 
            }
            return result;
        }

        public void Add(Func<IEnumerable<T>> supply)
        {
            _supply += supply;
        }

        public void Remove(Func<IEnumerable<T>> supply)
        {
            _supply -= supply;
        }
        
        public static Collector<T> operator +(Collector<T> c, Func<IEnumerable<T>> supply)
        {
            c.Add(supply);
            return c;
        }
        
        public static Collector<T> operator -(Collector<T> c, Func<IEnumerable<T>> supply)
        {
            c.Remove(supply);
            return c;
        }
    }
}