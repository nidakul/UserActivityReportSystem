using Application.Features.Activities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.Activities.Commands.Update;

public class UpdateActivityCommand : IRequest<UpdatedActivityResponse>, ILoggableRequest
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string ActivityType { get; set; }
    public required string Description { get; set; }

    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, UpdatedActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;
        private readonly ActivityBusinessRules _activityBusinessRules;

        public UpdateActivityCommandHandler(IMapper mapper, IActivityRepository activityRepository,
                                         ActivityBusinessRules activityBusinessRules)
        {
            _mapper = mapper;
            _activityRepository = activityRepository;
            _activityBusinessRules = activityBusinessRules;
        }

        public async Task<UpdatedActivityResponse> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            Activity? activity = await _activityRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _activityBusinessRules.ActivityShouldExistWhenSelected(activity);
            activity = _mapper.Map(request, activity);

            await _activityRepository.UpdateAsync(activity!);

            UpdatedActivityResponse response = _mapper.Map<UpdatedActivityResponse>(activity);
            return response;
        }
    }
}