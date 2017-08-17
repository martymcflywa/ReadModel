using System;
using System.Collections.Generic;

namespace ReadModel.Models
{
    public class Customer
    {
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public decimal AmountPaid { get; private set; }

        public Customer(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
            AmountPaid = 0;
        }

        public Customer(string firstName, string surname, decimal amountPaid)
        {
            FirstName = firstName;
            Surname = surname;
            AmountPaid = amountPaid;
        }

        public void AddPayment(decimal amountPaid)
        {
            AmountPaid += amountPaid;
        }
    }
}
