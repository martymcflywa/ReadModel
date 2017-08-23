using System;
using System.Collections.Generic;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class CustomersModel : IModel
    {
        private readonly IPersist _modelStore;
        public string Filename { get; }
        public long CurrentSequenceId { get; private set; }
        public DateTimeOffset ModelCreatedDate { get; }
        public Dictionary<Guid, Customer> Customers { get; }

        public CustomersModel(IPersist modelStore)
        {
            _modelStore = modelStore;
            Filename = "Customers.json";
            CurrentSequenceId = 0;
            ModelCreatedDate = DateTimeOffset.Now;
            Customers = new Dictionary<Guid, Customer>();
        }

        public void AddEvent(IEvent e)
        {
            var customerEvent = (CustomerCreatedEvent) e;
            var customerId = customerEvent.GetCustomerId();
            if (!Customers.ContainsKey(customerId))
            {
                Customers.Add(customerId, new Customer(
                    customerEvent.AggregateId,
                    customerEvent.FirstName,
                    customerEvent.Surname));
            }
            CurrentSequenceId = customerEvent.SequenceId;
            _modelStore.Write(this, Filename);
        }
    }
}
