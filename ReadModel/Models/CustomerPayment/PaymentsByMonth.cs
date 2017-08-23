using System;
using System.Collections.Generic;
using System.Linq;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class PaymentsByMonth
    {
        // K=DateTime.Months
        public Dictionary<DateTime, CustomersByMonth> Months { get; set; }

        public PaymentsByMonth()
        {
            Months = new Dictionary<DateTime, CustomersByMonth>();
        }

        public void Add(IRepaymentEvent payment)
        {
            var month = new DateTime(
                payment.GetTransactionDate().Year, 
                payment.GetTransactionDate().Month, 
                DateTime.DaysInMonth(payment.GetTransactionDate().Year, 
                payment.GetTransactionDate().Month));

            if(!Months.ContainsKey(month))
            {
                Months.Add(month, new CustomersByMonth());
            }
            Months[month].Add(payment);
        }

        public Tuple<Guid, decimal> GetHighestPayingCustomerFor(DateTime month)
        {
            if (Months.ContainsKey(month))
            {
                return Months[month].GetHighestPayingCustomer();
            }
            throw new ArgumentOutOfRangeException("No payments made for " + month.Month);
        }

        public Dictionary<DateTime, Tuple<Guid, decimal>> GetHighestPayingCustomer()
        {
            return Months.ToDictionary(month => month.Key, month => GetHighestPayingCustomerFor(month.Key));
        }
    }
}
