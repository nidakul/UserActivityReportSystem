using NArchitecture.Core.Application.Responses;

namespace Application.Features.Activities.Commands.Create;

public class CreatedActivityResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ActivityType { get; set; }
    public string Description { get; set; }
}