using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public class AccessDriver : DbDriver
    {
        private string _dbFileName;

        public AccessDriver(DbConnectionString dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        public AccessDriver(string dbFileName,ConnectionType connectionType=ConnectionType.Odbc)
        {
            _dbFileName = dbFileName;
            _dbConnectionString = new AccessConnectionString(_dbFileName, connectionType);
        }

        public string[] FindNonDuplicate(string tableLeft,string columnLeft,string tableRight,string columnRight,DbType dbType,bool doNotAcceptZero=false)
        {

            using (OdbcConnection connection = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)}; DBQ=" + _dbFileName))
            {
                string query = "SELECT DISTINCT ({0}.{1}) AS Expr1 "+ 
                                "FROM {0} LEFT JOIN {2} ON {0}.{1} = {2}.{3} " +
                                "WHERE ((({2}.{3}) Is Null))";

                query = string.Format(query,tableLeft,columnLeft,tableRight,columnRight);

                OdbcCommand command = new OdbcCommand(query);
                command.Connection = connection;

                connection.Open();
                OdbcDataReader reader = command.ExecuteReader();
                List<string> unmatchedList = new List<string>();

                String methodName = "Get" + dbType.ToString();
                Type type = reader.GetType();
                MethodInfo methodInfo = type.GetMethod(methodName);
                object rawData = null;

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) )
                    {
                        rawData = methodInfo.Invoke(reader, new object[] { 0 });
                        
                        if(rawData.ToString()!="0")
                            unmatchedList.Add(rawData.ToString());
                    }
                }

                return unmatchedList.ToArray();
            }
        }

        /*
       public DataTable GetSchemaTable()
        {
            using (OleDbConnection connection = new
                       OleDbConnection(GetOleDbConnectionString()))
            {
                connection.Open();
                DataTable schemaTable = connection.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });
                return schemaTable;
            }
        }
         */
    }
}
