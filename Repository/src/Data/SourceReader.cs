using System.Collections.Generic;

namespace Repository.Data
{
    public static class SourceReader
    {
        public static IEnumerable<EventEntry> Read(IDataSource source, string query)
        {
            var sequenceId = -1L;
            while(true)
            {
                var entries = source.ExecuteQuery(query, sequenceId);
                foreach (EventEntry entry in entries)
                {
                    sequenceId = entry.SequenceId;
                    yield return entry;
                }
            }
        }

        public static IEnumerable<EventEntry> Take(this IEnumerable<EventEntry> source, int count)
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
