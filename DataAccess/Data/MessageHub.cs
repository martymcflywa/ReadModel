using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Data
{
    public class MessageHub : BaseSqlSource
    {
        public MessageHub(string connectionString) : base(connectionString)
        {
        }

        protected override EventEntry EntrySelector(SqlDataReader reader)
        {
            return new EventEntry((long)reader["SequenceId"], (string)reader["Content"]);
        }
    }
}
