using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.Model
{
    public interface IDataWriter
    {
        void Write(DbDataReader dataReader);
    }
}
