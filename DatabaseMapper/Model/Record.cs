using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMapper.Model
{
    public class Record
    {
        Dictionary<string, string> _nameValueDic = new Dictionary<string, string>();
        private TableSchema _tableSchema;
        private DataMap _datamap;
        StringBuilder _recordString = new StringBuilder(1000);

        public Record(DataMap datamap)
        {
            _datamap = datamap;
        }

        public Record(TableSchema _tableSchema)
        {
            this._tableSchema = _tableSchema;
            for(int i=0;i<_tableSchema.ColumnCount;i++)
                _nameValueDic[_tableSchema[i]]="null";
        }

        public string this[string columnName]
        {
            get
            {
                return _nameValueDic[columnName];
            }
            set
            {
                _nameValueDic[columnName] = value;
            }
        }

        public override string ToString()
        {
            _recordString.Append("(");
           
            for (int i = 0; i < _nameValueDic.Count; i++)
            {
                _recordString.Append(_nameValueDic.ElementAt(i).Value+",");
            }

            _recordString.Remove(_recordString.Length - 1,1);
            _recordString.Append(")");

            return _recordString.ToString();
        }

        public void Clear()
        {
            _recordString.Clear();
            _nameValueDic.Clear();
        }

        public bool ValidForSaveToDatabase()
        {
            return _datamap.CanSaveToDatabase();
        }

        public string ToUpdateString()
        {
            string query="UPDATE {0} SET {1} WHERE {2};"; 
            string primaryKey=_datamap.GetDestinationPrimaryKey();
            string whereClause=primaryKey + "=" + _nameValueDic[primaryKey];

            for (int i = 0; i < _nameValueDic.Count; i++)
            {
                if(primaryKey!=_nameValueDic.ElementAt(i).Key)
                {
                    _recordString.Append(_nameValueDic.ElementAt(i).Key);
                    _recordString.Append("=");
                    _recordString.Append(_nameValueDic.ElementAt(i).Value);
                    if(i!=_nameValueDic.Count-1)
                        _recordString.Append(",");
                }
            }

            return string.Format(query, _datamap.GetDestinationTableName(), _recordString.ToString(), whereClause);
        }
    }
}
