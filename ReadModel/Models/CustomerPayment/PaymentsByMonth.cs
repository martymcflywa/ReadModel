using System;
using System.Collections.Generic;
using System.Linq;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class PaymentsByMonth
    {
        // K=DateTime.Month
        private readonly Dictionary<DateTime, CustomersByMonth> _months;

        public PaymentsByMonth()
        {
            _months = new Dictionary<DateTime, CustomersByMonth>();
        }

        public void Add(IRepaymentEvent payment)
        {
            var month = new DateTime(payment.GetTransactionDate().Year, payment.GetTransactionDate().Month, 1);
            if(!_months.ContainsKey(month))
            {
                _months.Add(month, new CustomersByMonth());
            }
            _months[month].Add(payment);
        }

        public Tuple<Guid, decimal> GetHighestPayingCustomerFor(DateTime month)
        {
            if (_months.ContainsKey(month))
            {
                return _months[month].GetHighestPayingCustomer();
            }
            throw new ArgumentOutOfRangeException("No payments made for " + month.Month);
        }

        public Dictionary<DateTime, Tuple<Guid, decimal>> GetHighestPayingCustomer()
        {
            return _months.ToDictionary(month => month.Key, month => GetHighestPayingCustomerFor(month.Key));
        }
    }
}
