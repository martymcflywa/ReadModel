using Newtonsoft.Json;
using Repository.Data;
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
            var messageHub = new MessageHub(_connectionString);
            foreach(string item in SourceReader.Read(messageHub, query).Take(200)) // TODO: work out how to segment collection
            {
                //var aggregateType = 11;
                //var messageType = 1;

                if(aggregateType ==1 && messageType == 1)
                {
                    yield return JsonConvert.DeserializeObject<CustomerCreated>(item);
                }

                //throw new Exception("Not implemented");
                //yield return JsonConvert.DeserializeObject<TSource>(item);
            }
        }
    }
}
