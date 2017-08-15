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
            if(entry.AggregateTypeId == 11 && entry.MessageTypeId == 1)
            {
                return JsonConvert.DeserializeObject<CustomerCreated>(entry.Message);
            }
            // loan repayment
            else if(entry.AggregateTypeId == 12 && (
                entry.MessageTypeId == 83 ||
                entry.MessageTypeId == 84 ||
                entry.MessageTypeId == 85 ||
                entry.MessageTypeId == 87 ||
                entry.MessageTypeId == 89 ||
                entry.MessageTypeId == 92))
            {
                return JsonConvert.DeserializeObject<RepaymentTaken>(entry.Message);
            }
            else
            {
                throw new ArgumentException("This type of message is not implemented.");
            }
        }
    }
}
