﻿using System;
using System.Collections.Generic;
using EventReader;
using Persistence;
using ReadModel;
using ReadModel.Models.CustomerPayment;

namespace Bootstrap
{
    public class Program
    {
        private const string ConnectionString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
        private const string Path = @"C:\Users\martin.ponce\Documents\dev\projects\ReadModel\Samples";
        private const int WritePageSize = 70000;

        public static void Main()
        {
            var source = new SqlSource(ConnectionString);
            var modelStore = new ModelStore(Path, WritePageSize);
            var dispatcher = new EventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor(modelStore);

            var resumeFrom = processor.ResumeFrom;
            processor.Register(dispatcher);
            dispatcher.Dispatch(source.Read(resumeFrom));

            PrintWinners(processor.GetHighestPayingCustomers());
        }

        private static void PrintWinners(Dictionary<DateTime, MonthlyResult> winners)
        {
            Console.WriteLine("Highest paying customers for each month: ");

            foreach (var customer in winners)
            {
                var yearMonth = customer.Value.YearMonth;
                var fullName = customer.Value.Customer.FirstName + " " + customer.Value.Customer.Surname;
                var amountpaid = customer.Value.Customer.AmountPaid;
                Console.WriteLine("Month: " + yearMonth + ", Name: " + fullName + ", Amount: $" + amountpaid);
            }
        }
    }
}
