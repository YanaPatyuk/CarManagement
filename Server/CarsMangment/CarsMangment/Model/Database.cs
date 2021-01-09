using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using MySqlConnector;


namespace CarsMangment.Model
{
    public class Database : IDisposable
    {
        public MySqlConnection Connection { get; }

        public Database(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
