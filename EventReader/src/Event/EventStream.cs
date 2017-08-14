using EventReader.Read;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EventReader.Event
{
    public class EventStream
    {
        readonly string _connectionString;
        IElementSelector _selector;

        public EventStream(string connectionString, IElementSelector selector)
        {
            _connectionString = connectionString;
            _selector = selector;
        }

        public IEnumerable<IEvent> Get(string query)
        {
            var sqlSource = new SqlSource(_connectionString, _selector);
            foreach(EventEntry entry in Reader.Read(sqlSource, query)) // TODO: work out how to segment collection
            {
                yield return DeserializeEntry(entry);
            }
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
