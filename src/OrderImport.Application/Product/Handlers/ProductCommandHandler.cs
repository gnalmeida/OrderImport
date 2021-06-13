using OrderImport.Application.Core.Commands;
using OrderImport.Application.Product.Commands;
using OrderImport.Domain.Core.Interfaces;
using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Product.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderImport.Application.Product.Handlers
{
    public class ProductCommandHandler : CommandHandlerBase, 
        ICommandHandler<AddProductCommand, Result<AddProductCommand>>,
        ICommandHandler<AddProductIfNotExistCommand, Result<AddProductIfNotExistCommand>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductValidation _productValidation;

        public ProductCommandHandler(IProductRepository productRepository,
                                     IProductValidation productValidation,
                                     IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _productRepository = productRepository;
            _productValidation = productValidation;
        }

        public async Task<Result<AddProductCommand>> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var result = await Add(command);

            await Commit(result);

            return result;
        }

        public async Task<Result<AddProductIfNotExistCommand>> Handle(AddProductIfNotExistCommand command, CancellationToken cancellationToken)
        {
            var product = (await _productRepository.FindAsync(c => c.SKU == command.AddProductCommand.SKU)).SingleOrDefault();

            if (product == null)
            {
                var result = await Add(command.AddProductCommand);
            }
            else
            {
                command.AddProductCommand.SetId(product.Id);
            }

            return new Result<AddProductIfNotExistCommand>(command);
        }

        public async Task<Result<AddProductCommand>> Add(AddProductCommand command)
        {
            var result = new Result<AddProductCommand>(command);

            var product = Domain.Product.Entities.Product.Create(command.SKU,
                                                                 command.Name,
                                                                 command.Description,
                                                                 command.Value,
                                                                 _productValidation);

            await _productRepository.AddAsync(product);

            result.Command.SetId(product.Id);

            return result;
        }
    }
}
