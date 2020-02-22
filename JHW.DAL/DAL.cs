using DapperLambda;
using JHW.IDAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JHW.DAL
{
    public class DAL<T> : IDAL<T>, IDALAsync<T>, IDataChanged<T> where T : class
    {
        private string _connStr;
        public DAL()
        {
            _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public event EventHandler<DataChangedEventArgs<T>> OnDataChanged;

        public int Delete(Expression<Func<T, bool>> whereExpression)
        {
            using (var db = GetDbContext())
            {
                var result = db.Delete(whereExpression).Execute();
                db.Sql("").Execute();

                if (result > 0)
                {
                    var data = db.Select<T>().Where(whereExpression).QueryMany();
                    OnDataChanged?.Invoke(this, new DataChangedEventArgs<T>(ChangedTypes.Deleted) { Data = data });
                }

                return result;
            }
        }

        public int ClearTable()
        {
            using (var db = GetDbContext())
            {
                return db.Delete<T>().Execute();
            }
        }

        public async Task<int> ClearTableAsync()
        {
            return await Task.Run(() => ClearTableAsync());
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Task.Run(() => Delete(whereExpression));
        }

        public dynamic Insert(T entity)
        {
            using (var db = GetDbContext())
            {
                try
                {
                    return db.Insert(entity).Execute();
                }
                finally
                {
                    OnDataChanged?.Invoke(this, new DataChangedEventArgs<T>(ChangedTypes.Inserted) { Data = entity.AsIEnumerable() });
                }
            }
        }

        public void Insert(IEnumerable<T> entities)
        {
            using (var db = GetDbContext())
            {
                try
                {
                    db.Insert(entities).Execute();
                }
                finally
                {
                    OnDataChanged?.Invoke(this, new DataChangedEventArgs<T>(ChangedTypes.Inserted) { Data = entities });
                }
            }
        }

        public async Task<dynamic> InsertAsync(T entity)
        {
            return await Task.Run(() => Insert(entity));
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => Insert(entities));
        }

        public T QuerySingle(Expression<Func<T, bool>> whereExpression)
        {
            using (var db = GetDbContext())
            {
                return db.Select(whereExpression).QuerySingle();
            }
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null)
        {
            using (var db = GetDbContext())
            {
                var sqlLam = db.Select<T>().Where(whereExpression);

                if (null != orderByExpression)
                {
                    sqlLam = sqlLam.OrderBy(orderByExpression);
                }

                return sqlLam.QueryMany();
            }
        }

        public async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> whereExpression, Expression<Func<T, object>> orderByExpression = null)
        {
            return await Task.Run(() => Select(whereExpression, orderByExpression));
        }

        public int Update(object entity, Expression<Func<T, bool>> whereExpression)
        {
            using (var db = GetDbContext())
            {
                var result = db.Update<T>().Set(entity).Where(whereExpression).Execute();

                if (result > 0)
                {
                    var data = db.Select<T>().Where(whereExpression).QueryMany();
                    OnDataChanged?.Invoke(this, new DataChangedEventArgs<T>(ChangedTypes.Updated) { Data = data });
                }

                return result;
            }
        }

        public async Task<int> UpdateAsync(object entity, Expression<Func<T, bool>> whereExpression)
        {
            return await Task.Run(() => Update(entity, whereExpression));
        }

        private DbContext GetDbContext()
        {
            return new DbContext().ConnectionString(_connStr, DatabaseType.MSSQLServer);
        }
    }
}
