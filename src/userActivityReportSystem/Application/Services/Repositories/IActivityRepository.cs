using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IActivityRepository : IAsyncRepository<Activity, Guid>, IRepository<Activity, Guid>
{
}