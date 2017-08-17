using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public class EventKey
    {
        public short AggregateTypeId { get; }
        public short MessageTypeId { get; }

        public EventKey(short aggregateTypeId, short messageTypeId)
        {
            AggregateTypeId = aggregateTypeId;
            MessageTypeId = messageTypeId;
        }
    }
}
