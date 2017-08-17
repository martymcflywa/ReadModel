using EventReader;
using ReadModel.Events;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EventReaderTest
{
    public class EventStreamTest
    {
        [Fact]
        public void GetCustomerCreated()
        {
            var eventStream = new EventStream();
            var actual = eventStream.Get(EventType.CustomerCreated).Take(100);
            Assert.NotEmpty(actual);

            var list = new List<IEvent>(actual);
            Assert.Equal(100, list.Count);
        }
    }
}
