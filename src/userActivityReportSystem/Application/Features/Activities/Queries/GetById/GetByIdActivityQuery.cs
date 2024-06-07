using Application.Features.Activities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Activities.Queries.GetById;

public class GetByIdActivityQuery : IRequest<GetByIdActivityResponse>
{
    public Guid Id { get; set; }

    public class GetByIdActivityQueryHandler : IRequestHandler<GetByIdActivityQuery, GetByIdActivityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _activityRepository;
        private readonly ActivityBusinessRules _activityBusinessRules;

        public GetByIdActivityQueryHandler(IMapper mapper, IActivityRepository activityRepository, ActivityBusinessRules activityBusinessRules)
        {
            _mapper = mapper;
            _activityRepository = activityRepository;
            _activityBusinessRules = activityBusinessRules;
        }

        public async Task<GetByIdActivityResponse> Handle(GetByIdActivityQuery request, CancellationToken cancellationToken)
        {
            Activity? activity = await _activityRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _activityBusinessRules.ActivityShouldExistWhenSelected(activity);

            GetByIdActivityResponse response = _mapper.Map<GetByIdActivityResponse>(activity);
            return response;
        }
    }
}