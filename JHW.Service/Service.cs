using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JHW.Service
{
    public class Service<T> : IService.IService<T> where T : class
    {
        private ICache.ICache _cache;
        private IDAL.IDAL<T> _dal;

        public Service(ICache.ICache cache, IDAL.IDAL<T> dal)
        {
            _cache = cache;
            _dal = dal;
        }

        public int ClearTable()
        {
            return _dal.ClearTable();
        }

        public void Insert(IEnumerable<T> entities)
        {
            _dal.Insert(entities);
        }

        public T QuerySingle(Expression<Func<T, bool>> whereExpression)
        {
            return _dal.QuerySingle(whereExpression);
        }

        public int Remove(Expression<Func<T, bool>> whereExpression)
        {
            return _dal.Delete(whereExpression);
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null)
        {
            return _dal.Select(whereExpression, orderByExpression);
        }
    }
}
