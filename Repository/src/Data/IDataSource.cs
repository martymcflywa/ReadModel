using System.Collections.Generic;

namespace Repository.Data
{
    public interface IDataSource
    {
        IEnumerable<SourceEntry> ExecuteQuery(string query, long sequenceId);
    }
}
