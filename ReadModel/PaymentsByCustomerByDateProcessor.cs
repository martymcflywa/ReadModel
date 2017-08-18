using ReadModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{

    /*
     
         var eventStoreReader = new ESReader("...");
         var processor = new TimeToCashProcessor();
         processor.Register(eventStoreReader);
         
        eventStoreReader.Process();

         */
    //public class SqlThing : IEventRegister
    //{
    //    private readonly Dictionary<EventKey, Action<object>> _dispatchMap = new Dictionary<EventKey, Action<object>>();

    //    public void RegisterEventHandler<T>(short aggregateTypeId, short messageTypeId, Action<T> eventHandler)
    //    {
    //        _dispatchMap[new EventKey(aggregateTypeId, messageTypeId)] = e => eventHandler((T)e);
    //    }

    //    public void Build(IEnumerable<IEvent> events)
    //    {
    //        foreach (var e in events)
    //        {
    //            Dispatch(e);
    //        }
    //    }

    //    private void Dispatch(IEvent e)
    //    {
    //        var handler = _dispatchMap[e.Key];
    //        handler(e);
    //    }
    //}

    public class PaymentsByCustomerByDateProcessor 
    {
        private readonly Dictionary<Guid, Customer> _customers;
        private readonly Dictionary<DateTime, PaymentsByMonth> _paymentsByMonthModel;

        public PaymentsByCustomerByDateProcessor()
        {
            _customers = new Dictionary<Guid, Customer>();
            _paymentsByMonthModel = new Dictionary<DateTime, PaymentsByMonth>();

            /*
             var myThing = new MyBuilder()
             .WithCustomerName("Lee")
                .WithLength(1)
                .WithFoo(bar)
                .Build();
             
             */
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

        

        private void HandleCustomerEvent(CustomerCreatedEvent cce)
        {
            var customerId = cce.GetCustomerId();
            if (!_customers.ContainsKey(customerId))
            {
                _customers.Add(customerId, new Customer(cce.FirstName, cce.Surname));
            }
        }

        private void HandleRepaymentEvent(IRepaymentEvent rpe)
        {
            var year = new DateTime(rpe.GetTransactionDate().Year, 1, 1);
            if (!_paymentsByMonthModel.ContainsKey(year))
            {
                _paymentsByMonthModel.Add(year, new PaymentsByMonth());
            }
            _paymentsByMonthModel[year].Add(rpe);
        }

        public Dictionary<DateTime, Guid> GetHighestPayingCustomerFor(DateTime targetYear)
        {
            // reset year to first day/month incase targetYear is a real DateTime.
            var year = new DateTime(targetYear.Year, 1, 1);
            if(_paymentsByMonthModel.ContainsKey(year))
            {
                return _paymentsByMonthModel[year].GetHighestPayingCustomer();
            }
            throw new ArgumentOutOfRangeException("No payments made for " + year.Year);
        }
    }
}
