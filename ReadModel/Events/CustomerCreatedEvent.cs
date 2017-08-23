using System;

namespace ReadModel.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public long SequenceId { get; set; }
        public EventKey Key { get; set; }
        public Guid AggregateId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public string FriendlyId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }

        public Guid OriginatingLoanApplicationId { get; set; }

        public Guid GetCustomerId()
        {
            return AggregateId;
        }
    }
}
