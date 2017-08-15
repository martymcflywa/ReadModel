using System.Collections.Generic;
using System.Data.SqlClient;

namespace EventReader.Read
{
    public static class Reader
    {
        public static IEnumerable<EventEntry> Read(this IDataSource source, string query)
        {
            var sequenceId = -1L;
            while(true)
            {
                var sqlParam = new SqlParameter("@sequenceId", sequenceId);
                var entries = source.ExecuteQuery(query, sequenceId);
                foreach (EventEntry entry in entries)
                {
                    sequenceId = entry.SequenceId;
                    yield return entry;
                }
            }
        }

        public static IEnumerable<EventEntry> Read(this IDataSource source, string query, Dictionary<string, string> parameters)
        {
            var entries = source.ExecuteQuery(query, parameters);
            foreach(EventEntry entry in entries)
            {
                yield return entry;
            }
        }

        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
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
