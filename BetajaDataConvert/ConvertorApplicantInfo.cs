using BetajaDataConvert.Conversion.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetajaDataConvert.DatabaseDriver;
using BetajaDataConvert.ScriptWiter;

namespace BetajaDataConvert
{
    
    class ConvertorApplicantInfo
    {
        public static void Convert()
        {
            string sourceTable = "TblPrs";
            string destinaionTable = "dbo.applicantinfo";
            string filePath = @"convertScripts\" + destinaionTable + ".sql";
            string databasePath = @"C:\Users\mirsaeedi\Downloads\db1.mdb";


            DataMap dataMap = new DataMap();

            dataMap.SetTablesName(sourceTable, destinaionTable);

            dataMap.AddColumnMap(new ColumnMap("pno", "id", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o)));

            dataMap.AddColumnMap(new ColumnMap("Nam", "name", DbType.String, (o) => ConvertUtility.ConvertToString(o)));

            dataMap.AddColumnMap(new ColumnMap("Fam", "lastname", DbType.String, (o) => ConvertUtility.ConvertToString(o)));

            dataMap.AddColumnMap(new ColumnMap(null, "nationalcode", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap("BrthDat", "dateofbirth", DbType.Int32, (o) => ConvertUtility.ConvertToGregorianDate(o)));

            dataMap.AddColumnMap(new ColumnMap("BrthPlcCod", "placeofbirth", DbType.Int32, (o) => ConvertUtility.ConvertToString(o)));

            dataMap.AddColumnMap(new ColumnMap("IdNo", "birthcertificatenumber", DbType.Int32, (o) => ConvertUtility.ConvertToString(o)));

            dataMap.AddColumnMap(new ColumnMap("Sanavat", "historyyear", DbType.String, (o) =>
            {
                var data = ConvertUtility.ConvertToInt(o,false).ToString();
                if (data != "null")
                    ConvertUtility.Prefix(data,'0',6);

                return data;
            }));

            dataMap.AddColumnMap(new ColumnMap("EstDat", "employmentdate", DbType.Int32, (o) => ConvertUtility.ConvertToGregorianDate(o)));

            dataMap.AddColumnMap(new ColumnMap(null, "address", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "mobilenumber", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "phonecode", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "phonenumber", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap(null, "email", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap("VaziatTaaholCod", "applicantmaritalstatusid", DbType.Byte, (o) => ConvertUtility.ConvertToInt(o)));

            dataMap.AddColumnMap(new ColumnMap("Rank", "degreeid", DbType.Int16, (o) => ConvertUtility.ConvertToInt(o,true)));

            dataMap.AddColumnMap(new ColumnMap("HoviatKhedmatiCod", "applicantidentityid", DbType.Byte, (o) => ConvertUtility.ConvertToInt(o, true)));

            dataMap.AddColumnMap(new ColumnMap("YegCod", "militaryunitid", DbType.Int32, (o) => ConvertUtility.ConvertToInt(o,true)));

            dataMap.AddColumnMap(new ColumnMap("VaziatKhedmaticod", "applicantservicestatusid", DbType.Byte, (o) => ConvertUtility.ConvertToInt(o,true)));

            dataMap.AddColumnMap(new ColumnMap(null, "townshipid", DbType.String, (o) =>"null"));

            dataMap.AddColumnMap(new ColumnMap(null, "applicanteducationid", DbType.String, (o) => "null"));

            dataMap.AddColumnMap(new ColumnMap("FNam", "fathername", DbType.String, (o) => ConvertUtility.ConvertToString(o)));

            var sepratedDate = new ColumnMap("MonfakBegDat", "seprateddate", DbType.Int32, (o) => ConvertUtility.ConvertToGregorianDate(o));
            dataMap.AddColumnMap(sepratedDate);

            dataMap.AddColumnMap(new ColumnMap("GenderCod", "applicantsexid", DbType.Byte, (o) => ConvertUtility.ConvertToInt(o)));

            dataMap.AddColumnMap(new ColumnMap("KhanehsazmaniKndCod", "orgazinationalhouseid", DbType.Byte, (o) => ConvertUtility.ConvertToInt(o,true)));

            dataMap.AddColumnMap(new ColumnMap("DaraieCod", "assetcodeid", DbType.Byte, (o) => ConvertUtility.ConvertToInt(o)));

            var sepratedCodeId = new ColumnMap("MonfakCod", "sepratedcodeid", DbType.Byte, (o) => ConvertUtility.ConvertToInt(o, true));
            dataMap.AddColumnMap(sepratedCodeId);

            ColumnMap code62begdatColumnMap = new ColumnMap("Cod62BegDat", null, DbType.Int16, (o) => ConvertUtility.ConvertToGregorianDate(o));

            dataMap.AddColumnMap(code62begdatColumnMap);

            ColumnMap code62enddatColumnMap = new ColumnMap("Cod62EndDat", null, DbType.Int32, (o) => ConvertUtility.ConvertToGregorianDate(o));

            dataMap.AddColumnMap(code62enddatColumnMap);

            ColumnMap aslMablagh62ColumnMap = new ColumnMap("AslMablagh62", null, DbType.Int32, (o) => o);

            dataMap.AddColumnMap(aslMablagh62ColumnMap);

            dataMap.AddColumnMap(new ColumnMap(null, "hekmatstatusid", DbType.String, (o) => 
            {
                if (code62begdatColumnMap.RawData == null)
                {
                    return 1;
                }
                else if (code62enddatColumnMap.RawData != null && code62enddatColumnMap.RawData.ToString() != "9209")
                {
                    return 3;
                }
                else if (code62enddatColumnMap.RawData != null && aslMablagh62ColumnMap.RawData!=null && aslMablagh62ColumnMap.RawData.ToString() == "0")
                {
                    return 3;
                }
                else 
                {
                    return 2;
                }

            }));

            dataMap.CanSaveToDatabaseHandler = () =>
            {
                if (sepratedCodeId.RawData!=null && (
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
                        string[] parts = stringDate.ToString().Replace("'", "").Split(new char[] {'/'});
                        
                        string normalizedDate=ConvertUtility.Prefix(parts[0],'0',2)
                            +'/'+
                            ConvertUtility.Prefix(parts[1],'0',2)
                            +'/'+
                            parts[2];

                        DateTime seprated = DateTime.ParseExact
                            (normalizedDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        DateTime baseDate = new DateTime(1998, 3, 21);

                        if (seprated<baseDate)
                            return false;

                    }
                }

                return true;
            };

            DbDriver sourceReader = new AccessDriver(databasePath);
            string selectQuery=string.Format("SELECT * FROM {0}", sourceTable);

            InsertScriptWriter insertScriptWriter = new InsertScriptWriter(filePath, dataMap);
            insertScriptWriter.Write(sourceReader, selectQuery);
        }
    }
}
