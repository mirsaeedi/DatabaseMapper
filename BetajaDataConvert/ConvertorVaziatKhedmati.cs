﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetajaDataConvert.Conversion.Model;
using System.Data;
using BetajaDataConvert.DatabaseDriver;
using BetajaDataConvert.ScriptWiter;

namespace BetajaDataConvert
{
    public class ConvertorVaziatKhedmati
    {
        public static void Convert()
        {
            string sourceTable = "vaziatkhedmaticod";
            string destinaionTable = "dbo.applicantservicestatus";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = "db1.mdb";

            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            ColumnMap codColumnMap = new ColumnMap("Cod", "id", DbType.Int16, (o) => o);
            dataMap.AddColumnMap(codColumnMap);

            ColumnMap titlcolumnMap = new ColumnMap("Titl", "description", DbType.String, (o) => "\'" + o.ToString() + "\'");
            dataMap.AddColumnMap(titlcolumnMap);


            InsertScriptWriter insertScriptWriter = new InsertScriptWriter(filePath, dataMap);
            AccessDriver reader = new AccessDriver(databasePath);
            var dataReader = reader.ExecuteQuery(string.Format("SELECT * FROM {0}", sourceTable));
            insertScriptWriter.Write(dataReader);

        }
    }
}
