using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public class DirectDebitRepaymentEvent : IRepaymentEvent
    {
        public long SequenceId { get; set; }
        public EventKey Key { get; set; }
        public Guid MessageId { get; set; }
        public Guid AggregateId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Guid CustomerId { get; set; }
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }

        public DateTimeOffset ClearingDate { get; set; }

        public Guid GetCustomerId()
        {
            return CustomerId;
        }

        public DateTimeOffset GetTransactionDate()
        {
            return ClearingDate;
        }
    }
}
