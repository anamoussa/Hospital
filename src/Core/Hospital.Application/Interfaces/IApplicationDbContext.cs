using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Hospital.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors 
        | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields 
        | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties 
        | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>() where TEntity:class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void Dispose();
}