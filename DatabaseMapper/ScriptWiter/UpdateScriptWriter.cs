using DatabaseMapper.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.ScriptWiter
{
	public class UpdateScriptWriter:IDataWriter
	{
		private DataMap _dataMap;
		private string _fileName;
		private DbDataReader _dataReader;
		private int MAX_SIZE = (int)Math.Pow(2, 15);

		public UpdateScriptWriter(String fileName, DataMap dataMap)
		{
			_fileName = fileName;
			_dataMap = dataMap;
		}

		public void Write(DbDataReader dataReader)
		{
			_dataReader=dataReader;
   
			StringBuilder valuesQuery = new StringBuilder(10000000);
			StringBuilder query = new StringBuilder(10000000);

			int recordNumber=0;
			int split = 0;
			
			while (dataReader.Read())
			{
				Record record = CreateRecord(dataReader);
			   
				if (record.ValidForSaveToDatabase())
				{
					valuesQuery.Append(record.ToUpdateString());
				}

			   recordNumber++;

			   if (recordNumber % MAX_SIZE == 0)
			   {
				   split++;
				   query.Append(valuesQuery);

				   FileWriter fileWriter = new FileWriter(_fileName);
				   fileWriter.Write(query.ToString(), split);
				   fileWriter.Close();

				   query.Clear();
				   valuesQuery.Clear();
			   }

			   record.Clear();
			}

			query.Append(valuesQuery);

			FileWriter fw = new FileWriter(_fileName);
			fw.Write(query.ToString(), ++split);
			fw.Close();

			query.Clear();
			valuesQuery.Clear();
		}

		private Record CreateRecord(DbDataReader dataReader)
		{
			ColumnMap[] columnMaps = _dataMap.ColumnMaps;
			Record record = new Record(_dataMap);
			columnMaps.ToList().ForEach(
					s =>
					{
						if (s.SourceName != null)
						{
							if (s.DestinationName != null)
								record[s.DestinationName] = GetColumnData(s).ToString();
							else
								GetColumnData(s);
						}
						else if (s.SourceName == null)
						{
							record[s.DestinationName] = s.ConvertRawData(null).ToString();
						}

					});

			return record;
		}

		private object GetColumnData(ColumnMap map)
		{
			int sourceIndex = _dataReader.GetOrdinal(map.SourceName);
			String methodName="Get"+map.SourceDataType;

			Type type=_dataReader.GetType();
			MethodInfo methodInfo = type.GetMethod(methodName);
			object rawData=null;
			
			if (!_dataReader.IsDBNull(sourceIndex))
				rawData=methodInfo.Invoke(_dataReader, new object[] { sourceIndex });

			map.RawData = rawData;

			return map.ConvertRawData(rawData);  
		}
	}
}
