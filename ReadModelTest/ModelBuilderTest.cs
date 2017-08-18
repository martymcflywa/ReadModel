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
            var year = new DateTime(2016, 1, 1);
            const int limit = 20000;
            var dispatcher = new SqlEventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Process(0, limit);
            var winners = processor.GetHighestPayingCustomerFor(year);
            // need some validation here that model contains expected data
        }

        [Fact]
        public void BuildFromSpecificSequenceId()
        {
            var year = new DateTime(2016, 1, 1);
            const int startSequenceId = 11926;
            const int limit = 100000;
            var dispatcher = new SqlEventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Process(startSequenceId, limit);
            var winners = processor.GetHighestPayingCustomerFor(year);
        }
    }
}
