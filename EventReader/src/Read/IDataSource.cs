using System.Collections.Generic;

namespace EventReader.Read
{
    public interface IDataSource
    {
        IEnumerable<EventEntry> ExecuteQuery(string query, long sequenceId);
    }
}
