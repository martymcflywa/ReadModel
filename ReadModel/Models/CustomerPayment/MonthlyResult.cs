using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Models.CustomerPayment
{
    public class MonthlyResult
    {
        public DateTime YearMonth { get; }
        public Customer Customer { get; }

        public MonthlyResult(DateTime yearMonth, Customer customer, decimal amountPaid)
        {
            YearMonth = yearMonth;
            Customer = customer;
            customer.AddPayment(amountPaid);
        }
    }
}
