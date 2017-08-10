using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataAccessTest
{
    public class RepositoryTest
    {
        [Fact]
        public void ExecuteReader()
        {
            var json = Repository.GetEvents(
                    "select top 100 MessageHub.Message.SequenceId, Content " +
                    "from MessageHub.Message " +
                    "join MessageHub.MessageContent " +
                    "on MessageHub.Message.SequenceId = MessageHub.MessageContent.SequenceId " +
                    "where AggregateTypeId = 12 and MessageTypeId = 3 ")
                .Take(100);

            var list = new List<string>(json);
            Assert.True(list.Count == 100);
        }
    }
}
