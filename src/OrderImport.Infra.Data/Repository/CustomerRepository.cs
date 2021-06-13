namespace OrderImport.Infra.Data.Repository
{
    public class CustomerRepository : Repository<Domain.Customer.Entities.Customer>, Domain.Customer.Interfaces.ICustomerRepository
    {
        public CustomerRepository(Context.OrderImportContext context)
            : base(context)
        {

        }
    }
}
