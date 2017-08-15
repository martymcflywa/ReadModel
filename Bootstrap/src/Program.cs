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
            var generator = new ModelGenerator(new EventStream());
            var customerModel = generator.GetCustomerModel();

            foreach(var item in customerModel)
            {
                Console.WriteLine(item.Value.FirstName + " " + item.Value.Surname);
            }
        }
    }
}
