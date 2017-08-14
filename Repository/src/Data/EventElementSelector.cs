using System;
using System.Data.SqlClient;

namespace Repository.Data
{
    public class EventElementSelector : IElementSelector
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
