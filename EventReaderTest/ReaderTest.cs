using EventReader;
using ReadModel.Events;
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
                .Read()
                .TakeWhile(e => e.SequenceId < 100);

            var list = new List<EventEntry>(actual);

            Assert.NotEmpty(actual);
            Assert.Equal(list.Count, 100);
        }
    }
}
