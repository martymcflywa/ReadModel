using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public interface IEventRegister
    {
        void RegisterEventHandler<T>(short aggregateTypeId, short messageTypeId, Action<T> eventHandler);
    }
}
