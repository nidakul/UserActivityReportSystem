using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Activities.Queries.GetList;

public class GetListActivityListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ActivityType { get; set; }
    public string Description { get; set; }
}