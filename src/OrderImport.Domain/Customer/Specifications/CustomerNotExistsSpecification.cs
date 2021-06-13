using OrderImport.Domain.Core.Specifications;
using OrderImport.Domain.Customer.Interfaces;
using System.Linq;

namespace OrderImport.Domain.Customer.Specifications
{
    public class CustomerNotExistsSpecification : BaseSpecification<Customer.Entities.Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerNotExistsSpecification(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public override bool IsSatisfiedBy(Entities.Customer customer)
        {
            return !_customerRepository.FindAsync(c => c.CPF.Number == customer.CPF.Number).Result.Any();
        }
    }
}