using EventReader.Event;
using EventReader.Read;
using ReadModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrap
{
    public class Program
    {
        public static void Main()
        {
            var connectionString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
            var selector = new EventElementSelector();
            var source = new SqlSource(connectionString, selector);
            var generator = new ModelGenerator(new EventStream(source));
            var customerModel = generator.GetCustomerModel();

            foreach(var item in customerModel)
            {
                Console.WriteLine(item.Value.FirstName + " " + item.Value.Surname);
            }
        }
    }
}
