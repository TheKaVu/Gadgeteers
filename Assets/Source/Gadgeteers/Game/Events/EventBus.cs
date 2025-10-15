using System;

namespace Source.Gadgeteers.Game.Events
{
    public static class EventBus<T> where T : IEvent
    {
        public static string Id = typeof(T).Name;
        
        public static event EventHandler<T> OnEvent;
        
        public static T Call(object sender, T e)
        {
            OnEvent?.Invoke(sender, e);
            return e;
        }
    }
}