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
    class ConvertorApplicantNationlIdFromUnderSupport
    {
        public static void Convert()
        {
            string sourceTable = "Table1";
            string destinaionTable = "dbo.applicantinfo";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = "db1.mdb";

            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            dataMap.AddColumnMap(new ColumnMap("s2p", "id", DbType.String, (o) => ConvertUtility.ConvertToInt(o), ConstraintType.PrimaryKey));
            dataMap.AddColumnMap(new ColumnMap("cml", "nationalcode", DbType.String, (o) => ConvertUtility.ConvertToString(o)));

            UpdateScriptWriter insertScriptWriter = new UpdateScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);

             var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0}  WHERE cyk2 IS NOT NULL", sourceTable));
             insertScriptWriter.Write(dataReader);
        }
    }
}
