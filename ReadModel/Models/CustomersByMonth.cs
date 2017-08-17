using ReadModel.Events;
using System;
using System.Collections.Generic;

namespace ReadModel.Models
{
    public class CustomersByMonth
    {
        // K=Customer.CustomerId
        public Dictionary<Guid, Customer> Customers { get; private set; }

        public CustomersByMonth()
        {
            Customers = new Dictionary<Guid, Customer>();
        }

        public void Add(IRepaymentEvent payment, CustomerCreatedEvent customer)
        {
            var customerId = payment.CustomerId;
            if(!Customers.ContainsKey(customerId))
            {
                Customers.Add(customerId, new Customer(customer.FirstName, customer.Surname));
            }
            Customers[customerId].AddPayment(payment.Amount);
        }

        public Customer GetHighestPayingCustomer()
        {
            var highestPayingCustomer = default(Customer);
            foreach(KeyValuePair<Guid, Customer> currentCustomer in Customers)
            {
                if(highestPayingCustomer == default(Customer))
                {
                    highestPayingCustomer = currentCustomer.Value;
                }
                if(currentCustomer.Value.AmountPaid > highestPayingCustomer.AmountPaid)
                {
                    highestPayingCustomer = currentCustomer.Value;
                }
            }
            return highestPayingCustomer;
        }
    }
}
