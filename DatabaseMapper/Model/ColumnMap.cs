using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.Model
{
    public class ColumnMap
    {
        private string _destinationName;
        private string _sourceName;
        private ColumnDataConvertor _columnDataConvertor;
        private DbType _dbType;
        private int _sourceIndex;
        private object _rawData;
        private ColumnMappingType _mappingType;
        private ConstraintType _constraintType;
        public delegate object ColumnDataConvertor(object rawData);

        public ColumnMap(string sourceName, string destinationName, DbType dbType, ColumnDataConvertor columnDataConvertor, ConstraintType constraintType = ConstraintType.Normal)
        {
            _constraintType = constraintType;
            _mappingType = ColumnMappingType.ColumnToColumn;
            _dbType = dbType;
            _columnDataConvertor = columnDataConvertor;
            _sourceName = sourceName;
            _destinationName = destinationName;
        }

        public ColumnMap(string sourceName, DbType dbType, ColumnDataConvertor columnDataConvertor)
        {
            _mappingType = ColumnMappingType.ColumnToRecord;
            _dbType = dbType;
            _columnDataConvertor = columnDataConvertor;
            _sourceName = sourceName;
        }

        public string SourceName
        {
            get { return _sourceName; }
        }

        public string DestinationName
        {
            get { return _destinationName; }
        }


        public string SourceDataType
        {
            get 
            {
                return Enum.GetName(typeof(DbType),_dbType);
            }
        }

        public object RawData
        {
            get { return _rawData; }
            set { _rawData = value; }
        }

        public object ConvertRawData(object rawData)
        {
            return _columnDataConvertor(rawData);
        }

        public int SourceIndex { get { return _sourceIndex; } }

        public ColumnMappingType ColumnMappingType { get { return _mappingType; } }

        public ConstraintType ConstraintType { get { return _constraintType; } }
    }
}
