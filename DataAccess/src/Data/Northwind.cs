using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Data
{
    public class Northwind : BaseSqlSource
    {

        public Northwind(string connectionString) : base(connectionString)
        {
        }

        protected override EventEntry EntrySelector(SqlDataReader reader)
        {
            return new EventEntry((int)reader[0], (string)reader[3]);
        }
    }
}
