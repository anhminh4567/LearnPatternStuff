using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using TestConceptPattern.Databases;
using TestConceptPattern.Repositories.Interfaces;
using TestConceptPattern.Repositories.Interfaces.Transac;

namespace TestConceptPattern.Repositories.Implementation.Transac
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly SchoolDbContext _context;
        private IDbContextTransaction? _currentTransaction;

        public IRepositoryWrapper Repositories { get; private set; }
        public UnitOfWork(SchoolDbContext context, IRepositoryWrapper repository)
        {
            _context = context;
            Repositories = repository;

        }
        public Task BeginTransaction()
		{
             _currentTransaction = _context.Database.BeginTransaction();
			return Task.CompletedTask;
        }

        public Task CommitAsync()
		{
            if (_currentTransaction is null)
                throw new InvalidOperationException(" a transaction has not been initialized");
            return _currentTransaction.CommitAsync();
		}


        public Task RollBackAsync()
		{
            if (_currentTransaction is null)
                throw new InvalidOperationException(" a transaction has not been initialized");
            return _currentTransaction.RollbackAsync();
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
            //_currentTransaction = null;
            _context.Dispose();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
