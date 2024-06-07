using NArchitecture.Core.Application.Responses;

namespace Application.Features.Activities.Commands.Delete;

public class DeletedActivityResponse : IResponse
{
    public Guid Id { get; set; }
}