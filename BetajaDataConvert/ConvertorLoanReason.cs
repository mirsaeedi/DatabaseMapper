using BetajaDataConvert.Conversion.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetajaDataConvert.DatabaseDriver;
using BetajaDataConvert.ScriptWiter;

namespace BetajaDataConvert
{
    class ConvertorLoanReason
    {
        public static void Convert()
        {
            string sourceTable = "dbo_TblCodVamType";
            string destinaionTable = "dbo.loanreason";
            
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = "db1.mdb";

            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            dataMap.AddColumnMap(new ColumnMap("Cod", "id", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o)));

            dataMap.AddColumnMap(new ColumnMap("Titl", "description", DbType.String, (o) => ConvertUtility.ConvertToString(o)));

            dataMap.AddColumnMap(new ColumnMap(null, "loantypeid", DbType.Int32, (o) => "1"));

            InsertScriptWriter insertScriptWriter = new InsertScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);

            var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0}", sourceTable));
            insertScriptWriter.Write(dataReader);
        }
    }
}
