using EventReader.Read;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventReader.Event
{
    public class EventStream
    {
        IDataSource Source;

        public EventStream(IDataSource source)
        {
            Source = source;
        }

        public IEnumerable<IEvent> Get(EventType eventType)
        {
            return Reader.Read(Source, eventType).Select(entry => DeserializeEntry(entry));
        }

        // Might still use this, see comment in ReadModel.ModelGenerator
        public IEnumerable<IEvent> Get(string query, Dictionary<string, string> parameters)
        {
            return Reader.Read(Source, query, parameters).Select(entry => DeserializeEntry(entry));
        }

        IEvent DeserializeEntry(EventEntry entry)
        {
            // customer created
            if(entry.AggregateTypeId == 11 && (entry.MessageTypeId == 1 || entry.MessageTypeId == 16))
            {
                return JsonConvert.DeserializeObject<CustomerCreatedEvent>(entry.Message);
            }
            // loan repayment
            if(entry.AggregateTypeId == 12)
            {
                if (entry.MessageTypeId == 83 ||
                    entry.MessageTypeId == 84 ||
                    entry.MessageTypeId == 85 ||
                    entry.MessageTypeId == 87 ||
                    entry.MessageTypeId == 92)
                {
                    return JsonConvert.DeserializeObject<BankingPaymentEvent>(entry.Message);
                }
                if(entry.MessageTypeId == 89)
                {
                    return JsonConvert.DeserializeObject<DirectDebitRepaymentEvent>(entry.Message);
                }
                throw new NotImplementedException("This type of event is not implemented.");
            }
            throw new NotImplementedException("This type of event is not implemented.");
        }
    }
}
