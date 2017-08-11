using System;
using System.Collections.Generic;
using System.Text;

namespace TopCustomer.Event
{
    public interface IEvent
    {
        long SequenceId { get; set; }
        Guid MessageId { get; set; }
        DateTimeOffset Timestamp { get; set; }
    }
}