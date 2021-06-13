using OrderImport.Domain.Core.Interfaces;
using OrderImport.Infra.Data.Context;
using System.Threading.Tasks;

namespace OrderImport.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderImportContext _context;

        public UnitOfWork(OrderImportContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return rowsAffected > 0;
        }

        public async Task<bool> CommitAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
