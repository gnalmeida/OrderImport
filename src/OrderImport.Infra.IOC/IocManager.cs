using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderImport.Application.Core.Notifications;
using OrderImport.Application.Customer.Commands;
using OrderImport.Application.Customer.Handlers;
using OrderImport.Application.Order.Commands;
using OrderImport.Application.Order.Dtos;
using OrderImport.Application.Order.Handlers;
using OrderImport.Application.Product.Commands;
using OrderImport.Application.Product.Handlers;
using OrderImport.Domain.Core.Interfaces;
using OrderImport.Domain.Core.Models;
using OrderImport.Domain.Customer.Interfaces;
using OrderImport.Domain.Customer.Validations;
using OrderImport.Domain.Order.Interfaces;
using OrderImport.Domain.Order.Validations;
using OrderImport.Domain.Product.Interfaces;
using OrderImport.Domain.Product.Validations;
using OrderImport.Infra.Data.Context;
using OrderImport.Infra.Data.Repository;
using OrderImport.Infra.Data.UoW;

namespace OrderInvoice.Infra.IOC
{
    public class IocManager
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain
            services.AddScoped<ICustomerValidation, CustomerValidation>();
            services.AddScoped<IProductValidation, ProductValidation>();
            services.AddScoped<IOrderValidation, OrderValidation>();

            //Application
            services.AddScoped<IRequestHandler<AddOrderCommand, Result<AddOrderCommand>>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<AddCustomerCommand, Result<AddCustomerCommand>>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<AddCustomerIfNotExistsCommand, Result<AddCustomerIfNotExistsCommand>>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<AddProductCommand, Result<AddProductCommand>>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<AddProductIfNotExistCommand, Result<AddProductIfNotExistCommand>>, ProductCommandHandler>();

            services.AddScoped<IRequestHandler<GetOrderQuery, OrderViewModelResult>, OrderQueryHandler>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Repositorys
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //Infra
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<OrderImportContext>();

            //services.AddDbContext<OrderImportContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
        }
    }
}
