using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EventReader.Read
{
    public class SqlSource : IDataSource
    {
        const string CONNECTION_STRING = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
        IElementSelector Selector = new MessageHubSelector();

        const string CUSTOMER_CREATED_QUERY =
            "select top 100 * " +
            "from MessageHub.Message as t0 " +
            "join MessageHub.MessageContent as t1 " +
            "on t0.SequenceId = t1.SequenceId " +
            "where (t0.MessageTypeId = 1 or t0.MessageTypeId = 16) and t0.AggregateTypeId = 11 and " +
            "t0.SequenceId > @sequenceId ";

        const string REPAYMENT_TAKEN_QUERY =
            "select top 100 * " +
            "from MessageHub.Message as t0 " +
            "join MessageHub.MessageContent as t1 " +
            "on t0.sequenceId = t1.sequenceId " +
            "where MessageTypeId in (83, 84, 85, 87, 89, 92) and " +
            "t0.AggregateTypeId = 12 and " +
            "t0.SequenceId > @sequenceId ";

        public IEnumerable<EventEntry> ExecuteQuery(EventType eventType, long sequenceId)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand command = CreateCommand(eventType, connection);
                using (command)
                {
                    command.Parameters.Add(new SqlParameter("@sequenceId", sequenceId));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return Selector.Select(reader);
                        }
                    }
                }
            }
        }

        SqlCommand CreateCommand(EventType eventType, SqlConnection connection)
        {
            switch (eventType)
            {
                case EventType.CustomerCreated:
                    return new SqlCommand(CUSTOMER_CREATED_QUERY, connection);
                case EventType.RepaymentTaken:
                    return new SqlCommand(REPAYMENT_TAKEN_QUERY, connection);
                default:
                    throw new NotImplementedException("Unsupported EventType.");
            }
        }

        public IEnumerable<EventEntry> ExecuteQuery(string query, Dictionary<string, string> parameters)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if(parameters != null)
                    {
                        foreach(var item in parameters)
                        {
                            command.Parameters.Add(new SqlParameter(item.Key, item.Value));
                        }
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return Selector.Select(reader);
                        }
                    }
                }
            }
        }
    }
}
