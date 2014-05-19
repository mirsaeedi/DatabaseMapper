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
    
    class ConvertorMilitaryUnit
    {
        public static void Convert()
        {
            string sourceTable = "dbo_TblCodYegan";
            string destinaionTable = "dbo.military_unit";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = "db1.mdb";

            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            ColumnMap codColumnMap = new ColumnMap("Cod", "id", DbType.Int32, (o) => o);
            dataMap.AddColumnMap(codColumnMap);

            ColumnMap titlcolumnMap = new ColumnMap("Titl", "description", DbType.String, (o) => "\'" + o.ToString() + "\'");
            dataMap.AddColumnMap(titlcolumnMap);

            ColumnMap forcecolumnMap = new ColumnMap(null, "militaryforceid", DbType.String, (o) => codColumnMap.RawData.ToString().Substring(0, 1));
            dataMap.AddColumnMap(forcecolumnMap);


            InsertScriptWriter insertScriptWriter = new InsertScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);

            var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0}", sourceTable));
            insertScriptWriter.Write(dataReader);
        }
    }
}
