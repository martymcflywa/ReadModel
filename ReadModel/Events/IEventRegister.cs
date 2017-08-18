using System;
using System.Collections.Generic;

namespace ReadModel.Events
{
    public interface IEventRegister
    {
        void RegisterEventHandler<T>(short aggregateTypeId, short messageTypeId, Action<T> eventHandler);

        // TODO: remove process() methods
        void Process();
        void Process(long startSequenceId);
        void Process(long startSequenceId, long endSequenceId);
    }
}
