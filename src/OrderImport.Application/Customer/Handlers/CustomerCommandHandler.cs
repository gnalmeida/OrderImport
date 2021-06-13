using OrderImport.Application.Core.Commands;
using OrderImport.Application.Customer.Commands;
using OrderImport.Domain.Core.Interfaces;
using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Core.ValueObjects;
using OrderImport.Domain.Customer.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderImport.Application.Customer.Handlers
{
    public class CustomerCommandHandler : CommandHandlerBase,
        ICommandHandler<AddCustomerCommand, Result<AddCustomerCommand>>,
        ICommandHandler<AddCustomerIfNotExistsCommand, Result<AddCustomerIfNotExistsCommand>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerValidation _customerValidation;

        public CustomerCommandHandler(ICustomerRepository customerRepository,
                                      ICustomerValidation customerValidation,
                                      IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _customerRepository = customerRepository;
            _customerValidation = customerValidation;
        }

        public async Task<Result<AddCustomerCommand>> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
        {
            var result = await AddAsync(command);

            await Commit(result);

            return result;
        }

        public async Task<Result<AddCustomerIfNotExistsCommand>> Handle(AddCustomerIfNotExistsCommand request, CancellationToken cancellationToken)
        {
            Domain.Customer.Entities.Customer customer = (await _customerRepository.FindAsync(c => c.CPF.Number == request.AddCustomerCommand.CPF)).SingleOrDefault();

            if (customer == null)
            {
                await AddAsync(request.AddCustomerCommand);
            }
            else
            {
                request.AddCustomerCommand.SetId(customer.Id);
            }

            return new Result<AddCustomerIfNotExistsCommand>(request);
        }

        private async Task<Result<AddCustomerCommand>> AddAsync(AddCustomerCommand command)
        {
            var result = new Result<AddCustomerCommand>(command);

            var cpf = new CPF(command.CPF);
            var customer = Domain.Customer.Entities.Customer.Create(command.Name,
                                                                    cpf,
                                                                    _customerValidation);

            await _customerRepository.AddAsync(customer);

            result.Command.SetId(customer.Id);

            return result;
        }
    }
}
