using EventReader.Event;
using EventReader.Read;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventReaderTest
{
    public class EventStreamTest
    {
        [Fact]
        public void GetCustomerCreated()
        {
            var connString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
            var selector = new EventElementSelector();

            var query =
                "select top 100 * " +
                "from MessageHub.Message as t0 " +
                "join MessageHub.MessageContent as t1 " +
                "on t0.SequenceId = t1.SequenceId " +
                "where t0.MessageTypeId = 1 and t0.AggregateTypeId = 11 " +
                "and t0.SequenceId > @sequenceId ";

            var eventStream = new EventStream(connString, selector);
            var actual = eventStream.Get(query).Take(100);
            Assert.NotEmpty(actual);

            var list = new List<IEvent>(actual);
            Assert.Equal(100, list.Count);
        }
    }
}
