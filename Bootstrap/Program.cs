using EventReader;
using ReadModel;

namespace Bootstrap
{
    public class Program
    {
        public static void Main()
        {
            var generator = new ModelGenerator(new EventStream());
            var model = generator.Build();
            // figure out who the highest paying customer is per month
        }
    }
}
