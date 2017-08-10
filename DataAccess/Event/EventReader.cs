using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Event
{
    public static class EventReader
    {
        public static IEnumerable<string> Read(IDataSource source, string query)
        {
            var sequenceId = -1L;
            while(true)
            {
                var entries = source.ExecuteQuery(query, sequenceId);
                foreach (EventEntry entry in entries)
                {
                    sequenceId = entry.SequenceId;
                    yield return entry.Message;
                }
            }
        }

        [Obsolete]
        public static IEnumerable<string> Read(string query)
        {
            var lastSequenceId = 0L;
            while (true)
            {
                using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@lastSequenceId", lastSequenceId));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lastSequenceId = (long) reader["SequenceId"];
                                yield return reader["Content"].ToString();
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
