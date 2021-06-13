using OrderImport.Application.Customer.Dtos;
using OrderImport.Application.Product.Dtos;
using System;
using System.Collections.Generic;

namespace OrderImport.Application.Order.Dtos
{
    public class OrderViewModelResult
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public CustomerViewModel Customer { get; set; }
        public List<ProductViewModel> OrderProducts { get; set; }
    }
}
