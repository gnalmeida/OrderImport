namespace OrderImport.Domain.Core.Specifications
{
    public abstract class BaseSpecification<T>
    {
        public abstract bool IsSatisfiedBy(T obj);
    }

}