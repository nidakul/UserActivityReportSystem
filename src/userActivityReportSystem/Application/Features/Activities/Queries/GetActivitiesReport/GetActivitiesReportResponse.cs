using NArchitecture.Core.Application.Responses;
using System;
namespace Application.Features.Activities.Queries.GetActivitiesReport
{
    public class GetActivitiesReportResponse : IResponse
    {
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public GetActivitiesReportResponse()
        {
        }
    }
}

