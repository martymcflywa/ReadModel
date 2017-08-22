using ReadModel.Events;
using System;
using System.Collections.Generic;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{
    public class PaymentsByCustomerByDateProcessor 
    {
        private IPersist Persist { get; }
        public CustomersModel Customers { get; private set; }
        public PaymentsByYearByMonthModel Payments { get; private set; }

        private const string CustomersFilename = "Customers.json";
        private const string PaymentsFilename = "PaymentsByYearByMonth.json";

        public PaymentsByCustomerByDateProcessor(IPersist persist)
        {
            Persist = persist;
            Customers = new CustomersModel();
            Payments = new PaymentsByYearByMonthModel();
        }

        public void Register(IEventRegister register)
        {
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 1, Customers.AddEvent);
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 16, Customers.AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 83, Payments.AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 84, Payments.AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 85, Payments.AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 87, Payments.AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 89, Payments.AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 92, Payments.AddEvent);
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

        // TODO: Make this private, called every x sequenceIds
        public void WriteModelsToFile()
        {
            Persist.Write(Customers, CustomersFilename);
            Persist.Write(Payments, PaymentsFilename);
        }

        // TODO: Test this. Read() returns default(T) when file doesn't exist.
        private void ReadModelsFromFile()
        {
            Customers = Persist.Read<CustomersModel>(CustomersFilename);
            Payments = Persist.Read<PaymentsByYearByMonthModel>(PaymentsFilename);
        }
    }
}
