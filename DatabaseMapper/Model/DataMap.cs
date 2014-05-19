using DatabaseMapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.Model
{
    public class DataMap
    {
        private string _tableSourceName;
        private string _tableDestinationName;
        public delegate bool CanSaveToDatabaseDelegate();

        private List<ColumnMap> _columnMaps = new List<ColumnMap>();
        private CanSaveToDatabaseDelegate _CanSaveToDatabaseHandler;

        public void SetTablesName(string tableSourceName, string tableDestinationName)
        {
            _tableSourceName = tableSourceName;
            _tableDestinationName = tableDestinationName;
        }

        public void AddColumnMap(ColumnMap columnMap)
        {
            _columnMaps.Add(columnMap);
        }

        public string[] getDestinationColumns()
        {

            return _columnMaps.Select(c => c.DestinationName).ToArray();
        }

        public string[] getSourceColumns()
        {

            return _columnMaps.Select(c => c.SourceName).ToArray();
        }

        public string GetSourceTableName()
        {

            return _tableSourceName;
        }

        public string GetDestinationTableName()
        {

            return _tableDestinationName;
        }

        public ColumnMap GetColumnMap(string sourceColumn)
        {

            return _columnMaps.Where(c => c.SourceName == sourceColumn).SingleOrDefault();
        }

        internal ColumnMap GetColumnMap(int sourceIndex)
        {
            return _columnMaps.Where(c => c.SourceIndex == sourceIndex).SingleOrDefault();
        }

        public ColumnMap[] ColumnMaps
        {
            get { return _columnMaps.ToArray();}
        }

        public CanSaveToDatabaseDelegate CanSaveToDatabaseHandler
        {
            set {_CanSaveToDatabaseHandler = value;}
        }


        internal bool CanSaveToDatabase()
        {
            if (_CanSaveToDatabaseHandler == null)
                return true;

            return _CanSaveToDatabaseHandler();
        }

        internal string GetDestinationPrimaryKey()
        {
            return _columnMaps.Where(c => c.ConstraintType == ConstraintType.PrimaryKey).First().DestinationName;
        }
    }
}
