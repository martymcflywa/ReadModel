using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EventReader;
using ReadModel;
using ReadModel.Events;
using Xunit;

namespace ReadModelTest
{
    public class PaymentsByCustomerByDateProcessorTest
    {
        [Fact]
        public void GetHighestPayingCustomers_UsingTestData()
        {
            var dispatcher = new EventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Dispatch(GetTestData());
            var actual = processor.GetHighestPayingCustomers();

            Assert.Equal(new DateTime(2016, 1, 1), actual.First().Value.YearMonth);
            Assert.Equal("Mike", actual.First().Value.Customer.FirstName);
            Assert.Equal("Diamond", actual.First().Value.Customer.Surname);
            Assert.Equal(300, actual.First().Value.Customer.AmountPaid);

            // TODO: Add CustomerId to Customer
            //Assert.Equal(StringToGuid("Mike Diamond"), winners.First().Value.Customer.CustomerId);
        }

        [Fact]
        public void GetHighestPayingCustomers_UsingSqlSource()
        {
            const int start = 11926;
            var source = new SqlSource();
            var dispatcher = new EventDispatcher();
            var processor = new PaymentsByCustomerByDateProcessor();
            processor.Register(dispatcher);
            dispatcher.Dispatch(source.Read(start));
            var winners = processor.GetHighestPayingCustomers();
        }

        private static IEnumerable<IEvent> GetTestData()
        {
            return BuildCustomers().Concat(BuildPayments());
        }

        private static IEnumerable<IEvent> BuildCustomers()
        {
            return new List<IEvent>
            {
                new CustomerCreatedEvent
                {
                    SequenceId = 0,
                    Key = new EventKey(11, 16),
                    AggregateId = StringToGuid("Mike Diamond"),
                    FirstName = "Mike",
                    Surname = "Diamond",
                    Timestamp = new DateTimeOffset(new DateTime(2016, 1, 1), TimeSpan.Zero)
                },
                new CustomerCreatedEvent
                {
                    SequenceId = 1,
                    Key = new EventKey(11, 1),
                    AggregateId = StringToGuid("Adam Yauch"),
                    FirstName = "Adam",
                    Surname = "Yauch",
                    Timestamp = new DateTimeOffset(new DateTime(2016, 1, 1), TimeSpan.Zero)
                },
                new CustomerCreatedEvent
                {
                    SequenceId = 2,
                    Key = new EventKey(11, 1),
                    AggregateId = StringToGuid("Adam Horovitz"),
                    FirstName = "Adam",
                    Surname = "Horovitz",
                    Timestamp = new DateTimeOffset(new DateTime(2016, 1, 1), TimeSpan.Zero)
                }
            };
        }

        private static IEnumerable<IEvent> BuildPayments()
        {
            return new List<IEvent>
            {
                new BankingPaymentEvent
                {
                    SequenceId = 3,
                    Key = new EventKey(12, 92),
                    CustomerId = StringToGuid("Mike Diamond"),
                    TransactionDate = new DateTimeOffset(new DateTime(2016, 1, 2), TimeSpan.Zero),
                    Amount = 100
                },
                new BankingPaymentEvent
                {
                    SequenceId = 4,
                    Key = new EventKey(12, 83),
                    CustomerId = StringToGuid("Mike Diamond"),
                    TransactionDate = new DateTimeOffset(new DateTime(2016, 1, 10), TimeSpan.Zero),
                    Amount = 100
                },
                new BankingPaymentEvent
                {
                    SequenceId = 6,
                    Key = new EventKey(12, 85),
                    CustomerId = StringToGuid("Mike Diamond"),
                    TransactionDate = new DateTimeOffset(new DateTime(2016, 1, 10), TimeSpan.Zero),
                    Amount = 100
                },
                new DirectDebitRepaymentEvent
                {
                    SequenceId = 7,
                    Key = new EventKey(12, 89),
                    CustomerId = StringToGuid("Adam Yauch"),
                    ClearingDate = new DateTimeOffset(new DateTime(2016, 1, 12), TimeSpan.Zero),
                    Amount = 200
                },
                new DirectDebitRepaymentEvent
                {
                    SequenceId = 8,
                    Key = new EventKey(12, 89),
                    CustomerId = StringToGuid("Adam Yauch"),
                    ClearingDate = new DateTimeOffset(new DateTime(2016, 1, 13), TimeSpan.Zero),
                    Amount = 50
                }
            };
        }

        private static Guid StringToGuid(string value)
        {
            var hasher = MD5.Create();
            var data = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            return new Guid(data);
        }
    }
}
