namespace OrderImport.Domain.Customer.Interfaces
{
    public interface ICustomerValidation
    {
        void AddRuleForCPF();
        void AddRuleForCustomerNotExists();
        void AddRuleForName();
    }
}