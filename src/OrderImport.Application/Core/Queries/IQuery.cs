using MediatR;

namespace OrderImport.Application.Core.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}