using EventReader;
using ReadModel;

namespace Bootstrap
{
    public class Program
    {
        public static void Main()
        {
            var generator = new ModelGenerator(new EventStream());
            var customerModel = generator.GetCustomerModel();
            // figure out who the highest paying customer is per month
        }
    }
}
