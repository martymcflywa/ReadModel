using Newtonsoft.Json;
using ReadModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventReader
{
    public class EventStream : IEventStream, IDeserialize
    {
        private readonly IDataSource _source;

        public EventStream()
        {
            _source = new SqlSource();
        }

        public IEnumerable<IEvent> Get()
        {
            return _source.Read().Select(DeserializeEntry);
        }

        public IEnumerable<IEvent> Get(long initSequenceId)
        {
            return _source.Read(initSequenceId).Select(DeserializeEntry);
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
