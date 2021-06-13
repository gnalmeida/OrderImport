using FluentValidation;
using OrderImport.Domain.Product.Interfaces;
using OrderImport.Domain.Product.Specifications;

namespace OrderImport.Domain.Product.Validations{

    public class ProductValidation : AbstractValidator<Entities.Product>, IProductValidation
    {
        private readonly IProductRepository _productRepository;

        public ProductValidation(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddRuleForName()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Nome é obrigaório");
        }

        public void AddRuleForSKU()
        {
            RuleFor(c => c.SKU).NotEmpty().WithMessage("SKU é obrigaório");
        }

        public void AddRuleForProductNotExists()
        {
            var spec = new ProductNotExistsSpecification(_productRepository);

            RuleFor(c => c).Must(spec.IsSatisfiedBy).WithMessage("SKU já cadastrado!");
        }
    }
}