using DataAccess.Data;
using DataAccess.Event;
using System.Collections.Generic;
using Xunit;

namespace DataAccessTest.Event
{
    public class EventReaderTest
    {
        [Fact]
        public void Northwind_Read()
        {
            var connString = @"Server=tcp:martynwind.database.windows.net,1433;Initial Catalog=Northwind;Persist Security Info=False;User ID=marty;Password=Northwind123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var query =
                "select * from SalesLT.Customer as t0 " +
                "join SalesLT.SalesOrderHeader as t1 " +
                "on t0.CustomerID = t1.CustomerID " +
                "where t0.CustomerID > @sequenceId " +
                "order by TotalDue desc";

            var db = new Northwind(connString);
            var actual = EventReader
                .Read(db, query)
                .Take(100);
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void MessageHub_Read()
        {
            var connString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";

            var query =
                    "select top 100 * " +
                    "from MessageHub.Message " +
                    "join MessageHub.MessageContent on MessageHub.Message.SequenceId = MessageHub.MessageContent.SequenceId " +
                    "where MessageHub.Message.AggregateTypeId = 12 and MessageHub.Message.MessageTypeId = 3 and MessageHub.Message.SequenceId > @sequenceId " +
                    "order by MessageHub.Message.SequenceId ";

            var db = new MessageHub(connString);
            var actual = EventReader
                .Read(db, query)
                .Take(100);
            var list = new List<string>(actual);

            Assert.NotEmpty(actual);
            Assert.Equal(list.Count, 100);
        }
    }
}
