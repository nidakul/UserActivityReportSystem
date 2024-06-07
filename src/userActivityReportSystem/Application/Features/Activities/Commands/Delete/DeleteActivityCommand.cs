using Application.Features.Activities.Constants;
using Application.Features.Activities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommand : IRequest<DeletedActivityResponse>, ILoggableRequest
{
    public Guid Id { get; set; }

    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, DeletedActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;
        private readonly ActivityBusinessRules _activityBusinessRules;

        public DeleteActivityCommandHandler(IMapper mapper, IActivityRepository activityRepository,
                                         ActivityBusinessRules activityBusinessRules)
        {
            _mapper = mapper;
            _activityRepository = activityRepository;
            _activityBusinessRules = activityBusinessRules;
        }

        public async Task<DeletedActivityResponse> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            Activity? activity = await _activityRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _activityBusinessRules.ActivityShouldExistWhenSelected(activity);

            await _activityRepository.DeleteAsync(activity!);

            DeletedActivityResponse response = _mapper.Map<DeletedActivityResponse>(activity);
            return response;
        }
    }
}