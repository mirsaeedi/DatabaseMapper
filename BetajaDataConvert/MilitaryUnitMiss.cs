using BetajaDataConvert.Conversion.Model;
using BetajaDataConvert.DatabaseDriver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetajaDataConvert
{
    class MilitaryUnitMiss
    {
        public static void FindNonDuplicate()
        {
            string tableLeft = "TblPrs";
            string columnLeft = "YegCod";
            string tableRight = "dbo_TblCodYegan";
            string columnRight = "Cod";
            DbType dbType = DbType.Int32;

            string databasePath = @"C:\Users\mirsaeedi\Downloads\db1.mdb";
            AccessDriver reader = new AccessDriver(databasePath);
            String[] result=reader.FindNonDuplicate(tableLeft,columnLeft,tableRight,columnRight,dbType);

            string query = "INSERT INTO {0} (\"{1}\",\"{2}\",\"{3}\") VALUES ";
            query = string.Format(query,"dbo.military_unit","id","description","militaryforceid");
            for (int i = 0; i < result.Length; i++)
            {
                query += "(";
                query += result[i] + ",\' \'," + result[i].Substring(0,1)+")";

                if (i < result.Length - 1)
                    query += ",";
                else
                    query += ";";
            }

            PostgreSqlDriver dbWriter = new PostgreSqlDriver("localhost", "Maskan", "maskan_admin", "12345");

            dbWriter.ExecuteNonQuery(query);
        }
    }
}
