using System;
using System.Collections.Generic;
using System.Text;

namespace EventReader.Event
{
    public interface IEvent
    {
        Guid MessageId { get; set; }
        Guid AggregateId { get; set; }
        DateTimeOffset Timestamp { get; set; }
    }
}