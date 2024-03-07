using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TestConceptPattern.Databases;
using TestConceptPattern.Repositories.Interfaces;

namespace TestConceptPattern.Repositories.Implementation
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		private readonly SchoolDbContext _context;
		public RepositoryBase(SchoolDbContext context)
		{
			_context = context;
		}
		public IEnumerable<T> FindAll()
		{
			return _context.Set<T>().AsNoTracking();
		}
		public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
		{
			IQueryable<T> query = _context.Set<T>();
			if (expression != null)
			{
				query = query.Where(expression);
			}

			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}
		public IQueryable<T> FindByCondition_Simplified(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().Where(expression).AsNoTracking();
		}
		public T Create(T entity)
		{
			return _context.Set<T>().Add(entity).Entity;	
		}

		public T Delete(T entity)
		{
			return _context.Set<T>().Remove(entity).Entity;
		}

		public T Update(T entity)
		{
			return _context.Set<T>().Update(entity).Entity;	
		}

		
	}
}
