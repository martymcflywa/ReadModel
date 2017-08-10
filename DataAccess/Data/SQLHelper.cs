using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Data
{
    public class SQLHelper
    {
        //private static string CONNECTION_STRING = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
        static string CONNECTION_STRING = @"Server=tcp:martynwind.database.windows.net,1433;Initial Catalog=Northwind;Persist Security Info=False;User ID=marty;Password=Northwind123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static string ConnectionString { get { return CONNECTION_STRING; } }
    }
}
