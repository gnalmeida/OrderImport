using OrderImport.Domain.Core.Specifications;
using OrderImport.Domain.Product.Interfaces;
using System.Linq;

namespace OrderImport.Domain.Product.Specifications
{
    public class ProductNotExistsSpecification : BaseSpecification<Product.Entities.Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductNotExistsSpecification(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override bool IsSatisfiedBy(Entities.Product product)
        {
            return !_productRepository.FindAsync(p => p.SKU == product.SKU).Result.Any();
        }
    }
}