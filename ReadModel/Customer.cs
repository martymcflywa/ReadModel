using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel
{
    public class Customer
    {
        public string FirstName { get; }
        public string Surname { get; }
        public Dictionary<DateTime, MonthYearPayment> PaymentsPerMonthYear { get; }

        public Customer(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
            PaymentsPerMonthYear = new Dictionary<DateTime, MonthYearPayment>();
        }

        public void AddPayment(decimal amountPaid, DateTimeOffset transactionDate)
        {
            var monthYear = new DateTime(transactionDate.Year, transactionDate.Month, 1);
            if(PaymentsPerMonthYear.ContainsKey(monthYear))
            {
                PaymentsPerMonthYear[monthYear].AddPayment(amountPaid);
            }
            else
            {
                PaymentsPerMonthYear.Add(monthYear, new MonthYearPayment(amountPaid));
            }
        }

        public bool HasPaid()
        {
            return PaymentsPerMonthYear.Count > 0;
        }
    }
}
