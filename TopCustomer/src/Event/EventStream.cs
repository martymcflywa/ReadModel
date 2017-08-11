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

        public IEnumerable<TSource> Collect<TSource>(string query)
        {
            var messageHub = new MessageHub(_connectionString);
            foreach(string item in SourceReader.Read(messageHub, query).Take(200)) // TODO: work out how to segment collection
            {
                yield return JsonConvert.DeserializeObject<TSource>(item);
            }
        }
    }
}
