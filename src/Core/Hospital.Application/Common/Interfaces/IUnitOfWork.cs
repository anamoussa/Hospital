
namespace Hospital.Application.Common.Interfaces;

public interface IUnitOfWork:IDisposable
{
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
