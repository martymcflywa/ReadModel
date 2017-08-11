using Repository.Data;
using System.Collections.Generic;

namespace Repository
{
    public static class SourceReader
    {
        public static IEnumerable<string> Read(IDataSource source, string query)
        {
            var sequenceId = -1L;
            while(true)
            {
                var entries = source.ExecuteQuery(query, sequenceId);
                foreach (SourceEntry entry in entries)
                {
                    sequenceId = entry.SequenceId;
                    yield return entry.Message;
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
