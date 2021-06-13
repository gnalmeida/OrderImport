namespace OrderImport.Domain.Product.Interfaces
{
    public interface IProductValidation
    {
        void AddRuleForProductNotExists();
        void AddRuleForName();
        void AddRuleForSKU();
    }
}