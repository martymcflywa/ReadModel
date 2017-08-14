using Newtonsoft.Json;
using Repository.Data;
using System;
using System.Collections.Generic;

namespace TopCustomer.Event
{
    public class EventStream
    {
        readonly string _connectionString;

        public EventStream(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<IEvent> Collect(string query)
        {
            var messageHubSelector = new EventElementSelector();
            var sqlSource = new SqlSource(_connectionString, messageHubSelector);
            foreach(EventEntry entry in SourceReader.Read(sqlSource, query).Take(200)) // TODO: work out how to segment collection
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
