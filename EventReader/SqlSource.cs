using System.Collections.Generic;
using System.Data.SqlClient;

namespace EventReader
{
    public class SqlSource : IDataSource
    {
        private readonly string _connectionString;
        private readonly IElementSelector _selector;

        private const string CustomersAndRepaymentsQuery =
            "select top 1000 t0.SequenceId, t0.AggregateTypeId, t0.MessageTypeId, t1.SequenceId as SequenceId, t1.Content " +
            "from MessageHub.Message as t0 " +
            "inner join MessageHub.MessageContent as t1 on t0.SequenceId = t1.SequenceId " +
            // customer created or imported
            "where (t0.AggregateTypeId = 11 and t0.MessageTypeId in (1, 16)) or " +
            // loan repayment
            "(t0.AggregateTypeId = 12 and t0.MessageTypeId in (83, 84, 85, 87, 89, 92)) and " +
            "t0.SequenceId > @sequenceId " +
            "order by t0.SequenceId ";

        public SqlSource()
        {
            // default for now
            _connectionString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
            _selector = new MessageHubSelector();
        }

        public SqlSource(string connectionString)
        {
            _connectionString = connectionString;
            _selector = new MessageHubSelector();
        }

        public IEnumerable<EventEntry> ExecuteQuery(long sequenceId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(CustomersAndRepaymentsQuery, connection);
                using (command)
                {
                    command.Parameters.Add(new SqlParameter("@sequenceId", sequenceId));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return _selector.Select(reader);
                        }
                    }
                }
            }
        }
    }
}
