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
            var actual = new SqlSource()
                .Read(EventType.CustomerCreated)
                .Take(100);

            var list = new List<EventEntry>(actual);

            Assert.NotEmpty(actual);
            Assert.Equal(list.Count, 100);
        }
    }
}
