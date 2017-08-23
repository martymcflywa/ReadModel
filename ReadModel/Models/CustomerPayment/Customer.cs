using System;

namespace ReadModel.Models.CustomerPayment
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
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
