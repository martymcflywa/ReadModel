using EventReader;
using ReadModel;
using System;
using System.Linq;
using Xunit;

namespace ReadModelTest
{
    public class ModelBuilderTest
    {
        [Fact]
        public void BuildFromFirstSequenceId()
        {
            const int limit = 20000;
            var stream = new EventStream();
            var builder = new ModelBuilder();
            builder.Build(stream.Get().TakeWhile(e => e.SequenceId < limit));
            var winners = builder.GetHighestPayingCustomerFor(new DateTime(2016, 1, 1));
            // need some validation here that model contains expected data
        }

        [Fact]
        public void BuildFromSpecificSequenceId()
        {
            const int startSequenceId = 11926;
            const int limit = 100000;
            var stream = new EventStream();
            var builder = new ModelBuilder();
            builder.Build(stream.Get(startSequenceId).TakeWhile(e => e.SequenceId < limit));
        }
    }
}
