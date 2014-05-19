using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseMapper.Model
{
    public class TableSchema
    {
        private List<String> _lstColumn = new List<string>();
        
        public int ColumnCount
        {
            get
            {
                return _lstColumn.Count;
            }
        }

        public void AddColumn(string column)
        {
            _lstColumn.Add(column);
        }

        public String this[int columnIndex]
        {
            get 
            {
                return _lstColumn[columnIndex];
            }
        }

    }
}
