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
    }
}
