using System;
using System.Collections.Generic;
using DataAccess;
using DataAccess.Data;
using Xunit;

namespace DataAccessTest.Data
{
    public class NorthwindTest
    {
        const string CONNECTION_STRING = @"Server=tcp:martynwind.database.windows.net,1433;Initial Catalog=Northwind;Persist Security Info=False;User ID=marty;Password=Northwind123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [Fact]
        public void ExecuteQueryTest()
        {
            var query =
                "select * from SalesLT.Customer as t0 " +
                "join SalesLT.SalesOrderHeader as t1 " +
                "on t0.CustomerID = t1.CustomerID " +
                "order by TotalDue desc";
            
            var db = new Northwind(CONNECTION_STRING);
            var actual = db.ExecuteQuery(query, 0);
            Assert.NotEmpty(actual);
        }
    }
}
