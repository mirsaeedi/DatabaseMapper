﻿using BetajaDataConvert.Conversion.Model;
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
    class ConvertorProjectMembership
    {
        public static void Convert()
        {
            string sourceTable = "TblPrs";
            string destinaionTable = "dbo.projectmembership";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = @"C:\Users\mirsaeedi\Downloads\db1.mdb";

            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            ColumnMap begDateColumnMap = new ColumnMap(null, "projectcontractnumber", DbType.String, (o) =>"null");
            dataMap.AddColumnMap(begDateColumnMap);

            ColumnMap NumerOfAzaColumnMap = new ColumnMap("OzviatCod", "membershipstatusid", DbType.Int16, (o) => ConvertUtility.ConvertToInt(o));
            dataMap.AddColumnMap(NumerOfAzaColumnMap);

            ColumnMap titlcolumnMap = new ColumnMap("pno", "applicantinfoid", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o));
            dataMap.AddColumnMap(titlcolumnMap);

            ColumnMap addresscolumnMap = new ColumnMap(null, "apartmenttypeid", DbType.String, (o) => "null");
            dataMap.AddColumnMap(addresscolumnMap);

            ColumnMap costcolumnMap = new ColumnMap("PrjctCod", "projectid", DbType.Int16, (o) => ConvertUtility.ConvertToInt(o));
            dataMap.AddColumnMap(costcolumnMap);

            ColumnMap endscheduledatecolumnMap = new ColumnMap(null, "apartmentid", DbType.String, (o) => "null");
            dataMap.AddColumnMap(endscheduledatecolumnMap);

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

            var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0} WHERE PrjctCod IS NOT NULL", sourceTable));
            insertScriptWriter.Write(dataReader);
        }
    }
}
