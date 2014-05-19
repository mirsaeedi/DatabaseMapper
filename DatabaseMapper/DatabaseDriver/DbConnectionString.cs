using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public abstract class DbConnectionString
    {
        protected ConnectionType _connectionType;

        public abstract string OdbcString
        {
            get;
        }

        public abstract string OleDbString
        {
            get;
        }

        public DbConnection GetConnection()
        {
            if (ConnectionType.Odbc == _connectionType)
                return new OdbcConnection(OdbcString);

            if (ConnectionType.Oledb == _connectionType)
                return new OleDbConnection(OleDbString);

            return null;
        }
    }
}
