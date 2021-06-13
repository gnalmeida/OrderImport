using OrderImport.Application.Core.Queries;
using OrderImport.Application.Order.Dtos;
using System;

namespace OrderImport.Application.Order.Handlers
{
    public class GetOrderQuery : IQuery<OrderViewModelResult>
    {
        public GetOrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
