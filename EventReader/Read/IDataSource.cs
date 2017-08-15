﻿using System.Collections.Generic;

namespace EventReader.Read
{
    public interface IDataSource
    {
        IEnumerable<EventEntry> ExecuteQuery(EventType eventType, long sequenceId);

        IEnumerable<EventEntry> ExecuteQuery(string query, Dictionary<string, string> parameters);
    }
}
