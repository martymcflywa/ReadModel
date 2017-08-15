using Newtonsoft.Json;
using System;

namespace EventReader.Event
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid MessageId { get; set; }
        public Guid AggregateId { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public string FriendlyId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }

        public Guid OriginatingLoanApplicationId { get; set; }

        [JsonIgnore]
        public Guid CustomerId { get { return AggregateId; } }
    }
}
