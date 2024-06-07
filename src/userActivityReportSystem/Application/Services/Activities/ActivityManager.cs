using Application.Features.Activities.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Activities;

public class ActivityManager : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private readonly ActivityBusinessRules _activityBusinessRules;

    public ActivityManager(IActivityRepository activityRepository, ActivityBusinessRules activityBusinessRules)
    {
        _activityRepository = activityRepository;
        _activityBusinessRules = activityBusinessRules;
    }

    public async Task<Activity?> GetAsync(
        Expression<Func<Activity, bool>> predicate,
        Func<IQueryable<Activity>, IIncludableQueryable<Activity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Activity? activity = await _activityRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return activity;
    }

    public async Task<IPaginate<Activity>?> GetListAsync(
        Expression<Func<Activity, bool>>? predicate = null,
        Func<IQueryable<Activity>, IOrderedQueryable<Activity>>? orderBy = null,
        Func<IQueryable<Activity>, IIncludableQueryable<Activity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Activity> activityList = await _activityRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return activityList;
    }

    public async Task<Activity> AddAsync(Activity activity)
    {
        Activity addedActivity = await _activityRepository.AddAsync(activity);

        return addedActivity;
    }

    public async Task<Activity> UpdateAsync(Activity activity)
    {
        Activity updatedActivity = await _activityRepository.UpdateAsync(activity);

        return updatedActivity;
    }

    public async Task<Activity> DeleteAsync(Activity activity, bool permanent = false)
    {
        Activity deletedActivity = await _activityRepository.DeleteAsync(activity);

        return deletedActivity;
    }
}
