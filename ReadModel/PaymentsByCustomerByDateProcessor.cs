using ReadModel.Events;
using System;
using System.Collections.Generic;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{
    public class PaymentsByCustomerByDateProcessor
    {
        private readonly IPersist _modelStore;
        private PaymentsByCustomerByDateModel _model;
        private const string Filename = "PaymentsByCustomerByDate.json";
        public long ResumeFrom { get; }
        
        public PaymentsByCustomerByDateProcessor(IPersist modelStore)
        {
            _modelStore = modelStore;
            ResumeFrom = Resume();
        }

        public long Resume()
        {
            // TODO: put this logic in Read()
            _model = _modelStore.IsFileExists(Filename)
                ? _modelStore.Read<PaymentsByCustomerByDateModel>(Filename)
                : new PaymentsByCustomerByDateModel(Filename);

            return _model.CurrentSequenceId;
        }

        public void Register(IEventRegister register)
        {
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 1, AddEvent);
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 16, AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 83, AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 84, AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 85, AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 87, AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 89, AddEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 92, AddEvent);
        }

        private void AddEvent<T>(T e)
        {
            _model.AddEvent(e);
            _modelStore.Write(_model);
        }

        public Dictionary<DateTime, MonthlyResult> GetHighestPayingCustomers()
        {
            var results = new Dictionary<DateTime, MonthlyResult>();
            foreach (var year in _model.Payments)
            {
                var highestPayingCustomersPerYear = year.Value.GetHighestPayingCustomer();
                foreach (var month in highestPayingCustomersPerYear)
                {
                    var yearMonth = new DateTime(
                        year.Key.Year, 
                        month.Key.Month, 
                        DateTime.DaysInMonth(year.Key.Year, month.Key.Month));
                    var customer = _model.Customers[month.Value.Item1];
                    var amountPaid = month.Value.Item2;
                    results.Add(yearMonth, new MonthlyResult(yearMonth, customer, amountPaid));
                }
            }
            return results;
        }
    }
}
