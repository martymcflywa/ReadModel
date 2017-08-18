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
            var stream = new EventStream();
            var builder = new ModelBuilder();
            builder.Build(stream.Get());
            var winners = builder.GetHighestPayingCustomerFor(new DateTime(2016, 1, 1));
        }
    }
}
