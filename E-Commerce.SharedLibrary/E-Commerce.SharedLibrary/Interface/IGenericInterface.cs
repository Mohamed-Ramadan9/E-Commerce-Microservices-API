using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.SharedLibrary.Responses;

namespace E_Commerce.SharedLibrary.Interface
{
    public interface IGenericInterface<T>where T : class
    {
        Task<Response> CreateAsync(T entity);

        Task<Response> UpdateAsync(T entity);

        Task<Response> DeleteAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T>GetByAsync(Expression<Func<T , bool >> predicate);
        Task<T>FindByIdAsync(int id);
    }
}
