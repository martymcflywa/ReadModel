using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class CustomersByMonth
    {
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

        public Tuple<Guid, decimal> GetHighestPayingCustomer()
        {
            var customerList = Customers.OrderByDescending(cl => cl.Value);
            return Tuple.Create(customerList.First().Key, customerList.First().Value);
        }
    }
}
