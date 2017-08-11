using System;

namespace TopCustomer.Event
{
    public class CustomerCreated : IEvent
    {
        public long SequenceId { get; set; }
        public Guid MessageId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }

        public Guid LoanApplicationId { get; set; }
    }
}
