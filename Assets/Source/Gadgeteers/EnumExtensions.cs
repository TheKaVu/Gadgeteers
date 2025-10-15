using System;
using System.Linq;

namespace Source.Gadgeteers
{
    public static class EnumExtensions
    {
        public static T[] GetActiveFlags<T>(this T t) where T : Enum
        {
            var activeFlags = Enum.GetValues(typeof(T)).Cast<T>().Where(flag => t.HasFlag(flag)).ToList();
            return activeFlags.ToArray();
        }
    }
}