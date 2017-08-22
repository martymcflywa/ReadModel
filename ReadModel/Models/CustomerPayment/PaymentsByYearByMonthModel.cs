using System;
using System.Collections.Generic;
using System.Text;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class PaymentsByYearByMonthModel
    {
        public long CurrentSequenceId { get; private set; }
        public DateTimeOffset ModelCreatedDate { get; }
        public Dictionary<DateTime, PaymentsByMonth> Years { get; }
        private readonly Dictionary<Guid, Customer> _customers;

        public PaymentsByYearByMonthModel()
        {
            CurrentSequenceId = 0;
            ModelCreatedDate = DateTimeOffset.Now;
            Years = new Dictionary<DateTime, PaymentsByMonth>();
            _customers = new Dictionary<Guid, Customer>();
        }

        /// <summary>
        /// Add created or imported Customer to customer collection. Use CustomerId to retrieve customer details.
        /// </summary>
        /// <param name="customerCreatedEvent">The customer created/imported event.</param>
        public void AddCustomerEvent(CustomerCreatedEvent customerCreatedEvent)
        {
            var customerId = customerCreatedEvent.GetCustomerId();
            if (!_customers.ContainsKey(customerId))
            {
                _customers.Add(customerId, new Customer(
                    customerCreatedEvent.AggregateId, 
                    customerCreatedEvent.FirstName, 
                    customerCreatedEvent.Surname));
            }
            CurrentSequenceId = customerCreatedEvent.SequenceId;
        }

        /// <summary>
        /// Add payment to model that represents highest paying customer per month, per year.
        /// </summary>
        /// <param name="repaymentEvent"></param>
        public void AddRepaymentEvent(IRepaymentEvent repaymentEvent)
        {
            var year = new DateTime(repaymentEvent.GetTransactionDate().Year, 1, 1);
            if (!Years.ContainsKey(year))
            {
                Years.Add(year, new PaymentsByMonth());
            }
            Years[year].Add(repaymentEvent);
            CurrentSequenceId = repaymentEvent.SequenceId;
        }

        public Dictionary<DateTime, MonthlyResult> GetHighestPayingCustomers()
        {
            var results = new Dictionary<DateTime, MonthlyResult>();
            foreach (var year in Years)
            {
                var highestPayingCustomersPerYear = year.Value.GetHighestPayingCustomer();
                foreach (var month in highestPayingCustomersPerYear)
                {
                    var yearMonth = new DateTime(year.Key.Year, month.Key.Month, DateTime.DaysInMonth(year.Key.Year, month.Key.Month));
                    var customer = _customers[month.Value.Item1];
                    var amountPaid = month.Value.Item2;
                    results.Add(yearMonth, new MonthlyResult(yearMonth, customer, amountPaid));
                }
            }
            return results;
        }
    }
}
