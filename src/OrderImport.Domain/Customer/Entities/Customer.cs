using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Core.ValueObjects;
using OrderImport.Domain.Customer.Interfaces;
using OrderImport.Domain.Customer.Validations;
using System;

namespace OrderImport.Domain.Customer.Entities
{
    public class Customer : Entity<Customer>
    {
        protected Customer() { }

        private Customer(string name, CPF cpf)
        {
            Id = Guid.NewGuid();
            Name = name;
            CPF = cpf;
        }

        public string Name { get; private set; }
        public CPF CPF { get; private set; }

        public static Customer Create(string name, CPF cpf, ICustomerValidation customerValidation)
        {
            var customer = new Customer(name, cpf);

            customerValidation.AddRuleForName();
            customerValidation.AddRuleForCPF();
            customerValidation.AddRuleForCustomerNotExists();

            var validation = ((CustomerValidation)customerValidation).Validate(customer);
            if (!validation.IsValid)
            {
                throw new DomainException(validation.Errors);
            };

            return customer;
        }
    }
}
