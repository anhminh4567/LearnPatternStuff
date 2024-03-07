using System.Linq.Expressions;

namespace TestConceptPattern.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = "");
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);

    }
}
