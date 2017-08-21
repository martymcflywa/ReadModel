using System;
using System.Collections.Generic;

namespace ReadModel.Events
{
    public interface IEventRegister
    {
        void RegisterEventHandler<T>(short aggregateTypeId, short messageTypeId, Action<T> eventHandler);
        void Dispatch(IEnumerable<IEvent> events);
    }
}
