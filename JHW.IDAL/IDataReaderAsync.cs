using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JHW.IDAL
{
    public interface IDataReaderAsync<T> where T : class
    {
        Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null);
    }
}
