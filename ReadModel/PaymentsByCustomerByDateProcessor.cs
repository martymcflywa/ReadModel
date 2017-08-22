using ReadModel.Events;
using System;
using System.Collections.Generic;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{
    public class PaymentsByCustomerByDateProcessor 
    {
        public PaymentsByYearByMonthModel Model { get; }

        public PaymentsByCustomerByDateProcessor()
        {
            Model = new PaymentsByYearByMonthModel();
        }

        public void Register(IEventRegister register)
        {
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 1, Model.AddCustomerEvent);
            register.RegisterEventHandler<CustomerCreatedEvent>(11, 16, Model.AddCustomerEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 83, Model.AddRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 84, Model.AddRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 85, Model.AddRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 87, Model.AddRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 89, Model.AddRepaymentEvent);
            register.RegisterEventHandler<IRepaymentEvent>(12, 92, Model.AddRepaymentEvent);
        }

        public Dictionary<DateTime, MonthlyResult> GetHighestPayingCustomers()
        {
            return Model.GetHighestPayingCustomers();
        }

        public void WriteModelToFile(IPersist writer, string path)
        {
            const string filename = "PaymentsByYearByMonth.json";
            writer.Write(Model, path, filename);
        }
    }
}
