using System;

namespace Source.Gadgeteers
{
    public class PrefabNotFoundException : Exception
    {
        public PrefabNotFoundException(string message) : base(message){}
    }
}