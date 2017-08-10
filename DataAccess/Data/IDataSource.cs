using System.Collections.Generic;

namespace DataAccess.Data
{
    public interface IDataSource
    {
        // execute query
        IEnumerable<EventEntry> ExecuteQuery(string query, long sequenceId);
    }
}
