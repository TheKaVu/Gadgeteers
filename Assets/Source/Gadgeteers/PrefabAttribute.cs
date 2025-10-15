using System;

namespace Source.Gadgeteers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabAttribute : Attribute
    {
        public string Id { get; }
        
        public PrefabAttribute(string id)
        {
            Id = id;
        }

        public PrefabAttribute(string path, string id) : this($"{path}{id}")
        {
        }
    }
}