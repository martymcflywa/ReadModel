using ReadModel.Events;
using ReadModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadModel
{
    public class ModelGenerator
    {
        IEventStream Stream;

        public ModelGenerator(IEventStream stream)
        {
            Stream = stream;
        }

        public Dictionary<Guid, Customer> GetCustomerModel()
        {
            var customerModel = new Dictionary<Guid, Customer>();
            var customerEvents = Stream.Get(EventType.CustomerCreated).Take(10000);
            var paymentEvents = Stream.Get(EventType.RepaymentTaken).Take(10000);

            foreach (CustomerCreatedEvent cce in customerEvents)
            {
                var customerId = cce.CustomerId;
                if (!customerModel.ContainsKey(customerId))
                {
                    customerModel.Add(customerId, new Customer(cce.FirstName, cce.Surname));
                }
            }

            foreach (IRepaymentEvent re in paymentEvents)
            {
                var customerId = re.CustomerId;
                if (customerModel.ContainsKey(customerId))
                {
                    customerModel[customerId].AddPayment(re.Amount, re.GetTransactionDate());
                }
            }

            return customerModel;
        }
    }
}
