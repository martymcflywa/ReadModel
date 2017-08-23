using ReadModel.Events;
using System;
using System.Collections.Generic;
using ReadModel.Models;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{
    public class PaymentsByCustomerByDateProcessor
    {
        private readonly IPersist _modelStore;
        public CustomersModel Customers { get; private set; }
        private const string CustomersFilename = "Customers.json";
        public PaymentsByYearByMonthModel Payments { get; private set; }
        private const string PaymentsFilename = "Payments.json";

        public PaymentsByCustomerByDateProcessor(IPersist modelStore)
        {
            _modelStore = modelStore;
        }

        public long Resume()
        {
            Customers = _modelStore.IsFileExists(CustomersFilename) ? _modelStore.Read<CustomersModel>(CustomersFilename) : new CustomersModel(CustomersFilename);
            Payments = _modelStore.IsFileExists(PaymentsFilename) ? _modelStore.Read<PaymentsByYearByMonthModel>(PaymentsFilename) : new PaymentsByYearByMonthModel(PaymentsFilename);
            return Math.Max(Customers.CurrentSequenceId, Payments.CurrentSequenceId);
        }

        public void Register(IEventRegister register)
        {
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 1, e => { Customers.AddEvent(e); _modelStore.Write(Customers); });
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 16, e => { Customers.AddEvent(e); _modelStore.Write(Customers); });
            register.RegisterEventHandler<IRepaymentEvent>(12, 83, e => { Payments.AddEvent(e); _modelStore.Write(Payments); });
            register.RegisterEventHandler<IRepaymentEvent>(12, 84, e => { Payments.AddEvent(e); _modelStore.Write(Payments); });
            register.RegisterEventHandler<IRepaymentEvent>(12, 85, e => { Payments.AddEvent(e); _modelStore.Write(Payments); });
            register.RegisterEventHandler<IRepaymentEvent>(12, 87, e => { Payments.AddEvent(e); _modelStore.Write(Payments); });
            register.RegisterEventHandler<IRepaymentEvent>(12, 89, e => { Payments.AddEvent(e); _modelStore.Write(Payments); });
            register.RegisterEventHandler<IRepaymentEvent>(12, 92, e => { Payments.AddEvent(e); _modelStore.Write(Payments); });
        }

        public Dictionary<DateTime, MonthlyResult> GetHighestPayingCustomers()
        {
            var results = new Dictionary<DateTime, MonthlyResult>();
            foreach (var year in Payments.Years)
            {
                var highestPayingCustomersPerYear = year.Value.GetHighestPayingCustomer();
                foreach (var month in highestPayingCustomersPerYear)
                {
                    var yearMonth = new DateTime(
                        year.Key.Year, 
                        month.Key.Month, 
                        DateTime.DaysInMonth(year.Key.Year, month.Key.Month));
                    var customer = Customers.Customers[month.Value.Item1];
                    var amountPaid = month.Value.Item2;
                    results.Add(yearMonth, new MonthlyResult(yearMonth, customer, amountPaid));
                }
            }
            return results;
        }
    }
}
