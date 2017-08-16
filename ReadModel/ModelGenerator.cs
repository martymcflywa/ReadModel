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

        // TODO: Target = ModelGenerator#Build(IEnumerable<IEvent> events)
        // IDEA: Current approach Build() does join on payment events with customer events on customerId
        // New approach: Do join in SQL. Every payment brings customer created event with it
        // Will only have to deal with single stream
        public Dictionary<DateTime, PaymentsByMonth> Build(IEnumerable<IEvent> events)
        {
            return default(Dictionary<DateTime, PaymentsByMonth>);
        }

        public Dictionary<DateTime, PaymentsByMonth> Build()
        {
            var model = new Dictionary<DateTime, PaymentsByMonth>();
            var customerEvents = Stream.Get(EventType.CustomerCreated).Take(200000);
            var paymentEvents = Stream.Get(EventType.RepaymentTaken).Take(200000);

            // TODO: this probably needs improvement, replace Tuple with another model? Maybe a function that populates the model.
            var customerPayments = paymentEvents.Join(customerEvents, pe => pe.GetCustomerId(), ce => ce.GetCustomerId(), Tuple.Create);

            foreach(var payment in customerPayments)
            {
                var year = new DateTime(((IRepaymentEvent)payment.Item1).GetTransactionDate().Year, 1, 1);
                if(!model.ContainsKey(year))
                {
                    model.Add(year, new PaymentsByMonth());
                }
                model[year].Add((IRepaymentEvent) payment.Item1, (CustomerCreatedEvent)payment.Item2);
            }
            return model;
        }

        public Dictionary<DateTime, Customer> GetHighestPayingCustomerFor(Dictionary<DateTime, PaymentsByMonth> model, DateTime year)
        {
            if(model.ContainsKey(year))
            {
                return model[year].GetHighestPayingCustomer();
            }
            throw new ArgumentOutOfRangeException("No payments made for " + year.Year.ToString());
        }
    }
}
