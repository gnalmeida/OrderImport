using FluentValidation;
using OrderImport.Domain.Core.ValueObjects;
using OrderImport.Domain.Customer.Interfaces;
using OrderImport.Domain.Customer.Specifications;

namespace OrderImport.Domain.Customer.Validations
{

    public class CustomerValidation : AbstractValidator<Customer.Entities.Customer>, ICustomerValidation
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerValidation(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddRuleForName()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Nome é obrigaório");
        }

        public void AddRuleForCPF()
        {
            RuleFor(c => c.CPF.Number).NotEmpty().WithMessage("CPF é obrigaório");
            RuleFor(c => c.CPF.Number).Must(CPF.Validate).WithMessage("CPF inválido");
        }

        public void AddRuleForCustomerNotExists()
        {
            var spec = new CustomerNotExistsSpecification(_customerRepository);

            RuleFor(c => c).Must(spec.IsSatisfiedBy).WithMessage("CPF já cadastrado!").OverridePropertyName(c => c.CPF);
        }
    }
}