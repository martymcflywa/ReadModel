using System;

namespace ReadModel.Models.CustomerPayment
{
    public class Customer
    {
        public Guid CustomerId { get; }
        public string FirstName { get; }
        public string Surname { get; }
        public decimal AmountPaid { get; set; }

        public Customer(Guid customerId, string firstName, string surname)
        {
            CustomerId = customerId;
            FirstName = firstName;
            Surname = surname;
            AmountPaid = 0;
        }
    }
}
