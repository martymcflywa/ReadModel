using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Data
{
    public abstract class BaseSqlSource : IDataSource
    {
        public readonly string _connectionString;

        public BaseSqlSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected abstract EventEntry EntrySelector(SqlDataReader reader);

        public IEnumerable<EventEntry> ExecuteQuery(string query, long sequenceId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@sequenceId", sequenceId));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return EntrySelector(reader);
                        }
                    }
                }
            }
        }
    }
}
