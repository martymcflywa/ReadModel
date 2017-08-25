using System;
using WebAPI.Interfaces;

namespace WebAPI.Models
{
    public class HighestPayingCustomer : IReadModel
    {
        public DateTime YearMonth { get; }
        public Customer Customer { get; }

        public HighestPayingCustomer(
            DateTime yearMonth,
            Guid customerId,
            string firstName,
            string surname,
            decimal amountPaid)
        {
            YearMonth = yearMonth.Date;
            Customer = new Customer(customerId, firstName, surname, amountPaid);
        }
    }
}
