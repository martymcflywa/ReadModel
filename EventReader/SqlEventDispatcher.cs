using Newtonsoft.Json;
using ReadModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventReader
{
    public class SqlEventDispatcher : IEventRegister
    {
        private readonly IDataSource _source;
        private readonly Dictionary<EventKey, Action<object>> _eventRegister;

        public SqlEventDispatcher()
        {
            _source = new SqlSource();
            _eventRegister = new Dictionary<EventKey, Action<object>>();
        }

        public void RegisterEventHandler<T>(short aggregateTypeId, short messageTypeId, Action<T> eventHandler)
        {
            _eventRegister[new EventKey(aggregateTypeId, messageTypeId)] = e => eventHandler((T) e);
        }

        public void Process()
        {
            Dispatch(_source.Read().Select(DeserializeEntry));
        }

        public void Process(long startSequenceId)
        {
            Dispatch(_source.Read(startSequenceId).Select(DeserializeEntry));
        }

        public void Process(long startSequenceId, long endSequenceId)
        {
            Dispatch(_source.Read(startSequenceId).Select(DeserializeEntry).TakeWhile(e => e.SequenceId < endSequenceId));
        }

        private void Dispatch(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                var handler = _eventRegister[e.Key];
                handler(e);
            }
        }

        public IEvent DeserializeEntry(EventEntry entry)
        {
            // customer created
            if (entry.AggregateTypeId == 11 && (entry.MessageTypeId == 1 || entry.MessageTypeId == 16))
            {
                var customer = JsonConvert.DeserializeObject<CustomerCreatedEvent>(entry.Payload);
                return AddEventKey(customer, entry);
            }
            // loan repayment
            if (entry.AggregateTypeId == 12)
            {
                switch (entry.MessageTypeId)
                {
                    case 83:
                    case 84:
                    case 85:
                    case 87:
                    case 92:
                        var bankPayment = JsonConvert.DeserializeObject<BankingPaymentEvent>(entry.Payload);
                        return AddEventKey(bankPayment, entry);
                    case 89:
                        var directDebitPayment = JsonConvert.DeserializeObject<DirectDebitRepaymentEvent>(entry.Payload);
                        return AddEventKey(directDebitPayment, entry);
                    default:
                        throw new NotImplementedException("This type of event is not implemented.");
                }
            }
            throw new NotImplementedException("This type of event is not implemented.");
        }

        private static IEvent AddEventKey(IEvent e, EventEntry entry)
        {
            e.SequenceId = entry.SequenceId;
            e.Key = new EventKey(entry.AggregateTypeId, entry.MessageTypeId);
            return e;
        }
    }
}
