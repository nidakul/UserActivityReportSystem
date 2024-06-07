using NArchitecture.Core.Application.Responses;

namespace Application.Features.Activities.Queries.GetById;

public class GetByIdActivityResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ActivityType { get; set; }
    public string Description { get; set; }
}