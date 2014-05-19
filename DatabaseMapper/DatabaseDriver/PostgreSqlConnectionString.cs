using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public class PostgreSqlConnectionString : DbConnectionString
    {
        private string _password;
        private string _userName;
        private string _dbName;
        private string _server;


        public PostgreSqlConnectionString(string server, string dbName, string userName, string password, ConnectionType connectionType=ConnectionType.Odbc)
        {
            _server = server;
            _dbName = dbName;
            _userName = userName;
            _password = password;
            _connectionType = connectionType;
        }

        public override string OdbcString
        {
            get
            {
                string connectionString = string.Format("Driver={{PostgreSQL UNICODE}};Server={0};Database={1}; UID={2}; PWD={3}"
                    , _server, _dbName, _userName, _password);

                return connectionString;
            }
        }

        public override string OleDbString
        {
            get { throw new NotImplementedException(); }
        }
    }
}
