using System.Linq;
using EventReader;
using Xunit;

namespace EventReaderTest
{
    public class SqlSourceTest
    {
        [Fact]
        public void DefaultConstructorConnectionString()
        {
            var sqlSource = new SqlSource();
            var actual = sqlSource
                .ExecuteQuery(-1)
                .Take(100);

            var eventEntries = actual as EventEntry[] ?? actual.ToArray();
            Assert.NotEmpty(eventEntries);
            Assert.Equal(100, eventEntries.Length);
            Assert.Equal(11927, eventEntries.First().SequenceId);
        }

        [Fact]
        public void ConnectionStringConstructor()
        {
            var sqlSource = new SqlSource(@"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;");
            var actual = sqlSource
                .ExecuteQuery(-1)
                .Take(100);

            var eventEntries = actual as EventEntry[] ?? actual.ToArray();
            Assert.NotEmpty(eventEntries);
            Assert.Equal(100, eventEntries.Length);
            Assert.Equal(11927, eventEntries.First().SequenceId);
        }
    }
}
