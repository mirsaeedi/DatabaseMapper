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
    class ConvertorProject
    {
        public static void Convert()
        {
            string sourceTable = "dbo_TblPrjcts";
            string destinaionTable = "dbo.project";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = "db1.mdb";

            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            ColumnMap codColumnMap = new ColumnMap("Cod", "id", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o));
            dataMap.AddColumnMap(codColumnMap);

            ColumnMap begDateColumnMap = new ColumnMap("BegDate", "startscheduledate", DbType.String, (o) => ConvertUtility.ConvertToGregorianDate(o));
            dataMap.AddColumnMap(begDateColumnMap);

            ColumnMap NumerOfAzaColumnMap = new ColumnMap("NumerOfAza", "numberofmembers", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o));
            dataMap.AddColumnMap(NumerOfAzaColumnMap);

            ColumnMap titlcolumnMap = new ColumnMap("Titl", "name", DbType.String, (o) => ConvertUtility.ConvertToString(o));
            dataMap.AddColumnMap(titlcolumnMap);

            ColumnMap addresscolumnMap = new ColumnMap(null, "address", DbType.String, (o) => "null");
            dataMap.AddColumnMap(addresscolumnMap);

            ColumnMap costcolumnMap = new ColumnMap(null, "cost", DbType.String, (o) => "null");
            dataMap.AddColumnMap(costcolumnMap);

            ColumnMap endscheduledatecolumnMap = new ColumnMap(null, "endscheduledate", DbType.String, (o) => "null");
            dataMap.AddColumnMap(endscheduledatecolumnMap);

            ColumnMap estateidcolumnMap = new ColumnMap(null, "estateid", DbType.String, (o) => "null");
            dataMap.AddColumnMap(estateidcolumnMap);

            ColumnMap ownercooperativecompanyidcolumnMap = new ColumnMap(null, "ownercooperativecompanyid", DbType.String, (o) => "null");
            dataMap.AddColumnMap(ownercooperativecompanyidcolumnMap);

            ColumnMap executercompanyidcolumnMap = new ColumnMap(null, "executercompanyid", DbType.String, (o) => "null");
            dataMap.AddColumnMap(executercompanyidcolumnMap);

            InsertScriptWriter insertScriptWriter = new InsertScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);

            var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0}", sourceTable));
            insertScriptWriter.Write(dataReader);
        }
    }
}
