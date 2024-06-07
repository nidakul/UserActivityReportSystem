using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Activities;

public interface IActivityService
{
    Task<Activity?> GetAsync(
        Expression<Func<Activity, bool>> predicate,
        Func<IQueryable<Activity>, IIncludableQueryable<Activity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Activity>?> GetListAsync(
        Expression<Func<Activity, bool>>? predicate = null,
        Func<IQueryable<Activity>, IOrderedQueryable<Activity>>? orderBy = null,
        Func<IQueryable<Activity>, IIncludableQueryable<Activity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Activity> AddAsync(Activity activity);
    Task<Activity> UpdateAsync(Activity activity);
    Task<Activity> DeleteAsync(Activity activity, bool permanent = false);
}
