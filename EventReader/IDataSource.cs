using ReadModel.Events;
using System.Collections.Generic;

namespace EventReader
{
    public interface IDataSource
    {
        IEnumerable<EventEntry> ExecuteQuery(long sequenceId);
    }
}
