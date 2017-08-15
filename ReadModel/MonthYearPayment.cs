using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel
{
    public class MonthYearPayment
    {
        DateTimeOffset Date { get; }
        decimal AmountPaid { get; set; }

        public MonthYearPayment(DateTimeOffset date, decimal amountPaid)
        {
            Date = date;
            AmountPaid = amountPaid;
        }

        public void AddPayment(decimal amountPaid)
        {
            AmountPaid += amountPaid;
        }
    }
}
