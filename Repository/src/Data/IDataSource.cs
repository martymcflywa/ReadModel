using System.Collections.Generic;

namespace Repository.Data
{
    public interface IDataSource
    {
        IEnumerable<EventEntry> ExecuteQuery(string query, long sequenceId);
    }
}
