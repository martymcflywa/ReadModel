using System.Data.SqlClient;

namespace Repository.Data
{
    public class Northwind : BaseSqlSource
    {

        public Northwind(string connectionString) : base(connectionString)
        {
        }

        protected override SourceEntry EntrySelector(SqlDataReader reader)
        {
            return new SourceEntry((int)reader[0], (string)reader[3]);
        }
    }
}
