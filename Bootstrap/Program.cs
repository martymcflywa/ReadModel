using EventReader;
using ReadModel;

namespace Bootstrap
{
    public class Program
    {
        public static void Main()
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
