using EventReader;
using ReadModel;
using Xunit;

namespace ReadModelTest
{
    public class ModelBuilderTest
    {
        [Fact]
        public void BuildFromFirstSequenceId()
        {
            var dispatcher = new SqlEventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Process();
            var winners = processor.GetHighestPayingCustomers();
            // need some validation here that model contains expected data
        }

        [Fact]
        public void BuildFromSpecificSequenceId()
        {
            const int startSequenceId = 11926;
            var dispatcher = new SqlEventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Process(startSequenceId);
            var winners = processor.GetHighestPayingCustomers();
        }
    }
}
