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
    class ConvertorApplicantLoanInfoTblPrsVam
    {
        public static void Convert()
        {
            string sourceTable = "dbo_TblPrsVam161";
            string destinaionTable = "dbo.applicantloaninfo";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = @"C:\Users\mirsaeedi\Downloads\db1.mdb";


            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            dataMap.AddColumnMap(new ColumnMap("pno", "applicantinfoid", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o)));

            dataMap.AddColumnMap(new ColumnMap(null, "lounreasonid", DbType.Int32, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "letternumber", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "letterdate", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap("AslMablagh161", "louncost", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o)));

            dataMap.AddColumnMap(new ColumnMap("Cod161Dat", "loundate", DbType.String, (o) => ConvertUtility.ConvertToGregorianDate(o)));

            dataMap.AddColumnMap(new ColumnMap(null, "description", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "reaymentdue", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "duedate", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "installmentcost", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "branchid", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "lounnumber", DbType.String, (o) => "null"));

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
            var dataReader = reader.ExecuteQuery(
                string.Format("SELECT {0}.pno, {0}.AslMablagh161, {0}.Cod161Dat,TblPrs.MonfakBegDat,TblPrs.MonfakCod " +
                        "FROM ( {0} LEFT JOIN dbo_TblVam ON {0}.[pno] = dbo_TblVam.[Pno] ) " +
                        "INNER JOIN TblPrs ON {0}.[pno] = TblPrs.[pno] " +
                        "WHERE (((dbo_TblVam.Pno) Is Null));", sourceTable));
            insertScriptWriter.Write(dataReader);
        }
    }
}
