using Application.Features.Activities.Commands.Create;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Logging.SeriLog.Messages;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using MailKit.Search;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Logging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Activities.Queries.GetActivitiesReport
{
    public class GetActivitiesReportQuery : IRequest<byte[]>, ILoggableRequest
    {
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
    }

    public class GetActivitiesReportQueryHandler : IRequestHandler<GetActivitiesReportQuery, byte[]>
    {
        private readonly IActivityRepository _activityRepository;

        public GetActivitiesReportQueryHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<byte[]> Handle(GetActivitiesReportQuery request, CancellationToken cancellationToken)
        {
            var activities = await _activityRepository.GetListAsync(
                predicate: a => a.UserId == request.UserId && a.CreatedDate >= request.StartDate && a.CreatedDate <= request.EndDate,
                cancellationToken: cancellationToken);

            var sortedActivities = activities.Items.OrderByDescending(a => a.CreatedDate).ToList();

            byte[] reportData;

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",  
                HasHeaderRecord = true 
            }))
            {
                csvWriter.WriteHeader<GetActivitiesReportResponse>();
                csvWriter.NextRecord();
                foreach (var activity in sortedActivities)
                {
                    var record = new GetActivitiesReportResponse
                    {
                        ActivityType = activity.ActivityType,
                        Description = activity.Description,
                        CreatedDate = activity.CreatedDate
                    };
                    csvWriter.WriteRecord(record); 
                    csvWriter.NextRecord(); 
                }
                writer.Flush(); 
                reportData = memoryStream.ToArray();
            }

            CreateActivityForLogResponse createActivityForLogResponse = new CreateActivityForLogResponse
                { 
                    UserId = request.UserId,
                    ActivityType = "Dosya İndirme",
                    Description = SerilogMessages.DownloadActivityReportMessage,
                };

            await _activityRepository.CreateActivityAsync(createActivityForLogResponse);

            return reportData;

        }
    }
}

 