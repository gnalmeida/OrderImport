namespace OrderImport.Infra.Data.Repository
{
    public class ProductRepository : Repository<Domain.Product.Entities.Product>, Domain.Product.Interfaces.IProductRepository
    {
        public ProductRepository(Context.OrderImportContext context) 
            : base(context)
        {

        }
    }
}
