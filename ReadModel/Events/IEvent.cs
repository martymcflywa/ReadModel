using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public interface IEvent
    {
        long SequenceId { get; set; }
        EventKey Key { get; set; }
        Guid AggregateId { get; set; }
        DateTimeOffset Timestamp { get; set; }

        Guid GetCustomerId();
    }
}