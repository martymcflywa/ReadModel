using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Data
{
    public class Northwind : IDataSource
    {
        readonly string _connString;

        public Northwind(String connString)
        {
            _connString = connString;
        }

        public IEnumerable<EventEntry> ExecuteQuery(string query, long sequenceId)
        {
            using(SqlConnection conn = new SqlConnection(_connString))
            {
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
				    cmd.Parameters.Add(new SqlParameter("@sequenceId", sequenceId));
				    using(SqlDataReader reader = cmd.ExecuteReader())
				    {
				        while(reader.Read())
				        {
				            yield return new EventEntry((int) reader[0], (string) reader[3]); 
				        }
				    }
				}
            }
        }
    }
}
