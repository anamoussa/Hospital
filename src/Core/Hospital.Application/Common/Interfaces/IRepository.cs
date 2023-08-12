using System.Linq.Expressions;

namespace Hospital.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> expression);
}
