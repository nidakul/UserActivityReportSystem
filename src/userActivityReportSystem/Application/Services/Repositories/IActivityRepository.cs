using Application.Features.Activities.Commands.Create;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IActivityRepository : IAsyncRepository<Activity, Guid>, IRepository<Activity, Guid>
{
    Task<Activity> CreateActivityAsync(CreateActivityForLogResponse createActivityForLogResponse);
}


