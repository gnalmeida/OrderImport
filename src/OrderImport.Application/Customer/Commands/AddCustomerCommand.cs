using OrderImport.Application.Core.Commands;
using OrderImport.Domain.Core.Models;

namespace OrderImport.Application.Customer.Commands
{    
    public class AddCustomerCommand : CommandBase<Result<AddCustomerCommand>>
    {
        public AddCustomerCommand(string name, string cPF)
        {
            Name = name;
            CPF = cPF;
        }

        public string Name { get; private set; }
        public string CPF { get; private set; }
    }

    public class AddCustomerIfNotExistsCommand : CommandBase<Result<AddCustomerIfNotExistsCommand>>
    {
        public AddCustomerIfNotExistsCommand(AddCustomerCommand addCustomerCommand)
        {
            this.AddCustomerCommand = addCustomerCommand;
        }

        public AddCustomerCommand AddCustomerCommand { get; private set; }
    }
}
