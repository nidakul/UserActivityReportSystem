using Application.Services.Repositories;
using CsvHelper;
using MediatR;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Activities.Queries.GetActivitiesReport
{
    public class GetActivitiesReportQuery : IRequest<byte[]>
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

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(activities.Items);
                writer.Flush();
                return memoryStream.ToArray();
            }
        }
    }
}
