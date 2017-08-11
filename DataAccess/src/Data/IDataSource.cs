using System.Collections.Generic;

namespace DataAccess.Data
{
    public interface IDataSource
    {
        IEnumerable<EventEntry> ExecuteQuery(string query, long sequenceId);
    }
}
