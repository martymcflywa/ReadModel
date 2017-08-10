using DataAccess.Event;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataAccessTest.Event
{
    public class EventReaderTest
    {
        [Fact]
        public void Read()
        {
            var json = EventReader.Read(
                    "select top 100 * " +
                    "from MessageHub.Message " +
                    "join MessageHub.MessageContent on MessageHub.Message.SequenceId = MessageHub.MessageContent.SequenceId " +
                    "where MessageHub.Message.AggregateTypeId = 12 and MessageHub.Message.MessageTypeId = 3 and MessageHub.Message.SequenceId > @lastSequenceId " +
                    "order by MessageHub.Message.SequenceId ")
                .Take(100);
            var list = new List<string>(json);
            Assert.True(list.Count == 100);
        }
    }
}
