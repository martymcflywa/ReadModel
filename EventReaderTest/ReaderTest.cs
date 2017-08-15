using EventReader.Read;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EventReaderTest
{
    public class ReaderTest
    {
        [Fact]
        public void Read()
        {
            var connString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";

            var selector = new EventElementSelector();
            var actual = new SqlSource(connString, selector)
                .Read(EventType.CustomerCreated)
                .Take(100);

            var list = new List<EventEntry>(actual);

            Assert.NotEmpty(actual);
            Assert.Equal(list.Count, 100);
        }
    }
}
