using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public class DirectDebitRepaymentEvent : IRepaymentEvent
    {
        public Guid MessageId { get; set; }
        public Guid AggregateId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Guid CustomerId { get; set; }
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }

        public DateTimeOffset ClearingDate { get; set; }

        public DateTimeOffset GetTransactionDate()
        {
            return ClearingDate;
        }
    }
}
