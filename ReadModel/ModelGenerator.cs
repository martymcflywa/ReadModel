using EventReader.Event;
using EventReader.Read;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadModel
{
    public class ModelGenerator
    {
        EventStream Stream;

        // TODO: might still use CUSTOMER_DETAILS_QUERY below...
        // IDEA: Rather than build a separate stream of CustomerCreated and RepaymentTaken events...
            // Iterate each RepaymentTaken event
                // If Customer doesn't exist in CustomerModel
                    // Call Stream.Get() to retrieve single CustomerCreated event, where PaymentEvent.CustomerId == AggregateId
                    // Create new Customer and add to CustomerModel
                // Add payment to Customer
        const string CUSTOMER_DETAILS_QUERY =
            "select * " +
            "from MessageHub.Message as t0 " +
            "join MessageHub.MessageContent as t1 " +
            "on t0.SequenceId = t1.SequenceId " +
            "where t0.MessageTypeId = 1 and t0.AggregateTypeId = 11 and " +
            "t0.AggregateId = @customerId ";

        public ModelGenerator(EventStream stream)
        {
            Stream = stream;
        }

        public Dictionary<Guid, Customer> GetCustomerModel()
        {
            var customerModel = new Dictionary<Guid, Customer>();
            var customerEvents = Stream.Get(EventType.CustomerCreated).Take(50000);
            var paymentEvents = Stream.Get(EventType.RepaymentTaken).Take(50000);

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
