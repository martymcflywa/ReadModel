using EventReader;
using ReadModel;
using System;
using System.Collections.Generic;
using ReadModel.Models.CustomerPayment;

namespace Bootstrap
{
    public class Program
    {
        public static void Main()
        {
            var generator = new ModelGenerator(new EventStream());
            var model = generator.Build();
            var winners = generator.GetHighestPayingCustomerFor(model, new DateTime(2016, 1, 1));

            foreach(var customer in winners)
            {
                Console.WriteLine(
                    "Highest paying customer for " + 
                    customer.Key.Year + "-" + customer.Key.Month + " " + 
                    customer.Value.FirstName + " " + customer.Value.Surname + " " +
                    "$" + customer.Value.AmountPaid);
            }
        }
    }
}
