namespace OrderImport.Infra.Data.Repository
{
    public class OrderRepository : Repository<Domain.Order.Entities.Order>, Domain.Order.Interfaces.IOrderRepository
    {
        public OrderRepository(Context.OrderImportContext context) 
            : base(context)
        {

        }
    }
}
