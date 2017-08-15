using System;
using System.Data.SqlClient;

namespace EventReader.Read
{
    public class MessageHubSelector : IElementSelector
    {
        public EventEntry Select(SqlDataReader reader)
        {
            return new EventEntry(
                (long)reader["SequenceId"],
                (short)reader["AggregateTypeId"],
                (short)reader["MessageTypeId"],
                (string)reader["Content"]);
        }
    }
}
