using BetajaDataConvert.Conversion.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetajaDataConvert.DatabaseDriver;
using BetajaDataConvert.ScriptWiter;
using System.Globalization;

namespace BetajaDataConvert
{
    class ConvertorDissuasion62Info
    {
        public static void Convert()
        {
            string sourceTable = "TblPrs";
            string destinaionTable = "dbo.dissuasion62info";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = @"C:\Users\mirsaeedi\Downloads\db1.mdb";


            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            dataMap.AddColumnMap(new ColumnMap("Cod62EndDat", "dissuasiondate", DbType.Int32, (o) => ConvertUtility.ConvertToGregorianDate(o)));

            dataMap.AddColumnMap(new ColumnMap("pno", "applicantinfoid", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o)));

            dataMap.AddColumnMap(new ColumnMap(null, "dissuasionletternumber", DbType.Int32, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "dissuasionreasonid", DbType.Int32, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "description", DbType.String, (o) => "null"));

            var sepratedDate = new ColumnMap("MonfakBegDat", null, DbType.Int32, (o) => "null");
            dataMap.AddColumnMap(sepratedDate);

            var sepratedCodeId = new ColumnMap("MonfakCod", null, DbType.Byte, (o) => "null");
            dataMap.AddColumnMap(sepratedCodeId);

            dataMap.CanSaveToDatabaseHandler = () =>
            {
                if (sepratedCodeId.RawData != null && (
                    sepratedCodeId.RawData.ToString() == "26" ||
                    sepratedCodeId.RawData.ToString() == "35" ||
                    sepratedCodeId.RawData.ToString() == "61" ||
                    sepratedCodeId.RawData.ToString() == "62" ||
                    sepratedCodeId.RawData.ToString() == "63" ||
                    sepratedCodeId.RawData.ToString() == "64" ||
                    sepratedCodeId.RawData.ToString() == "65" ||
                    sepratedCodeId.RawData.ToString() == "66" ||
                    sepratedCodeId.RawData.ToString() == "67" ||
                    sepratedCodeId.RawData.ToString() == "68" ||
                    sepratedCodeId.RawData.ToString() == "69"))
                {
                    var stringDate = ConvertUtility.ConvertToGregorianDate(sepratedDate.RawData);
                    if (stringDate.ToString() != "null")
                    {
                        string[] parts = stringDate.ToString().Replace("'", "").Split(new char[] { '/' });

                        string normalizedDate = ConvertUtility.Prefix(parts[0], '0', 2)
                            + '/' +
                            ConvertUtility.Prefix(parts[1], '0', 2)
                            + '/' +
                            parts[2];

                        DateTime seprated = DateTime.ParseExact
                            (normalizedDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        DateTime baseDate = new DateTime(1998, 3, 21);

                        if (seprated < baseDate)
                            return false;

                    }
                }

                return true;
            };

            InsertScriptWriter insertScriptWriter = new InsertScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);

            var dataReader = reader.ExecuteQuery(string.Format("SELECT pno,Cod62EndDat,MonfakBegDat,MonfakCod FROM {0} WHERE Cod62BegDat IS NOT NULL AND ((Cod62EndDat IS NULL AND AslMablagh62=0) OR (Cod62EndDat IS NOT NULL AND Cod62EndDat<>9209))", sourceTable));
            insertScriptWriter.Write(dataReader);
        }
    }
}
