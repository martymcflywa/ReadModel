using System.Data.SqlClient;

namespace Repository.Data
{
    public class MessageHub : BaseSqlSource
    {
        public MessageHub(string connectionString) : base(connectionString)
        {
        }

        protected override SourceEntry EntrySelector(SqlDataReader reader)
        {
            return new SourceEntry((long)reader["SequenceId"], (string)reader["Content"]);
        }
    }
}
