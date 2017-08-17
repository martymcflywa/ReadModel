using ReadModel.Events;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EventReader
{
    public static class Reader
    {
        public static IEnumerable<EventEntry> Read(this IDataSource source)
        {
            var sequenceId = -1L;
            while(true)
            {
                var entries = source.ExecuteQuery(sequenceId);
                foreach (var entry in entries)
                {
                    sequenceId = entry.SequenceId;
                    yield return entry;
                }
            }
        }
    }
}
