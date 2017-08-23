using System.IO;
using EventReader;
using Persistence;
using ReadModel;

namespace Bootstrap
{
    public class Program
    {
        public static void Main()
        {
            const long writePageSize = 5000;
            var source = new SqlSource();
            var dispatcher = new EventDispatcher();
            var path = Directory.GetCurrentDirectory();
            var processor = new PaymentsByCustomerByDateProcessor(new ModelStore(path, writePageSize));
            processor.Register(dispatcher);
            dispatcher.Dispatch(source.Read());
            var winners = processor.GetHighestPayingCustomers();
        }
    }
}
