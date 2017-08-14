using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EventReader.Read
{
    public class SqlSource : IDataSource
    {
        public readonly string _connectionString;
        private IElementSelector Selector;

        public SqlSource(string connectionString, IElementSelector selector)
        {
            _connectionString = connectionString;
            Selector = selector;
        }

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
                            yield return Selector.Select(reader);
                        }
                    }
                }
            }
        }
    }
}
