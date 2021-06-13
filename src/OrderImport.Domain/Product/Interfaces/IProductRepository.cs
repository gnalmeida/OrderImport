using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderImport.Domain.Product.Interfaces
{
    public interface IProductRepository //: IRepository<Entities.Product>
    {
        Task<Entities.Product> GetAsync(Guid id);
        Task<IEnumerable<Entities.Product>> FindAsync(Expression<Func<Entities.Product, bool>> predicate);
        Task AddAsync(Entities.Product customer);
    }
}
