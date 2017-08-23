using System;
using System.Collections.Generic;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class PaymentsByCustomerByDateModel : IModel
    {
        public string Filename { get; }
        public long CurrentSequenceId { get; set; }
        public DateTimeOffset ModelCreatedDate { get; }

        public Dictionary<Guid, Customer> Customers { get; set; }
        public Dictionary<DateTime, PaymentsByMonth> Payments { get; set; }


        public PaymentsByCustomerByDateModel(string filename)
        {
            Filename = filename;
            CurrentSequenceId = 0;
            ModelCreatedDate = DateTimeOffset.Now;

            Customers = new Dictionary<Guid, Customer>();
            Payments = new Dictionary<DateTime, PaymentsByMonth>();
        }

        public void AddEvent<T>(T e)
        {
            if (e.GetType() == typeof(CustomerCreatedEvent))
            {
                AddCustomerEvent(e as CustomerCreatedEvent);
            }
            else if (e is IRepaymentEvent)
            {
                AddRepaymentEvent((IRepaymentEvent) e);
            }
            else
            {
                throw new NotImplementedException("Event type not implemented.");
            }
        }

        private void AddCustomerEvent(CustomerCreatedEvent e)
        {
            var customerId = e.GetCustomerId();
            if (!Customers.ContainsKey(customerId))
            {
                Customers.Add(customerId, new Customer(
                    e.AggregateId,
                    e.FirstName,
                    e.Surname));
            }
            CurrentSequenceId = e.SequenceId;
        }

        private void AddRepaymentEvent(IRepaymentEvent e)
        {
            var year = new DateTime(e.GetTransactionDate().Year, 1, 1);
            if (!Payments.ContainsKey(year))
            {
                Payments.Add(year, new PaymentsByMonth());
            }
            Payments[year].Add(e);
            CurrentSequenceId = e.SequenceId;
        }
    }
}
