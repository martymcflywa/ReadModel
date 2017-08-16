using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public interface IEvent
    {
        Guid MessageId { get; set; }
        Guid AggregateId { get; set; }
        DateTimeOffset Timestamp { get; set; }

        Guid GetCustomerId();
    }
}