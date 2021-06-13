using OrderImport.Application.Core.Commands;
using OrderImport.Domain.Core.Models;

namespace OrderImport.Application.Product.Commands
{
    public class AddProductCommand : CommandBase<Result<AddProductCommand>>
    {
        public AddProductCommand(string sKU, string name, decimal value, string description)
        {
            SKU = sKU;
            Name = name;
            Value = value;
            Description = description;
        }

        public string SKU { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }

    public class AddProductIfNotExistCommand : CommandBase<Result<AddProductIfNotExistCommand>>
    {
        public AddProductIfNotExistCommand(AddProductCommand addProductCommand)
        {
            AddProductCommand = addProductCommand;
        }

        public AddProductCommand AddProductCommand { get; private set; }
    }
}
