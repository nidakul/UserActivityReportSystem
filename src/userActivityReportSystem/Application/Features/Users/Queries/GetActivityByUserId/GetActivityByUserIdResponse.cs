using NArchitecture.Core.Application.Responses;
using System;
namespace Application.Features.Users.Queries.GetActivityByUserId
{
    public class GetActivityByUserIdResponse : IResponse
    {
        public Guid Id { get; set; }
        public List<GetActivityByUserId> UserActivities { get; set; }


       


        public GetActivityByUserIdResponse()
        {
        }
    }

    public class GetActivityByUserId
    {
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

