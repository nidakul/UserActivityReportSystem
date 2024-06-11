using Application.Features.Activities.Commands.Create;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Logging.SeriLog.Messages;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Security.Hashing;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ActivityRepository : EfRepositoryBase<Activity, Guid, BaseDbContext>, IActivityRepository
{
    public ActivityRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<Activity> CreateActivityAsync(CreateActivityForLogResponse createActivityForLogResponse)
    {
        Activity activity = new Activity
        {
            UserId = createActivityForLogResponse.UserId,
            ActivityType = createActivityForLogResponse.ActivityType,
            Description = createActivityForLogResponse.Description,
        };
        return await AddAsync(activity);

    }
}

