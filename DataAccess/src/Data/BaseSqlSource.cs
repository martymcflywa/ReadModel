using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Data
{
    public abstract class BaseSqlSource : IDataSource
    {
        public readonly string _connectionString;

        public BaseSqlSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected abstract SourceEntry EntrySelector(SqlDataReader reader);

        public IEnumerable<SourceEntry> ExecuteQuery(string query, long sequenceId)
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
