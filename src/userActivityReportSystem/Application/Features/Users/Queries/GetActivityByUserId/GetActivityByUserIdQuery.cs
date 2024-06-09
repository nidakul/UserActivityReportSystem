using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
namespace Application.Features.Users.Queries.GetActivityByUserId
{
    public class GetActivityByUserIdQuery : IRequest<GetActivityByUserIdResponse>
    {
        public Guid UserId { get; set; }

        public class GetActivityByUserIdQueryHandler : IRequestHandler<GetActivityByUserIdQuery, GetActivityByUserIdResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public GetActivityByUserIdQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }
            public async Task<GetActivityByUserIdResponse> Handle(GetActivityByUserIdQuery request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetAsync(predicate: u => u.Id.Equals(request.UserId),
                include: u => u.Include(u => u.Activities), 

                enableTracking: false,
                    cancellationToken: cancellationToken);
                await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

                GetActivityByUserIdResponse response = _mapper.Map<GetActivityByUserIdResponse>(user);
                return response;
            }

        }
    }
}

