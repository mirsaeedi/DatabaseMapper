using BetajaDataConvert.Conversion.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetajaDataConvert.DatabaseDriver;

namespace BetajaDataConvert
{
    
    class RankMiss
    {
        public static void FindNonDuplicate()
        {
            string tableLeft = "TblPrs";
            string columnLeft = "Rank";
            string tableRight = "darajecod";
            string columnRight = "Cod";
            DbType dbType = DbType.Int16;

            string databasePath = @"C:\Users\mirsaeedi\Downloads\db1.mdb";
            AccessDriver reader = new AccessDriver(databasePath);
            String[] result = reader.FindNonDuplicate(tableLeft, columnLeft, tableRight, columnRight, dbType);

            string query = "INSERT INTO {0} (\"{1}\",\"{2}\") VALUES ";
            query = string.Format(query, "dbo.degree", "id", "description");
            for (int i = 0; i < result.Length; i++)
            {
                query += "(";
                query += result[i] + ",\' \')";

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
