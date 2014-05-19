using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    class FoxProDriver : DbDriver
    {
        private string _dbFileName;
        

        public FoxProDriver(DbConnectionString dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        public FoxProDriver(string dbFileName, ConnectionType connectionType = ConnectionType.Odbc)
        {
            _dbFileName = dbFileName;
            _dbConnectionString = new FoxProConnectionString(_dbFileName, connectionType);
        }
    }
}
