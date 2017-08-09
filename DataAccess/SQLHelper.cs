using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class SQLHelper
    {
        private static string CONNECTION_STRING = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
        public static string ConnectionString { get { return CONNECTION_STRING; } }

        public static SqlDataReader ExecuteQuery(SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
        {
            SqlCommand cmd = conn.CreateCommand();
            return default(SqlDataReader);
        }

        private static void PrepareCommand(SqlConnection conn, SqlCommand cmd, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParams)
        {
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if(trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if(cmdParams != null)
            {
                foreach(SqlParameter param in cmdParams)
                {
                    if(param.Direction == ParameterDirection.InputOutput && param.Value == null)
                    {
                        param.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(param);
                }
            }
        }
    }
}
