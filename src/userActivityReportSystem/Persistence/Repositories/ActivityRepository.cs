using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ActivityRepository : EfRepositoryBase<Activity, Guid, BaseDbContext>, IActivityRepository
{
    public ActivityRepository(BaseDbContext context) : base(context)
    {
    }
}