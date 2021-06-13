using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderImport.Domain.Customer.Interfaces
{
    public interface ICustomerRepository //: IRepository<Entities.Customer>
    {
        Task<Entities.Customer> GetAsync(Guid id);
        Task<IEnumerable<Entities.Customer>> FindAsync(Expression<Func<Entities.Customer, bool>> predicate);
        Task AddAsync(Entities.Customer customer);
    }
}
