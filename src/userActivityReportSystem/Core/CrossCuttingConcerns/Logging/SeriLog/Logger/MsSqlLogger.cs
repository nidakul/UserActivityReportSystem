using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Serilog;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Logger
{
    public class MsSqlLogger : SerilogLoggerServiceBase
    {
        public MsSqlLogger(MsSqlConfiguration configuration) : base(logger: null!)
        {
            Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: "Server=localhost; Database=UserActivityReportSystem; User Id=SA; Password=rentacardb; TrustServerCertificate=True;",
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = configuration.TableName,
                        AutoCreateSqlTable = configuration.AutoCreateSqlTable
                    })
                .CreateLogger();
        }
    }
}

 