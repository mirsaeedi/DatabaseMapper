using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public class FoxProConnectionString:DbConnectionString
    {
         private string _dbFileName;

         public FoxProConnectionString(string dbFileName, ConnectionType connectionType=ConnectionType.Oledb)
         {
             _dbFileName = dbFileName;
             _connectionType = connectionType;
         }

        public override string OdbcString
        {
            get { throw new NotImplementedException(); }
        }

        public override string OleDbString
        {
            get
            {
                string connectionString = string.Format("Provider=vfpoledb;Data Source={0};Collating Sequence=general;", _dbFileName);
                return connectionString;
            }
        }
    }
}
