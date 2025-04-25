
using System.Linq.Expressions;
using E_Commerce.SharedLibrary.Interface;
using OrderAPi_Domain.Entites;

namespace OrderAPi_Application.Interfaces
{
    public interface IOrder:IGenericInterface<Order>
    {
        Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> predicate);
    }
}
