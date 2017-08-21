using System.Collections.Generic;
using System.Data.SqlClient;

namespace EventReader
{
    public class SqlSource : IDataSource
    {
        private readonly string _connectionString;
        private readonly IElementSelector _selector;

        private const string CustomersAndRepaymentsQuery =
            "select * " +
            "from MessageHub.Message as t0 " +
            "inner join MessageHub.MessageContent as t1 on t0.SequenceId = t1.SequenceId " +
            "where (t0.SequenceId > @sequenceId) and " +
            // customer created or imported
            "((t0.AggregateTypeId = 11 and t0.MessageTypeId in (1, 16)) or " +
            // loan repayment
            "(t0.AggregateTypeId = 12 and t0.MessageTypeId in (83, 84, 85, 87, 89, 92))) " +
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
