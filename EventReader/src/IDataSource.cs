using System.Collections.Generic;

namespace EventReader
{
    public interface IDataSource
    {
        IEnumerable<EventEntry> ExecuteQuery(string query, long sequenceId);
    }
}
