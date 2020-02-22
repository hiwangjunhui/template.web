using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JHW.IService
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> Select(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null);

        T QuerySingle(Expression<Func<T, bool>> whereExpression);

        void Insert(IEnumerable<T> entities);

        int Remove(Expression<Func<T, bool>> whereExpression);

        int ClearTable();
    }
}
