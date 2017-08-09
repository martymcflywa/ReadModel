using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public static class Repository
    {
        public static bool Connect()
        {
            using (SqlConnection conn = new SqlConnection(SQLHelper.ConnectionString))
            {
                conn.Open();
                if(conn.State == ConnectionState.Open)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
