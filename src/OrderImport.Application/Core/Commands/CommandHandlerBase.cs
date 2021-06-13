using OrderImport.Domain.Core.Interfaces;
using OrderImport.Domain.Core.Models;
using System.Threading.Tasks;

namespace OrderImport.Application.Core.Commands
{
    public abstract class CommandHandlerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected CommandHandlerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Commit<T>(Result<T> result) where T : CommandBase<Result<T>>
        {
            if (result.IsValid && result.Command.Id != default)
            {
                return await _unitOfWork.CommitAsync();
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
