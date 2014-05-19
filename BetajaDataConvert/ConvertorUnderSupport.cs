﻿using BetajaDataConvert.Conversion.Model;
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
    class ConvertorUnderSupport
    {

        public static void Convert()
        {
            string sourceTable = "Table1";
            string destinaionTable = "dbo.undersupportinfo";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = "db1.mdb";


            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);
           
            dataMap.AddColumnMap(new ColumnMap("s2p", "applicantinfoid", DbType.String, (o) => ConvertUtility.ConvertToInt(o)));
            dataMap.AddColumnMap(new ColumnMap("n", "firstname", DbType.String, (o) => ConvertUtility.ConvertToString(o)));
            dataMap.AddColumnMap(new ColumnMap("nk2", "lastname", DbType.String, (o) => ConvertUtility.ConvertToString(o)));
            dataMap.AddColumnMap(new ColumnMap("np", "fathername", DbType.String, (o) => ConvertUtility.ConvertToString(o)));
            dataMap.AddColumnMap(new ColumnMap("cml", "nationalid", DbType.String, (o) => ConvertUtility.ConvertToString(o)));
            dataMap.AddColumnMap(new ColumnMap("s2s", "identificationid", DbType.String, (o) => ConvertUtility.ConvertToString(o)));
            ColumnMap nsbtColumnMap = new ColumnMap("nsbt", null, DbType.String, (o) => ConvertUtility.ConvertToString(o));
            dataMap.AddColumnMap(nsbtColumnMap);
            dataMap.AddColumnMap(new ColumnMap(null, "undersupportrelationid", DbType.String, (o) => ConvertUtility.ConvertToUnderSupportRelationId(nsbtColumnMap.RawData.ToString())));
            dataMap.AddColumnMap(new ColumnMap("ttv", "dateofbirth", DbType.String, (o) =>
                ConvertUtility.ConvertToGregorianDate(o)));
            dataMap.AddColumnMap(new ColumnMap(null, "applicantmaterialstatusid", DbType.String, (o) => "null"));
            dataMap.AddColumnMap(new ColumnMap(null, "applicanteducationid", DbType.String, (o) => "null"));

            dataMap.CanSaveToDatabaseHandler = () => nsbtColumnMap.RawData.ToString() != "سرپرست";

            InsertScriptWriter insertScriptWriter = new InsertScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);

            var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0}", sourceTable));
            insertScriptWriter.Write(dataReader);
        }

     
    }
}