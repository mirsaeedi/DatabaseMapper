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
    class ConvertorApplicantNationlIdFromBagheri
    {
        public static void Convert()
        {
            string sourceTable = "main";
            string destinaionTable = "dbo.applicantinfo";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = "db1.mdb";

            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            dataMap.AddColumnMap(new ColumnMap("pno", "id", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o), ConstraintType.PrimaryKey));
            dataMap.AddColumnMap(new ColumnMap("mellicod", "nationalcode", DbType.String, (o) => ConvertUtility.ConvertToString(o)));

            UpdateScriptWriter insertScriptWriter = new UpdateScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);

            var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0}", sourceTable));
            insertScriptWriter.Write(dataReader);
        }
    }
}
