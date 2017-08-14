using EventReader.Event;
using EventReader.Read;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel
{
    public class ModelGenerator
    {
        EventStream Stream;
        public Dictionary<Guid, Customer> Customers { get; }

        const string CUSTOMER_QUERY =
            "select top 100 * " +
            "from MessageHub.Message as t0 " +
            "join MessageHub.MessageContent as t1 " +
            "on t0.SequenceId = t1.SequenceId " +
            "where t0.MessageTypeId = 1 and t0.AggregateTypeId = 11 " +
            "and t0.SequenceId > @sequenceId ";

        const string REPAYMENT_QUERY =
            "select top 100 * " +
            "from MessageHub.Message as t0 " +
            "join MessageHub.MessageContent as t1 " +
            "on t0.sequenceId = t1.sequenceId " +
            "where MessageTypeId in (83, 84, 85, 87, 89, 92) and " +
            "t0.AggregateTypeId = 12 and " +
            "t0.SequenceId > @sequenceId ";

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
            Customers = new Dictionary<Guid, Customer>();
        }

        public void PopulateModel()
        {
            var customerEvents = Stream.Get(CUSTOMER_QUERY).Take(10000);
            var paymentEvents = Stream.Get(REPAYMENT_QUERY).Take(10000);

            foreach (CustomerCreated cc in customerEvents)
            {
                var customerId = cc.CustomerId;
                if (!Customers.ContainsKey(customerId))
                {
                    Customers.Add(customerId, new Customer(cc.FirstName, cc.Surname));
                }
            }

            foreach (RepaymentTaken rt in paymentEvents)
            {
                var customerId = rt.CustomerId;
                if (Customers.ContainsKey(customerId))
                {
                    Customers[customerId].AddPayment(rt.Amount, rt.BankingDate);
                }
            }
        }
    }
}
