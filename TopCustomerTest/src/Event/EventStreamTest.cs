using TopCustomer.Event;
using Xunit;

namespace TopCustomerTest.Event
{
    public class EventStreamTest
    {
        [Fact]
        public void CollectCustomerCreated()
        {
            var connectionString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";

            var query =
                "select top 100 * " +
                "from MessageHub.Message as t0 " +
                "join MessageHub.MessageContent as t1 " +
                "on t0.SequenceId = t1.SequenceId " +
                "where t0.MessageTypeId = 1 and t0.AggregateTypeId = 11 " +
                "and t0.SequenceId > @sequenceId ";

            var eventStream = new EventStream(connectionString);
            var actual = eventStream.Collect<CustomerCreated>(query);
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void CollectRepaymentTaken()
        {
            var connectionString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";

            var query =
                "select top 100 * " +
                "from MessageHub.Message as t0 " +
                "join MessageHub.MessageContent as t1 " +
                "on t0.sequenceId = t1.sequenceId " +
                "where MessageTypeId in (83, 84, 85, 87, 89, 92) and " +
                "t0.AggregateTypeId = 12 and " +
                "t0.SequenceId > @sequenceId ";

            var eventStream = new EventStream(connectionString);
            var actual = eventStream.Collect<RepaymentTaken>(query);
            Assert.NotEmpty(actual);
        }
    }
}
