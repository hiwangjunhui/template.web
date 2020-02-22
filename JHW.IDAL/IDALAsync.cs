using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHW.IDAL
{
    public interface IDALAsync<T> : IDataReaderAsync<T>, IDataWriterAsync<T>, IDataChanged<T> where T : class
    {
    }
}
