using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public class BankingPaymentEvent : IRepaymentEvent
    {
        public Guid MessageId { get; set; }
        public Guid AggregateId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Guid CustomerId { get; set; }
        public Guid TransactionId { get; set; }
        public decimal Amount { get; set; }

        public int TransactionType { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public DateTimeOffset BankingDate { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
        public bool TransactionCleared { get; set; }
        public decimal Balance { get; set; }

        public Guid GetCustomerId()
        {
            return CustomerId;
        }

        public DateTimeOffset GetTransactionDate()
        {
            return TransactionDate;
        }
    }
}
