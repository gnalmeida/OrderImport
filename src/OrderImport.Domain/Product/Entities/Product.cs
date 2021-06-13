using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Product.Interfaces;
using OrderImport.Domain.Product.Validations;
using System;

namespace OrderImport.Domain.Product.Entities
{
    public class Product : Entity<Product>
    {
        protected Product() { }

        private Product(string sKU, string name, string description, decimal value)
        {
            Id = Guid.NewGuid();
            SKU = sKU;
            Name = name;
            Description = description;
            Value = value;
        }

        public string SKU { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Value { get; private set; }

        public static Product Create(string sKU, string name, string description, decimal value, IProductValidation productValidation)
        {
            var product = new Product(sKU, name, description, value);

            productValidation.AddRuleForName();
            productValidation.AddRuleForSKU();
            productValidation.AddRuleForProductNotExists();

            var validation = ((ProductValidation)productValidation).Validate(product);
            if (!validation.IsValid)
            {
                throw new DomainException(validation.Errors);
            };

            return product;
        }
    }
}
