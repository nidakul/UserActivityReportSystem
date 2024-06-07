using Application.Features.Activities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.Activities.Commands.Create;

public class CreateActivityCommand : IRequest<CreatedActivityResponse>, ILoggableRequest
{
    public required Guid UserId { get; set; }
    public required string ActivityType { get; set; }
    public required string Description { get; set; }

    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, CreatedActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;
        private readonly ActivityBusinessRules _activityBusinessRules;

        public CreateActivityCommandHandler(IMapper mapper, IActivityRepository activityRepository,
                                         ActivityBusinessRules activityBusinessRules)
        {
            _mapper = mapper;
            _activityRepository = activityRepository;
            _activityBusinessRules = activityBusinessRules;
        }

        public async Task<CreatedActivityResponse> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            Activity activity = _mapper.Map<Activity>(request);

            await _activityRepository.AddAsync(activity);

            CreatedActivityResponse response = _mapper.Map<CreatedActivityResponse>(activity);
            return response;
        }
    }
}