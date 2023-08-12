using Hospital.Application.Common.Interfaces;

namespace Hospital.Application.Common.Concrete;

public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly IApplicationDbContext _context;
    public UnitOfWork(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
