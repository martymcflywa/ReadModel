using Repository.Data;
using System.Collections.Generic;
using Xunit;

namespace RepositoryTest.Data
{
    public class SourceReaderTest
    {
        [Fact]
        public void MessageHub_Read()
        {
            var connString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";

            var query =
                    "select top 100 * " +
                    "from MessageHub.Message " +
                    "join MessageHub.MessageContent on MessageHub.Message.SequenceId = MessageHub.MessageContent.SequenceId " +
                    "where MessageHub.Message.AggregateTypeId = 12 and MessageHub.Message.MessageTypeId = 3 and MessageHub.Message.SequenceId > @sequenceId " +
                    "order by MessageHub.Message.SequenceId ";

            var selector = new EventElementSelector();
            var db = new SqlSource(connString, selector);
            var actual = SourceReader
                .Read(db, query)
                .Take(100);
            var list = new List<EventEntry>(actual);

            Assert.NotEmpty(actual);
            Assert.Equal(list.Count, 100);
        }
    }
}
