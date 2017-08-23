using System;
using System.Collections.Generic;
using System.Linq;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class CustomersModel : IModel
    {
        public string Filename { get; }
        public long CurrentSequenceId { get; set; }
        public DateTimeOffset ModelCreatedDate { get; }
        public Dictionary<Guid, Customer> Customers { get; set; }

        public CustomersModel(string filename)
        {
            Filename = filename;
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
        }
    }
}
