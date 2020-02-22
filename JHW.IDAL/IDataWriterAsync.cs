using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JHW.IDAL
{
    public interface IDataWriterAsync<T> where T : class
    {
        Task<dynamic> InsertAsync(T entity);

        Task InsertAsync(IEnumerable<T> entities);

        Task<int> DeleteAsync(Expression<Func<T, bool>> whereExpression);
        
        Task<int> UpdateAsync(object entity, Expression<Func<T, bool>> whereExpression);

        Task<int> ClearTableAsync();
    }
}
