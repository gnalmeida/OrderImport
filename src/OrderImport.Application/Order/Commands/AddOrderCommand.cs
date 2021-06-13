using OrderImport.Application.Core.Commands;
using OrderImport.Application.Customer.Dtos;
using OrderImport.Application.Product.Dtos;
using OrderImport.Domain.Core.Models;
using System.Collections.Generic;

namespace OrderImport.Application.Order.Commands
{
    public class AddOrderCommand : CommandBase<Result<AddOrderCommand>>
    {
        public AddOrderCommand(string code,
                               string street,
                               string number,
                               string complement,
                               string neighborhood,
                               string city,
                               string state,
                               string country,
                               string postalCode,
                               CustomerViewModel customer,
                               List<ProductViewModel> orderProducts)
        {
            Code = code;
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Customer = customer;
            OrderProducts = orderProducts;
        }

        public string Code { get; private set; }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }

        public CustomerViewModel Customer { get; private set; }
        public List<ProductViewModel> OrderProducts { get; private set; }
    }
}
