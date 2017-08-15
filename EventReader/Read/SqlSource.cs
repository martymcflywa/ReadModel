using System;
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

        public IEnumerable<EventEntry> ExecuteQuery(string query, Dictionary<string, string> parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if(parameters != null)
                    {
                        foreach(var item in parameters)
                        {
                            command.Parameters.Add(new SqlParameter(item.Key, item.Value));
                        }
                    }
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
