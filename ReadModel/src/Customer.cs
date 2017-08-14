using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel
{
    class Customer
    {
        string FirstName;
        string Surname;
        Dictionary<DateTime, MonthYearPayment> PaymentsPerMonthYear;

        public Customer(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
            PaymentsPerMonthYear = new Dictionary<DateTime, MonthYearPayment>();
        }

        public void AddPayment(decimal amountPaid, DateTimeOffset date)
        {
            var monthYear = new DateTime(date.Year, date.Month, 1);
            if(PaymentsPerMonthYear.ContainsKey(monthYear))
            {
                PaymentsPerMonthYear[monthYear].AddPayment(amountPaid);
            }
            else
            {
                PaymentsPerMonthYear.Add(monthYear, new MonthYearPayment(date, amountPaid));
            }
        }
    }
}
