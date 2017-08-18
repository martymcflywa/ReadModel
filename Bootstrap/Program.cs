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
            const int startSequenceId = 11926;
            const int limit = 100000;
            var dispatcher = new SqlEventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Process(startSequenceId);
            var winners = processor.GetHighestPayingCustomers();
        }
    }
}
