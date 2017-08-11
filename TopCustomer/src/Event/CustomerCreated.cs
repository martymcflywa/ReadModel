using System;

namespace TopCustomer.Event
{
    public class CustomerCreated : IEvent
    {
        public Guid MessageId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public string FriendlyId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }

        public Guid OriginatingLoanApplicationId { get; set; }
    }
}
