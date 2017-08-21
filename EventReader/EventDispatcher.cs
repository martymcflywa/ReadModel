using Newtonsoft.Json;
using ReadModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventReader
{
    public class EventDispatcher : IEventRegister
    {
        private readonly Dictionary<EventKey, Action<object>> _eventRegister;

        public EventDispatcher()
        {
            _eventRegister = new Dictionary<EventKey, Action<object>>();
        }

        public void RegisterEventHandler<T>(short aggregateTypeId, short messageTypeId, Action<T> eventHandler)
        {
            _eventRegister[new EventKey(aggregateTypeId, messageTypeId)] = e => eventHandler((T) e);
        }

        public void Dispatch(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                var handler = _eventRegister[e.Key];
                handler(e);
            }
        }
    }
}
