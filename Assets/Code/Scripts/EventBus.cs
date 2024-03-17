using System;
using System.Collections.Generic;

namespace Code.Scripts
{
    public sealed class EventBus
    {
        private readonly Dictionary<Type, List<object>> listenersByEventTypes = new();

        public void Subscribe<T>(Action<T> listener)
        {
            if (!listenersByEventTypes.ContainsKey(typeof(T)))
            {
                listenersByEventTypes.Add(typeof(T), new List<object>());
            }
            
            listenersByEventTypes[typeof(T)].Add(listener);
        }
        
        public void Unsubscribe<T>(Action<T> listener)
        {
            if (!listenersByEventTypes.ContainsKey(typeof(T)))
            {
                return;
            }
            
            listenersByEventTypes[typeof(T)].Remove(listener);
        }
        
        public void RaiseEvent<T>(T @event)
        {
            if (!listenersByEventTypes.ContainsKey(typeof(T)))
            {
                return;
            }

            foreach (var listenerObject in listenersByEventTypes[typeof(T)])
            {
                if (listenerObject is Action<T> listener)
                {
                    listener?.Invoke(@event);
                }
            }
        }
    }
}