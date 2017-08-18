using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class CustomersByMonth
    {
        // K=Customer.CustomerId
        public Dictionary<Guid, decimal> Customers { get; }

        public CustomersByMonth()
        {
            Customers = new Dictionary<Guid, decimal>();
        }

        public void Add(IRepaymentEvent payment)
        {
            var customerId = payment.CustomerId;
            if(!Customers.ContainsKey(customerId))
            {
                Customers.Add(customerId, 0);
            }
            Customers[customerId] += payment.Amount;
        }

        public Guid GetHighestPayingCustomer()
        {
            var highestPayingCustomer = default(Guid);
            var highestPaidAmount = default(decimal);
            foreach(var currentCustomer in Customers)
            {
                if(highestPayingCustomer == default(Guid))
                {
                    highestPayingCustomer = currentCustomer.Key;
                    highestPaidAmount = currentCustomer.Value;
                }
                if(currentCustomer.Value > highestPaidAmount)
                {
                    highestPayingCustomer = currentCustomer.Key;
                    highestPaidAmount = currentCustomer.Value;
                }
            }
            return highestPayingCustomer;
        }
    }
}
