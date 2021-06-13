using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderImport.Domain.Order.Interfaces
{
    public interface IOrderRepository //: IRepository<Entities.Order>
    {
        Task<Entities.Order> GetAsync(Guid id);
        Task<IEnumerable<Entities.Order>> FindAsync(Expression<Func<Entities.Order, bool>> predicate);
        Task AddAsync(Entities.Order customer);
    }
}
