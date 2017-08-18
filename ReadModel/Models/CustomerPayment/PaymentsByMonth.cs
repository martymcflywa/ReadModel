using System;
using System.Collections.Generic;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class PaymentsByMonth
    {
        // K=DateTime.Month
        public Dictionary<DateTime, CustomersByMonth> Months { get; }

        public PaymentsByMonth()
        {
            Months = new Dictionary<DateTime, CustomersByMonth>();
        }

        public void Add(IRepaymentEvent payment)
        {
            var month = new DateTime(payment.GetTransactionDate().Year, payment.GetTransactionDate().Month, 1);
            if(!Months.ContainsKey(month))
            {
                Months.Add(month, new CustomersByMonth());
            }
            Months[month].Add(payment);
        }

        public Guid GetHighestPayingCustomerFor(DateTime month)
        {
            if (Months.ContainsKey(month))
            {
                return Months[month].GetHighestPayingCustomer();
            }
            throw new ArgumentOutOfRangeException("No payments made for " + month.Month.ToString());
        }

        public Dictionary<DateTime, Guid> GetHighestPayingCustomer()
        {
            var highestPayingForAllMonths = new Dictionary<DateTime, Guid>();
            foreach(KeyValuePair<DateTime, CustomersByMonth> month in Months)
            {
                highestPayingForAllMonths.Add(month.Key, GetHighestPayingCustomerFor(month.Key));
            }
            return highestPayingForAllMonths;
        }
    }
}
