using System;

namespace WebAPI.Models
{
    public class Customer
    {
        public Guid CustomerId { get; }
        public string FirstName { get; }
        public string Surname { get; }
        public decimal AmountPaid { get; }

        public Customer(Guid customerId, string firstName, string surname, decimal amountPaid)
        {
            CustomerId = customerId;
            FirstName = firstName;
            Surname = surname;
            AmountPaid = amountPaid;
        }
    }
}
