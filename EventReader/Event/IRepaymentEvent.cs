using System;
using System.Collections.Generic;
using System.Text;

namespace EventReader.Event
{
    public interface IRepaymentEvent : IEvent
    {
        Guid CustomerId { get; set; }
        Guid TransactionId { get; set; }
        decimal Amount { get; set; }

        DateTimeOffset GetTransactionDate();
    }
}
