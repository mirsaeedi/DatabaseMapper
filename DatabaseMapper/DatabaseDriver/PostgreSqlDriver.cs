using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public class PostgreSqlDriver: DbDriver
    {

        private string _dbName;
        private string _userName;
        private string _password;
        private string _server;

        public PostgreSqlDriver(string server, string dbName, string username, string password, bool hasPersistentConnection = false)
        {
            _server = server;
            _dbName = dbName;
            _userName = username;
            _password = password;
            _hasPersistentConnection = hasPersistentConnection;
            _dbConnectionString = new PostgreSqlConnectionString(_server, _dbName, _userName, _password, ConnectionType.Odbc);
        }

        public PostgreSqlDriver(DbConnectionString dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }
    }
}
