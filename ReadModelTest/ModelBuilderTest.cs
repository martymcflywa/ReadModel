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
            var source = new SqlSource();
            var dispatcher = new EventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Dispatch(source.Read());
            var winners = processor.GetHighestPayingCustomers();
            // need some validation here that model contains expected data
        }

        [Fact]
        public void BuildFromSpecificSequenceId()
        {
            const int start = 11926;
            var source = new SqlSource();
            var dispatcher = new EventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Dispatch(source.Read(start));
            var winners = processor.GetHighestPayingCustomers();
        }

        public void NewKickOff()
        {
            var source = new SqlSource();
            var dispatcher = new EventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Dispatch(source.Read());
            var winners = processor.GetHighestPayingCustomers();
        }
    }
}
