using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public static class Repository
    {

        private static SqlDataReader ExecuteReader(string query)
        {
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader;
                    }
                }
            }
        }

        public static IEnumerable<string> GetEvents(string query)
        {
            var lastSequenceId = default(string);

            while(true)
            {
                if(lastSequenceId != null)
                {
                    query += "and MessageHub.Message.SequenceId > " + lastSequenceId;
                }

                using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                lastSequenceId = reader["SequenceId"].ToString();
                                yield return (string) reader["Content"];
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<string> Take(this IEnumerable<string> source, int count)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var i = 0;
                while (enumerator.MoveNext() && i++ < count)
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}
