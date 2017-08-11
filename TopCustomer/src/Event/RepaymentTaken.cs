using System;
using System.Collections.Generic;
using System.Text;

namespace TopCustomer.Event
{
    public class RepaymentTaken : IEvent
    {
        public Guid MessageId { get; set; }
        public Guid AggregateId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Guid CustomerId { get; set; }
        public Guid TransactionId { get; set; }
        public int TransactionType { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public DateTime BankingDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool TransactionCleared { get; set; }

        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
