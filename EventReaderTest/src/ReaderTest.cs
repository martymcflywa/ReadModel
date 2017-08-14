using EventReader.Read;
using System.Collections.Generic;
using Xunit;

namespace EventReaderTest
{
    public class ReaderTest
    {
        [Fact]
        public void Read()
        {
            var connString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";

            var query =
                    "select top 100 * " +
                    "from MessageHub.Message " +
                    "join MessageHub.MessageContent on MessageHub.Message.SequenceId = MessageHub.MessageContent.SequenceId " +
                    "where MessageHub.Message.AggregateTypeId = 12 and MessageHub.Message.MessageTypeId = 3 and MessageHub.Message.SequenceId > @sequenceId " +
                    "order by MessageHub.Message.SequenceId ";

            var selector = new EventElementSelector();
            var actual = new SqlSource(connString, selector)
                .Read(query)
                .Take(100);

            var list = new List<EventEntry>(actual);

            Assert.NotEmpty(actual);
            Assert.Equal(list.Count, 100);
        }
    }
}
