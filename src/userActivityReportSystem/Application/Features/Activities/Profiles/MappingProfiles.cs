using Application.Features.Activities.Commands.Create;
using Application.Features.Activities.Commands.Delete;
using Application.Features.Activities.Commands.Update;
using Application.Features.Activities.Queries.GetById;
using Application.Features.Activities.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Activities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateActivityCommand, Activity>();
        CreateMap<Activity, CreatedActivityResponse>();

        CreateMap<UpdateActivityCommand, Activity>();
        CreateMap<Activity, UpdatedActivityResponse>();

        CreateMap<DeleteActivityCommand, Activity>();
        CreateMap<Activity, DeletedActivityResponse>();

        CreateMap<Activity, GetByIdActivityResponse>();

        CreateMap<Activity, GetListActivityListItemDto>();
        CreateMap<IPaginate<Activity>, GetListResponse<GetListActivityListItemDto>>();
    }
}