using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public class AccessConnectionString : DbConnectionString
    {
        private string _dbFileName;

        public AccessConnectionString(string dbFileName)
        {
            _dbFileName = dbFileName;
        }

        public AccessConnectionString(string dbFileName, ConnectionType connectionType=ConnectionType.Odbc)
        {
            
            _dbFileName = dbFileName;
            _connectionType = connectionType;
        }

        public override string OdbcString
        {
            get
            {
                string connectionString = string.Format("Driver={{Microsoft Access Driver (*.mdb)}}; DBQ={0};", _dbFileName);
                return connectionString;
            }
        }

        public override string OleDbString
        {
            get
            {
                string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};" +
                    "Persist Security Info=False;", _dbFileName);

                return connectionString;
            }
        }
    }
}
