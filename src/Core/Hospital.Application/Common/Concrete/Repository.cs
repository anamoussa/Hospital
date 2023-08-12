using Hospital.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Application.Common.Concrete;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IApplicationDbContext _context;

    public Repository(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await GetAsync(expression);
        _context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        var result = await _context.Set<T>().FirstOrDefaultAsync(expression);
        return result!;
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
