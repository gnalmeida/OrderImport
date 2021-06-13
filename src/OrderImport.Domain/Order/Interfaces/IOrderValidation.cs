namespace OrderImport.Domain.Order.Interfaces
{
    public interface IOrderValidation
    {
        void AddRuleForOrderNotExists();
        void AddRuleForOrderProducts();
    }
}