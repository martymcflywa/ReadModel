using ReadModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{
    public class PaymentsByCustomerByDateProcessor 
    {
        private readonly Dictionary<Guid, Customer> _customers;
        private readonly Dictionary<DateTime, PaymentsByMonth> _paymentsByMonthModel;

        public PaymentsByCustomerByDateProcessor()
        {
            _customers = new Dictionary<Guid, Customer>();
            _paymentsByMonthModel = new Dictionary<DateTime, PaymentsByMonth>();
        }

        public void Register(IEventRegister register)
        {
            const short customerAggregateType = 11;
            const short repaymentAggregateType = 12;
            register.RegisterEventHandler<CustomerCreatedEvent>(customerAggregateType, 1, HandleCustomerEvent);
            register.RegisterEventHandler<CustomerCreatedEvent>(customerAggregateType, 16, HandleCustomerEvent);
            register.RegisterEventHandler<IRepaymentEvent>(repaymentAggregateType, 83, HandleRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(repaymentAggregateType, 84, HandleRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(repaymentAggregateType, 85, HandleRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(repaymentAggregateType, 87, HandleRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(repaymentAggregateType, 89, HandleRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(repaymentAggregateType, 92, HandleRepaymentEvent);
        }

        /// <summary>
        /// Add created or imported Customer to customer collection. Use CustomerId to retrieve customer details.
        /// </summary>
        /// <param name="customerCreatedEvent">The customer created/imported event.</param>
        private void HandleCustomerEvent(CustomerCreatedEvent customerCreatedEvent)
        {
            var customerId = customerCreatedEvent.GetCustomerId();
            if (!_customers.ContainsKey(customerId))
            {
                _customers.Add(customerId, new Customer(customerCreatedEvent.FirstName, customerCreatedEvent.Surname));
            }
        }

        /// <summary>
        /// Add payment to model that represents highest paying customer per month, per year.
        /// </summary>
        /// <param name="repaymentEvent"></param>
        private void HandleRepaymentEvent(IRepaymentEvent repaymentEvent)
        {
            var year = new DateTime(repaymentEvent.GetTransactionDate().Year, 1, 1);
            if (!_paymentsByMonthModel.ContainsKey(year))
            {
                _paymentsByMonthModel.Add(year, new PaymentsByMonth());
            }
            _paymentsByMonthModel[year].Add(repaymentEvent);
        }

        // TODO: change return type to Dictionary<Year, MonthlyResult>
        // TODO: MonthlyResults members = Month, CustomerId, AmountPaid
        // TODO: Customer/pay models should contain SequenceId
        public Dictionary<DateTime, MonthlyResult> GetHighestPayingCustomers()
        {
            //return _paymentsByMonthModel.ToDictionary(year => year.Key, year => year.Value.GetHighestPayingCustomer());
            var results = new Dictionary<DateTime, MonthlyResult>();
            foreach (var year in _paymentsByMonthModel)
            {
                var highestPayingCustomersThisYear = year.Value.GetHighestPayingCustomer();
                foreach (var month in highestPayingCustomersThisYear)
                {
                    var yearMonth = new DateTime(year.Key.Year, month.Key.Month, 1);
                    var customer = _customers[month.Value.Item1];
                    var amountPaid = month.Value.Item2;
                    results.Add(yearMonth, new MonthlyResult(yearMonth, customer, amountPaid));
                }
            }
            return results;
        }
    }
}
