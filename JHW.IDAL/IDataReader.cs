using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JHW.IDAL
{
    public interface IDataReader<T> where T : class
    {
        IEnumerable<T> Select(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null);

        T QuerySingle(Expression<Func<T, bool>> whereExpression);
    }
}
